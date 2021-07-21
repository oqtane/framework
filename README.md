# Oqtane Framework

[![Deploy to Azure](https://aka.ms/deploytoazurebutton)](https://portal.azure.com/#create/Microsoft.Template/uri/https%3A%2F%2Fraw.githubusercontent.com%2Foqtane%2Foqtane.framework%2Fmaster%2Fazuredeploy.json)

![Oqtane](https://github.com/oqtane/framework/blob/master/oqtane.png?raw=true "Oqtane")

Oqtane is a Modular Application Framework. It leverages Blazor, an open source and cross-platform web UI framework for building single-page apps using .NET and C# instead of JavaScript. Blazor apps are composed of reusable web UI components implemented using C#, HTML, and CSS. Both client and server code is written in C#, allowing you to share code and libraries.

Oqtane is being developed based on some fundamental principles which are outlined in the [Oqtane Philosophy](https://www.oqtane.org/Resources/Blog/PostId/538/oqtane-philosophy).

Please note that this project is owned by the .NET Foundation and is governed by the **[.NET Foundation Contributor Covenant Code of Conduct](https://dotnetfoundation.org/code-of-conduct)**

# Getting Started

**Using Version 2+:**

- Install **[.NET 5 SDK (v5.0.100)](https://dotnet.microsoft.com/download/dotnet/5.0)**.
   
- Install the latest edition (v16.8 or higher) of [Visual Studio 2019](https://visualstudio.microsoft.com/vs/) (Community, Professional, or Enterprise Editions) with the **ASP.NET and web development** workload enabled. Oqtane works with ALL editions of Visual Studio from Community to Enterprise. If you do not have a SQL Server installation available already and you wish to use LocalDB for development, you must also install the **.NET desktop development workload**.  

- Download a release or Clone the Oqtane source code to your local system. Open the **Oqtane.sln** solution file and Build the solution. Make sure you specify Oqtane.Server as the Startup Project and then Run the application.

**Using Version 1:**

- Install **[.NET Core 3.1 SDK (v3.1.300)](https://dotnet.microsoft.com/download/dotnet-core/thank-you/sdk-3.1.300-windows-x64-installer)**.
   
- Install [Visual Studio 2019](https://visualstudio.microsoft.com/vs) (Community, Professional, or Enterprise Editions) with the **ASP.NET and web development** workload enabled. Oqtane works with ALL editions of Visual Studio from Community to Enterprise. If you do not have a SQL Server installation available already and you wish to use LocalDB for development, you must also install the **.NET desktop development workload**.  

- Download a release or Clone the Oqtane source code from a v1.x Tag to your local system. Open the **Oqtane.sln** solution file and Build the solution. Make sure you specify Oqtane.Server as the Startup Project and then Run the application.

**Installing an official release:**

- A detailed set of instructions for installing Oqtane on IIS is located here: [Installing Oqtane on IIS](https://www.oqtane.org/Resources/Blog/PostId/542/installing-oqtane-on-iis)
- Instructions for upgrading Oqtane are located here: [Upgrading Oqtane](https://www.oqtane.org/Resources/Blog/PostId/543/upgrading-oqtane)

**Additional Instructions**

- If you have already installed a previous version of Oqtane and you wish to do a clean database install, simply reset the DefaultConnection value in the Oqtane.Server\appsettings.json file to "". This will trigger a re-install when you run the application which will execute the database installation scripts.
   
- If you want to submit pull requests make sure you install the [Github Extension For Visual Studio](https://visualstudio.github.com/). It is recommended you ignore any local changes you have made to the appsettings.json file before you submit a pull request. To automate this activity, open a command prompt and navigate to the /Oqtane.Server/ folder and enter the command "git update-index --skip-worktree appsettings.json" 

**Video Series**

- If you are getting started with Oqtane, a [series of videos](https://www.youtube.com/watch?v=JPfUZPlRRCE&list=PLYhXmd7yV0elLNLfQwZBUlM7ZSMYPTZ_f) are available which explain how to install the product, interact with the user interface, and develop custom modules.

# Documentation
There is a separate [Documentation repository](https://github.com/oqtane/oqtane.docs) which contains a variety of types of documentation for Oqtane, including API documentation that is auto generated using Docfx. The contents of the repository is published to Githib Pages and is available at [https://docs.oqtane.org](https://docs.oqtane.org/)

# Roadmap
This project is a work in progress and the schedule for implementing enhancements is dependent upon the availability of community members who are willing/able to assist.

V.3.0.0 ( Q4 2021 )
- [ ] Migration to .NET 6

V.2.3.0 ( Q3 2021 )
- [ ] Authentication extensibility ( ie. Azure B2C, Social logins, etc... )
- [ ] Configurable password complexity for local authentication

V.2.2.0 ( Jul 2021 )
- [x] Bootstrap 5 Upgrade
- [x] Package Service Integration
- [x] Default and Shared Resource File Inclusion
- [x] Startup Error Logging
- [x] API Controller Validation and Logging

V.2.1.0 ( Jun 4, 2021 )
- [x] Cross Platform Database Support ( ie. LocalDB, SQL Server, SQLite, MySQL, PostgreSQL ) - see [#964](https://github.com/oqtane/oqtane.framework/discussions/964)
- [x] Utilize EF Core Migrations - see [#964](https://github.com/oqtane/oqtane.framework/discussions/964)
- [x] Public Content Folder Support
- [x] Multi-tenant Infrastructure Improvements
- [x] User Authorization Optimization
- [x] Consiolidation of Package Management
- [x] Blazor Server Pre-rendering
- [x] Translation Package Installation

V.2.0.2 ( Apr 19, 2021 )
- [x] Assorted fixes and user experience improvements

V.2.0.1 ( Feb 27, 2021 )
- [x] Complete Static Localization of Admin UI

V.2.0.0 ( released in conjuntion with .NET 5 on Nov 11, 2020 )
- [x] Migration to .NET 5
- [x] Static Localization ( ie. labels, help text, etc.. )
- [x] Improved JavaScript Reference Support
- [x] Performance Optimizations
- [x] Developer Productivity Enhancements

V.1.0.0 ( released in conjunction with .NET Core 3.2 on May 19, 2020 )
- [x] Multi-Tenant ( Shared Database & Isolated Database ) 
- [x] Modular Architecture
- [x] Headless API with Swagger Support
- [x] Dynamic Page Compositing Model / Site & Page Management
- [x] Authentication / User Management / Profile Management
- [x] Authorization / Roles Management / Granular Permissions
- [x] Dynamic Routing
- [x] Extensibility via Custom Modules
- [x] Extensibility via Custom Themes
- [x] Event Logging / Audit Trail
- [x] Folder / File Management
- [x] Recycle Bin
- [x] Scheduled Jobs ( Background Processing )
- [x] Notifications / Email Delivery
- [x] Seamless Upgrade Experience
- [x] Progressive Web Application Support
- [x] JavaScript Lazy Loading
- [x] Dynamic CSS/Lazy Loading

# Background
Oqtane was created by [Shaun Walker](https://www.linkedin.com/in/shaunbrucewalker/) and is inspired by the DotNetNuke web application framework. Initially created as a proof of concept, Oqtane is a native Blazor application written from the ground up using modern .NET Core technology. It is a modular application framework offering a fully dynamic page compositing model, multi-site support, designer friendly templates (skins), and extensibility via third party modules.

# Release Announcements

[Oqtane 2.0](https://www.oqtane.org/Resources/Blog/PostId/544/announcing-oqtane-20-for-net-5)

[Oqtane 1.0](https://www.oqtane.org/Resources/Blog/PostId/540/announcing-oqtane-10-a-modular-application-framework-for-blazor)

[Oqtane POC](https://www.oqtane.org/Resources/Blog/PostId/520/announcing-oqtane-a-modular-application-framework-for-blazor)

# Architecture

The following diagram visualizes the client and server components in the Oqtane architecture.

![Architecture](https://github.com/oqtane/framework/blob/master/screenshots/Architecture.png?raw=true "Oqtane Architecture")

# Databases

As of version 2.1, Oqtane supports multiple relational database providers.

![Databases](https://github.com/oqtane/framework/blob/dev/screenshots/databases.png?raw=true "Oqtane Databases")

# Example Screenshots

Install Wizard:

![Installer](https://github.com/oqtane/framework/blob/master/screenshots/Installer.png?raw=true "Installer")

Default view after installation:

![Home](https://github.com/oqtane/framework/blob/master/screenshots/screenshot0.png?raw=true "Home")

A seamless login flow utilizing .NET Core Identity services:

![Login](https://github.com/oqtane/framework/blob/master/screenshots/screenshot1.png?raw=true "Login")

Main view for authorized users, allowing full management of modules and content:

![Admin View](https://github.com/oqtane/framework/blob/master/screenshots/screenshot2.png?raw=true "Admin View")

Content editing user experience using modal dialog:

![Edit Content](https://github.com/oqtane/framework/blob/master/screenshots/screenshot3.png?raw=true "Edit Content")

Context menu for managing specific module on page:

![Manage Module](https://github.com/oqtane/framework/blob/master/screenshots/screenshot4.png?raw=true "Manage Module")

Control panel for adding, editing, and deleting pages as well as adding new modules to a page:

![Manage Page](https://github.com/oqtane/framework/blob/master/screenshots/screenshot5.png?raw=true "Manage Page")

Admin dashboard for accessing the various administrative features of the framework:

![Admin Dashboard](https://github.com/oqtane/framework/blob/master/screenshots/screenshot6.png?raw=true "Admin Dashboard")

Responsive design mobile view:

![Mobile View](https://github.com/oqtane/framework/blob/master/screenshots/screenshot7.png?raw=true "Mobile View")
