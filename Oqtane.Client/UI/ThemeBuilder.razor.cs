﻿using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Oqtane.Models;
using Oqtane.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Oqtane.UI
{
    public partial class ThemeBuilder
    {
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime _jsRuntime;

        public ThemeBuilder(NavigationManager navigationManager, IJSRuntime jsRuntime)
        {
            _navigationManager = navigationManager;
            _jsRuntime = jsRuntime;
        }

        [CascadingParameter] PageState PageState { get; set; }

        RenderFragment DynamicComponent { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            var interop = new Interop(_jsRuntime);

            // handle page redirection
            if (!string.IsNullOrEmpty(PageState.Page.Url))
            {
                _navigationManager.NavigateTo(PageState.Page.Url);
                return;
            }

            // set page title
            if (!string.IsNullOrEmpty(PageState.Page.Title))
            {
                await interop.UpdateTitle(PageState.Page.Title);
            }
            else
            {
                await interop.UpdateTitle(PageState.Site.Name + " - " + PageState.Page.Name);
            }

            // manage stylesheets for this page 
            string batch = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            var links = new List<object>();
            foreach (Resource resource in PageState.Page.Resources.Where(item => item.ResourceType == ResourceType.Stylesheet))
            {
                links.Add(new { id = "app-stylesheet-" + batch + "-" + (links.Count + 1).ToString("00"), rel = "stylesheet", href = resource.Url, type = "text/css", integrity = resource.Integrity ?? string.Empty, crossorigin = resource.CrossOrigin ?? string.Empty, key = string.Empty });
            }
            await interop.IncludeLinks(links.ToArray());
            await interop.RemoveElementsById("app-stylesheet", string.Empty, "app-stylesheet-" + batch + "-00");

            // add favicon
            if (PageState.Site.FaviconFileId != null)
            {
                await interop.IncludeLink("app-favicon", "shortcut icon", Utilities.ContentUrl(PageState.Alias, PageState.Site.FaviconFileId.Value), "image/x-icon", string.Empty, string.Empty, "id");
            }
            // add PWA support
            if (PageState.Site.PwaIsEnabled)
            {
                await InitializePwa(interop);
            }

            DynamicComponent = builder =>
            {
                var themeType = Type.GetType(PageState.Page.ThemeType);
                builder.OpenComponent(0, themeType);
                builder.CloseComponent();
            };
        }

        private async Task InitializePwa(Interop interop)
        {
            // dynamically create manifest.json and add to page
            string manifest = "setTimeout(() => { " +
                "var manifest = { " +
                "\"name\": \"" + PageState.Site.Name + "\", " +
                "\"short_name\": \"" + PageState.Site.Name + "\", " +
                "\"start_url\": \"/\", " +
                "\"display\": \"standalone\", " +
                "\"background_color\": \"#fff\", " +
                "\"description\": \"" + PageState.Site.Name + "\", " +
                "\"icons\": [{ " +
                    "\"src\": \"" + Utilities.ContentUrl(PageState.Alias, PageState.Site.PwaAppIconFileId.Value) + "\", " +
                    "\"sizes\": \"192x192\", " +
                    "\"type\": \"image/png\" " +
                    "}, { " +
                    "\"src\": \"" + Utilities.ContentUrl(PageState.Alias, PageState.Site.PwaSplashIconFileId.Value) + "\", " +
                    "\"sizes\": \"512x512\", " +
                    "\"type\": \"image/png\" " +
                "}] " +
                "} " +
                "const serialized = JSON.stringify(manifest); " +
                "const blob = new Blob([serialized], {type: 'application/javascript'}); " +
                "const url = URL.createObjectURL(blob); " +
                "document.getElementById('app-manifest').setAttribute('href', url); " +
                "} " +
                ", 1000);";
            await interop.IncludeScript("app-pwa", string.Empty, string.Empty, string.Empty, manifest, "body", "id");

            // service worker must be in root of site
            string serviceworker = "if ('serviceWorker' in navigator) { " +
                "navigator.serviceWorker.register('/service-worker.js').then(function(registration) { " +
                    "console.log('ServiceWorker Registration Successful'); " +
                "}).catch (function(err) { " +
                    "console.log('ServiceWorker Registration Failed ', err); " +
                "}); " +
                "}";
            await interop.IncludeScript("app-serviceworker", string.Empty, string.Empty, string.Empty, serviceworker, "body", "id");
        }
    }
}
