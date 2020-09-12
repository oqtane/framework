﻿using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using Oqtane.Services;
using System;
using System.Linq;

namespace Oqtane.UI
{
    public partial class Pane
    {
        private readonly IUserService _userService;
        private readonly IModuleService _moduleService;
        private readonly IModuleDefinitionService _moduleDefinitionService;

        public Pane(IUserService userService, IModuleService moduleService, IModuleDefinitionService moduleDefinitionService)
        {
            _userService = userService;
            _moduleService = moduleService;
            _moduleDefinitionService = moduleDefinitionService;
        }

        private string _paneadminborder = "";
        private string _panetitle = "";

        [CascadingParameter]
        protected PageState PageState { get; set; }

        [Parameter]
        public string Name { get; set; }

        RenderFragment DynamicComponent { get; set; }

        protected override void OnParametersSet()
        {
            if (PageState.EditMode && UserSecurity.IsAuthorized(PageState.User, PermissionNames.Edit, PageState.Page.Permissions) && Name != Constants.AdminPane)
            {
                _paneadminborder = "app-pane-admin-border";
                _panetitle = "<div class=\"app-pane-admin-title\">" + Name + " Pane</div>";
            }
            else
            {
                _paneadminborder = "";
                _panetitle = "";
            }

            DynamicComponent = builder =>
            {
                if (PageState.ModuleId != -1 && PageState.Action != Constants.DefaultAction)
                {
                    if (Name.ToLower() == Constants.AdminPane.ToLower())
                    {
                        Module module = PageState.Modules.FirstOrDefault(item => item.ModuleId == PageState.ModuleId);
                        if (module != null && !module.IsDeleted)
                        {
                            var typename = module.ModuleType;
                            // check for core module actions component
                            if (Constants.DefaultModuleActions.Contains(PageState.Action))
                            {
                                typename = Constants.DefaultModuleActionsTemplate.Replace(Constants.ActionToken, PageState.Action);
                            }

                            var moduleType = Type.GetType(typename);
                            if (moduleType != null)
                            {
                                bool authorized = false;
                                if (Constants.DefaultModuleActions.Contains(PageState.Action))
                                {
                                    authorized = UserSecurity.IsAuthorized(PageState.User, PermissionNames.Edit, PageState.Page.Permissions);
                                }
                                else
                                {
                                    switch (module.SecurityAccessLevel)
                                    {
                                        case SecurityAccessLevel.Anonymous:
                                            authorized = true;
                                            break;
                                        case SecurityAccessLevel.View:
                                            authorized = UserSecurity.IsAuthorized(PageState.User, PermissionNames.View, module.Permissions);
                                            break;
                                        case SecurityAccessLevel.Edit:
                                            authorized = UserSecurity.IsAuthorized(PageState.User, PermissionNames.Edit, module.Permissions);
                                            break;
                                        case SecurityAccessLevel.Admin:
                                            authorized = UserSecurity.IsAuthorized(PageState.User, Constants.AdminRole);
                                            break;
                                        case SecurityAccessLevel.Host:
                                            authorized = UserSecurity.IsAuthorized(PageState.User, Constants.HostRole);
                                            break;
                                    }
                                }

                                if (authorized)
                                {
                                    if (!Constants.DefaultModuleActions.Contains(PageState.Action) && module.ControlTitle != "")
                                    {
                                        module.Title = module.ControlTitle;
                                    }
                                    CreateComponent(builder, module);
                                }
                            }
                            else
                            {
                                // module control does not exist with name specified
                            }
                        }
                    }
                }
                else
                {
                    if (PageState.ModuleId != -1)
                    {
                        Module module = PageState.Modules.FirstOrDefault(item => item.ModuleId == PageState.ModuleId);
                        if (module != null && module.Pane.ToLower() == Name.ToLower() && !module.IsDeleted)
                        {
                            // check if user is authorized to view module
                            if (UserSecurity.IsAuthorized(PageState.User, PermissionNames.View, module.Permissions))
                            {
                                CreateComponent(builder, module);
                            }
                        }
                    }
                    else
                    {
                        foreach (Module module in PageState.Modules.Where(item => item.PageId == PageState.Page.PageId && item.Pane.ToLower() == Name.ToLower() && !item.IsDeleted).OrderBy(x => x.Order).ToArray())
                        {
                            // check if user is authorized to view module
                            if (UserSecurity.IsAuthorized(PageState.User, PermissionNames.View, module.Permissions))
                            {
                                CreateComponent(builder, module);
                            }
                        }
                    }
                }
            };
        }

        private void CreateComponent(RenderTreeBuilder builder, Module module)
        {
            builder.OpenComponent(0, Type.GetType(Constants.ContainerComponent));
            builder.AddAttribute(1, "Module", module);
            builder.SetKey(module.PageModuleId);
            builder.CloseComponent();
        }
    }
}
