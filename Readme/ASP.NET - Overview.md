# ASP.NET - Overview

| No | Topic                                                                |
| -- | -------------------------------------------------------------------- |
| 1  | [What is ASP?](#what-is-asp)                                         |
|    | **.Net Framework**                                                   |
| 2  | [Web Forms](#web-forms)                                              |
|    | **.Net Core**                                                        |
| 3  | [ASP.NET Core Application Model](#aspnet-core-application-model)     |
|    | **MVC**                                                              |
| 4  | [Model View Controller](#model-view-controller)                      |
|    | **EF Core**                                                          |
| 5  | [Introduction to ORM](#introduction-to-net-orm)                      |
|    | **Testing**                                                          |
| 6  | [Status Code Pages](#status-code-pages)                              |
| 7  | [Introduction to Testing](#introduction-to-testing)                  |
|    | **Migration**                                                        |
| 8  | [Database Migrations](#database-migrations)                          |
|    | **Scaffolding**                                                      |
| 9  | [Scaffolding, Searching and Sorting](#scaffolding)                   |
| 10 | [Pagingination](#paging)                                             |
|    | **API**                                                              |
| 11 | [Web API](#web-api-1)                                                |
| 12 | [Making a Web Service Call](#making-a-web-service-call)              |
|    | **Security**                                                         |
| 13 | [Authentication and Authorization](#authentication-vs-authorization) |
| 14 | [Role based Authentication](#customizing-the-user)                   |
|    | **Razor**                                                            |
| 15 | [Razor Pages](#razor-pages)                                          |
|    | **Blazor**                                                           |
| 16 | [What is Blazor](#what-is-blazor)                                    |
| 17 | [Blazor Wasm Project Structure](#blazor-wasm-project-structure)      |
| 18 | [Blazor EditForm component](#editform)                               |

# What is ASP?

- Active Server Pages released by Microsoft in 1996
- A technology for generating dynamic web pages
- A mixture of HTML tags, ASP tags and VBscript
- Pages were interpreted at run time and HTML was rendered and delivered to client
- Often referred to as "Classic" ASP
- With any luck, you will never encounter it

## What is ASP.NET?

- A complete redesign, released in 2002
- Advantages over Classic ASP:
  - Makes use of .NET Framework
  - UI/Code separation via code-behind files
  - High performance (code is compiled)
  - Powerful new controls (Data, Client-side validation, and more)
  - Powerful IDE integration
  - Same tools as Windows Forms development

## What is ASP.Net Core?

- Open source, cross platform framework released in 2016, works on Windows, macOS or Linux
- Architected for testability
- Higher performance than ASP.NET
- More "modern" approach to web development
- The future of .NET web development

## ASP.NET Version History

- ASP.NET 1.0 - 2002
- ASP.NET 1.1 - 2003
- ASP.NET 2.0 - 2005
  - Master pages (Templates)
- ASP.NET 3.0 - 2006
- ASP.NET 3.5 - 2007
  - LINQ
  - ASP.NET AJAX
- ASP.NET 4.0 - 2010
- ASP.NET 4.5 - 2012
  - MVC
- ASP.NET 4.6 - 2015
- ~~ASP.NET 5.0~~ ASP.NET Core 1.0 - 2016
- ASP.NET Core 1.1 - 2016
- ASP.NET 4.7 - 2017
- ASP.NET Core 2.0 - 2017
- ASP.NET Core 2.1 - 2018
- ASP.NET Core 2.2 - 2018
- ASP.NET 4.8 - 2019
  - Last version with full .NET Framework
- ASP.NET Core 3.0 - 2019
- ASP.NET Core 3.1 - 2019 - LTS (Long Term Support)
- ASP.NET 5.0 - 2020
- ASP.NET 6.0 - 2021 - LTS
- ASP.NET 7.0 - 2022
- ASP.NET 8.0 - 2023 - LTS

## .NET Standard

- The .NET Standard is a formal specification of .NET APIs that are intended to be available on all .NET implementations
- The motivation behind the .NET Standard is establishing greater uniformity in the .NET ecosystem
- [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

## ASP.NET Framework

- Collection of technologies
- ASP.NET is not a language, it is a framework to develop web applications and web services
- Many supported languages and tools, main languages include C#, VB.NET and JavaScript
- Mature and considered feature complete

## ASP.NET Core

- Newer, portable version of ASP.NET that also runs on Linux and macOS
- Some features of ASP.NET were not brought forward to ASP.NET Core
- WebForms, WCF, and more
- All future development will take place here

## Mono/Xamarin

- Mono is an open-source project to create a .NET Framework-compatible software framework, initially released in 2004
- Mono's goal is to be able to run Microsoft .NET applications cross-platform and bring better development tools to Linux developers
- Mono can be run on Android, most Linux distributions, BSD, macOS, Windows, Solaris, PlayStation, Wii and Xbox
- Mono was initially developed by Ximian
- In 2011 all Ximian staff working on Mono were laid off and formed Xamarin right away
- In 2016 Microsoft acquired Xamarin
- .NET Core is leveraging the work done by Xamarin

## .NET Architecture

![][image1]

## ASP.NET Architecture

![][image2]

## ASP.NET Languages

- ASP.NET supports development in C#, VB.NET, Perl, Python, Ruby, and more.
- C# was developed expressly for .NET
- Even though .NET is multilingual, many consider C# to be its native language

## ASP.NET Programming Models

| Feature                 | .NET Framework | .NET Core |
| :---------------------- | :------------- | :-------- |
| Web Forms               | Yes            | No        |
| MVC                     | Yes            | Yes       |
| Web API                 | Yes            | Yes       |
| SignalR                 | Yes            | Yes       |
| Single Page Application | Yes            | No        |
| Web Application         | No             | Yes       |
| Blazor                  | No             | Yes       |

# Web Forms

- Control and event-based programming model similar to Windows Forms
- Controls encapsulate HTML, JavaScript and CSS
- Rich UI controls included - datagrids, charts, AJAX, and more
- Browser differences handled for you

## MVC

- Model View Controller
- Total control of HTML markup
- Supports unit testing
- Extremely flexible and extensible
- Emerging as preferred development technique

## Web API

- Application Programming Interface (API)
- Representational State Transfer (REST)
- Program to program calls across the Internet
- Allows passing complex objects back and forth
- Content is typically JavaScript Object Notation (JSON), can also be eXtensible Markup Language (XML) or other schemes

## Web Application

- Better known as Razor Pages
- Similar to MVC
  - Total control of HTML markup
  - Supports unit testing
  - Extremely flexible and extensible
- Alternative to MVC

## Blazor

- Blazor is a framework for building interactive client-side web UI with .NET
- Blazor depends on WebAssembly, supported by most major browsers
  - Create rich interactive UIs using C# instead of JavaScript
  - Render the UI as HTML and CSS for wide browser support, including mobile browsers

## Security

- Normally, most websites should use HTTPS
- ASP.NET is fully capable of supporting HTTPS

## Web Forms

- ASP.NET uses the concept of the "web form", similar to Windows Forms
- A web form is made up of two files
- Filename.aspx (markup)
- Filename.aspx.cs (code-behind)
- The ASP.NET engine renders standards-based HTML and JavaScript code
- Startup page is Default.aspx

## Three Types of Markup

| Type              | Example                                                    | Comment                                                                                           |
| :---------------- | :--------------------------------------------------------- | :------------------------------------------------------------------------------------------------ |
| Standard HTML     | \<h1\>Heading\</h1\>                                       |                                                                                                   |
| Inline expression | \<%: Title %\>                                             | From classic ASP: Execute statements, Call functions, Access properties, Directives, Data-binding |
| Web form control  | \<asp:Button ID="Button1" runat="server" Text="Button" /\> | Can be manipulated in code-behind files.                                                          |

## Web Form Controls

- Standard: Buttons, text boxes, labels, ...
- Rich: Calendars, wizards, ...
- Validation: Client-side validation
- Data: GridView, Repeater, ...
- Many more...

## Event Model

- Page\_Load routine used for initializations
- Buttons have click events just like in windows forms
- Programming intuitive and very similar to windows forms

## Page Events

1. PreInit
2. Init
3. InitComplete
4. PreLoad
5. Load
6. LoadComplete
7. Control Events
8. PreRender
9. PreRenderComplete
10. SaveStateComplete
11. Unload

## Creating a WebForm

- Type on design surface and drag controls onto it
- Or edit in Source mode
- Controls can also be dragged into Source mode

## Add an Event Handler

- Double click a button and write code
- Just like Windows Form programming
- Or wire up your own event handlers to handle events from multiple controls

## State

- ASP.NET simplifies web programming by automatically maintaining state on page when posting back to the same page
  - Known as a postback
- Most other web development environments force you to manage state yourself
  - Non-trivial
- State is not stored on the server
  - Good for scalability
  - Implemented using a hidden HTML field: \_\_VIEWSTATE

## Master Pages

- When creating a site with many pages, master pages provides an easy way to control the look and feel of all pages
- A master page is a "template" that will frame how all pages on the site look
- Master pages can contain controls and code like any other ASP.NET page
- A web site may have multiple master pages
- A style sheet can be linked to any page, but linking to a master page causes its effect to spread to many pages

## Content Placeholders

- `ContentPlaceHolders` define regions for content in an ASP.NET master page
- This is where content will show up on actual pages
- `ContentPlaceHolders` should be empty on the master page
- `ContentPlaceHolders` will contain HTML, controls, etc. on content pages

## SqlDataSource

- The `SqlDataSource` data source control represents data in an SQL relational database to data-bound controls
- Can be connected to a variety of controls including the `GridView` and `DropDownList`
- Uses a connection string to connect to the database

## Connection Strings

- Connection strings should be stored in web.config
  - More secure
  - If it needs to change, only one place
  - Better performance due to connection pooling
  - Default behavior
- SQL Server connection strings can be specified in a variety of ways, the most common approach has three required parts:
  - Data Source (where is the database server)
  - Initial Catalog (which database is being used)
  - Authentication Credentials (how does the application login to the database)

## GridView

- A GridView renders as a table to the end user
- Like many ASP.NET controls, clicking \> will render a task window
- Selecting Edit Columns... task will allow control of what columns are displayed and how they are displayed

## GridView Columns

- Common DataFormatString values
  - {0:c} Currency
  - {0:d} Short date
  - {0:n2} Numeric with 2 decimal places
- To right justify numeric columns
  - Expand ItemStyle
  - Set HorizontalAlign to Right

## GridView Tasks

- Paging
- Sorting
- Editing
  - Data source must support this by including primary key
- Deleting

# ASP.NET Core Application Model

- An ASP.NET Core web application is actually an .NET console application
- The console application starts up an instance of an ASP.NET Core web server
- Microsoft provides a default, cross-platform web server named Kestrel
- The web application logic is run by Kestrel

## Console Application Entry Point

- The Main (case sensitive) method is the entry point of a C# application
- It must return either nothing (void) or an int
- It may optionally accept a string array of command line arguments

## Kestrel

- An ASP.NET Core app runs with an in-process HTTP server implementation
- The server implementation listens for HTTP requests and surfaces them to the app as a set of request features composed into an HttpContext
- Microsoft is working hard to make Kestrel fast [TechEmpower Web Framework Benchmark](https://www.techempower.com/benchmarks/#section=data-r21&hw=ph&test=composite)
- Kestrel can be used by itself as an edge server processing requests directly from a network, including the Internet

![][image3]

- Kestrel can also be used with a reverse proxy server, such as Internet Information Services (IIS), Nginx, or Apache
- A reverse proxy server receives HTTP requests from the Internet and forwards them to Kestrel

![][image4]

## NuGet Package Manager

- Packages are compiled code that we add to our projects to increase functionality
- Many are written by Microsoft, many are written by 3rd parties
- You can see what packages are installed by right-clicking the project and selecting Manage NuGet Packages, you can also search for and install new packages from the browse tab

## Package Manager Console

- If the name of the package is known, it can also be installed from the Package Manager Console (**PMC**) using the Install-Package command
- The PMC is accessed from the Tools / NuGet Package Manager menu
- The PMC also has a variety of commands for working with the project, databases and more

## HttpContext

- Encapsulates all HTTP-specific information about an individual HTTP request such as:
  - Request, Response, Server, Session, Item, Cache, User's information like authentication and authorization and much more
- Every HTTP request creates a new object of HttpContext with current information

## ASP.NET Core Host

- ASP.NET Core apps configure and launch a host
- The host is responsible for app startup and lifetime management
- At a minimum, the host configures a server and a request processing pipeline
- The host can also set up logging, dependency injection, configuration and much more

## CreateDefaultBuilder

- The CreateDefaultBuilder method of the Host class is responsible for the following:
  - Configures Kestrel server as the web server
  - Sets the content root path
  - Loads host configuration
  - Configured logging
  - And more

## Startup.cs

- The Startup class typically has two methods ConfigureServices and Configure
- ConfigureServices is an optional method called before Configure and sets up classes for dependency injection such as a database context, identity and many more
- Configure specifies the middleware pipeline

## Middleware

- Middleware is code that gets called in between an initial request and the response to the client
- Each component:
  - Chooses whether to pass the request to the next component in the pipeline
  - Can perform work before and after the next component in the pipeline

![][image5]

## ASP.NET Core Middleware

- Middleware delegates are configured with Run, Map and Use methods
  - Run adds a terminal (or final) middleware delegate to the application's request pipeline
  - Map branches the request pipeline based on matches of the given request path
  - Use adds a middleware delegate defined in-line to the application's request pipeline

# Model View Controller

- **MVC** is a design pattern
- **Model** contains domain classes and business logic
- **View** displays models to the user using HTML, JavaScript, CSS, etc.
- **Controller** handles user input, builds the model and passes it to the view to display the model

## Standard MVC Architecture

1. Incoming request directed to **Controller**
2. **Controller** processes a request and forms a data **Model**
3. **Model** is passed to **View**
4. **View** transforms **Model** into an appropriate output format
5. Response is rendered

![][image6]

## ASP.NET MVC

- Provides another approach to ASP.NET development instead of Web Forms with the following benefits:
  - Available in both .NET Framework and .NET Core
  - Improved testability (ideal for TDD \- Test Driven Development)
  - Removes ViewState (\_\_VIEWSTATE) and it's overhead
  - Complete control over HTML markup
  - Better separation of concerns
  - Embraces web standards (HTML, JavaScript, CSS)
  - Open Source code available for modifications

## ASP.NET Core MVC Applications

- ASP.NET Core MVC applications are Console applications which start in the Main method of the Program class
- Startup code is placed in the Startup class. It configures:
  - Request Pipeline
  - Default Route
  - Logging
  - And more

## ASP.NET MVC Routing

- URLs in MVC do not map to a specific file but instead to a specific controller action
- Routing Engine parses URL, looks up the corresponding controller action and directs the request to it
- The action corresponds to a public method in the specified controller
- A Map Route provides a name, URL pattern and default values
- Configuration of the mapping happens in the Configure method of the Startup class
- Default Route: {controller=Home}/{action=Index}/{id?}
  - HomeController is the default controller
  - Index is the default action method
  - The id parameter is optional
- By default URLs map to controller/action/id
- https://my.shopping.app/Products/Electronics/5 would:
  - Instantiate an instance of the Products controller
  - Invoke the Register method of the controller
  - Pass the id parameter set to 5
- Provides clear and simple URLs which is great for Search Engine Optimization (SEO)
- HomeController
  - public Index()
- OtherController - public Index()
  - public Post(id)

| Controller | Action | Route            |
| :--------- | :----- | :--------------- |
| Home       | Index  | /home/index      |
| Other      | Index  | /other/index     |
| Other      | Post   | /other/post/{id} |

- Pattern: {controller=Other}/{action=Post}/{id?}
- Url: /other/post/1
  - Controller: Other
  - Action: Post
  - ID: 1
- Pattern: {controller=Other}/{action=Post}/{id?}
- Url: /other/post
  - Controller: Other
  - Action: Post
  - ID: null
- Pattern: {controller=Other}/{action=Index}/{id?}
- Url: /other
  - Controller: Other
  - Action: Index
  - ID: null
- Pattern: {controller=Home}/{action=Index}/{id?}
- Url: /
  - Controller: Home
  - Action: Index
  - ID: null

## Passing Data from Controllers to Views - Part 1

- ViewData
  - A dynamic dictionary object (key/value pairs) that can be passed from a controller to a view
  - It can contain nearly any type
- ViewBag is a wrapper for ViewData
- The two can be used interchangeably

## Route Customization

- Any controller method can be customized to respond to a different route pattern than the default
- A Route attribute is placed before the method to indicate the pattern

## Route Constraints

- Route constraints can be placed on the route place holders to limit the URLs that the route will match
- **Example:** {controller=Home}/{action=Index}/{id:int?} indicates that the id parameter must be an integer, but it is still optional
- A few other route constraints: double, datetime, min(value), range(min, max)
- These constraints should not be used for input validation

## ASP.NET MVC View

- Responsible for displaying the model to the user
- cshtml files are used and can be considered templates with static html and C# code (prefix with @ symbol) for dynamically inserting the model's properties
- Use Razor View Engine to display the View

## Razor Syntax

- Razor is a markup syntax for embedding server-based code into web pages
- The Razor syntax consists of Razor markup, C#, and HTML
- Consists of implicit Razor expressions, explicit Razor expressions and Razor code blocks
- @@ to escape the @ symbol
- Comment Syntax: @\* comment \*@
- @ Implicit Razor expression
  - \<p\>Today: @DateTime.Now\</p\>
  - Renders as HTML, spaces not allowed (_Except when used with await_)
- @() Explicit Razor expression
  - \<p\>Week ago: @(DateTime.Now \- TimeSpan.FromDays(7))\</p\>
  - Renders as HTML, spaces allowed
- @{} Razor code block
- Doesn't render as HTML, semicolons required @{ var name \= "Bob Loblaw"; var len \= name.Length; }

- Then in the same file:

\<p\>The length of @name is @len bytes.\</p\>

## IActionResult

- The most common return type for Controller Actions is IActionResult
- It is an interface that provides for a very flexible set of possible return types including
- ViewResult, ContentResult, RedirectResult, JsonResult and many more

## URL Helpers

- Simplifies HTML coding in Views by using metadata and annotations
- @Url.Action("Action", "Controller")
- Many more

## Layout

- Special Razor pages that help define the layout of the overall site
- Similar to Master Pages in Web Forms
- By convention stored in a folder named Shared
- @RenderBody and @RenderSection are placeholders for content on individual pages

## Passing Data from Controllers to Views - Part 2

- A Model property
  - Typically a class
  - Cleaner and clearer
  - Support IntelliSense

## Project Structure \- Folders

- Properties contains launchSettings.json file
- launchSettings.json contains some environment settings including which IP ports to use during development
- wwwroot contains static files, such as HTML, CSS, images and JavaScript
- Controllers contains the MVC controllers
- Models contains the model classes for the app
- Views contains a folder for each controller, which contains the views for that controller
- Views also contains a shared folder that contains views used throughout the app
- Views also contains \_ViewImports.cshtml and \_ViewStart.cshtml which are used to help structure all views
- appsettings.json is the project configuration file which contains settings related to logging, connection strings and other items
- Program.cs is the file which starts the app
- Startup.cs configures the app's services and request processing pipeline

## Data Annotations

- Data Annotations are rules applied to model properties
- MVC will take care of enforcing them and displaying appropriate messages to users
- Selected annotations: \[Key\], \[Required\], \[Range\], \[Display\], \[DataType\]
- [View complete list of annotations at docs.microsoft.com](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-5.0)

## MVC Scaffolding

- MVC Scaffolding uses a Wizard that can automatically generate much of the standard code for our Controllers and Views
- VS Scaffolding is a great example of Code Generation tools

## HTML Helpers

- Similar to URL helpers
- @Html.DisplayNameFor - plain text to display the read-only field name
- @Html.DisplayFor - figures out the best HTML5 for read-only display of the actual field value
- @Html.ActionLink - action link with anchor tag

## MVC Controller Actions

- Typically a controller will have the following actions (or methods)
  - Index which is the "first" page that shows a list of any existing items, and allows access to the other actions
  - Details which shows one item in full
  - Create which allows for the creation of a new item
  - Edit which allows for the editing of an existing item
  - Delete which allows for the deletion of an existing item
- The Create, Edit and Delete actions are comprised of two methods each, a get and a post

## Controller Method Attributes

- **HttpPost**
  - Typically two controller actions are required for modifying data (edit, create, delete)
  - One controller action for the HTTP GET displays the blank form (or the existing data)
  - The other controller actions for the HTTP POST validates the data and updates the data store if necessary
- **ValidateAntiForgeryToken**
  - Use for HTTP POST to ensure that the data is originating from the correct user to stop Cross-Site Request Forgery (CSRF) attacks

## Model Binding

- Request fields are mapped to controller action parameters automatically
- Can be almost any data type including objects
- Parameters can be automatically validated, if any errors are found they are added to the ModelState
- Sources of model binding values...
  - Form post values
  - Route data / URL parameters
  - QueryString parameters
  - User cookies

## ModelState

- When updating the model, the ModelState property (of the controller) indicates if the model is valid
- The IsValid property checks all properties of the model to ensure correct data type, appropriate values and any other specified attributes are correct
- There are other useful properties, such as ErrorCount, Keys, Values, etc.

## Tag Helpers

- Similar to HTML Helpers but rely on attributes for well known HTML tags
- Makes views look more like standard HTML
- Easier to read for HTML developers \<label asp-for\="DateSeen" class\="control-label"\>\</label\> \<input asp-for\="DateSeen" class\="form-control" /\> \<span asp-validation-for\="DateSeen" class\="text-danger"\>\</span\>

# Introduction to ORM (Object-Relational Mapping)

- There are major differences between relational and object oriented models including:
  - **Types** (e.g. varchar vs. string)
  - **Identity** (keys vs. address)
  - **Relations** (foreign keys vs. references)
  - **Many to Many** relations require linking table in SQL
  - **Inheritance** and **Polymorphism** are not directly supported in SQL
- **Object/Relational Mapping** (ORM or O/RM) maps between these two models allowing us to work in the object oriented model without having to worry about the underlying data structure
- ORM tools allow us to easily save our objects to the database and load them from the database
- ORMs typically provide support for the full set of CRUD operations
- Why would one use an ORM?
  - To save time and money (time to market)
  - To focus on the business logic rather than database/persistence logic

## Introduction to .NET ORM

- First major ORM for .NET was **NHibernate** which is an open source solution which is still around
- .NET provided LINQ to SQL as its first ORM
- .NET now includes the **Entity Framework** which effectively replaced **LINQ** to **SQL**
- **Entity Framework** can be used with many different databases including _MySQL_, _Oracle_, _SQL Server_, _SQLite_ and more
- Complete list of [EF Core Database Providers](https://docs.microsoft.com/en-us/ef/core/providers/index?tabs=dotnet-core-cli)
- [Entity Framework alternatives](https://stackshare.io/entity-framework/alternatives)

## Entity Framework

- Microsoft's latest ORMs are **Entity Framework 6** (EF6) and the lighter, portable **Entity Framework Core 5** (EF Core)
- **EF6** is more mature and slightly richer in functionality than **EF Core**
- However, just like .NET Framework, future development will primarily be with **EF Core**
- Good tutorial site: [EntityFrameworkTutorial](https://www.entityframeworktutorial.net/)

## Performance Considerations

- In general **EF** generated database access code performs very well, although perhaps not quite as well as native SQL and stored procedures
- For the vast majority of applications, **EF** performance will be more than adequate
- On the occasion when **EF** performance is the bottleneck, it is still possible to write native SQL, stored procedures, views and indexes and configure **EF** to use them

## EF Workflows

- **EF Core** has two workflows and the choice would be depend on the situation
  - **Code** first can be used for a **new** application
  - **Database** first makes sense if a database already exists
- **EF6** also has a **Model** first workflow that is similar to **Code** first, but uses a _graphical editor_

## Important Classes

- Main Context container class provides access to all of our entities
  - Derives from DbContext
  - Uses DbContextOptions for configuration information
    - **Database provider**: UseSqlServer, UseSqlite, etc.
    - **Connection string**
  - Contains DbSet\<T\> members for our strongly typed entities (AKA tables)
  - Provides support for change tracking to our entities
- Entities with the DbContext API are **POCO** (Plain Old CLR Objects) and therefore are persistence ignorant

## MVC EF Scaffolding

- **Scaffolding** in MVC can generate most of our standard CRUD code automatically from a model
- **Scaffolding** will install necessary packages, build the **Controller** and **Views**, register the database and configure its connection

## Asynchronous Programming

- Makes heavy use of asynchronous code, lambda expressions and interfaces
- To improve efficiency, the scaffolder makes extensive use of [asynchronous programming](https://docs.microsoft.com/en-us/dotnet/csharp/async) which is helpful for I/O bound code such as file, database and network I/O
- Two C# keywords to note for asynchronous code:
  - **async:** marking a method as making an asynchronous call
  - **await:** relinquishes the current thread to the thread pool while the awaited (asynchronous) task operation completes
- Methods that use async must also return either a:
  - Task\<return type\> \- generic object for methods that would otherwise return a plain return type e.g. Task\<string\> instead of string
  - Task - object for methods that would otherwise return void
- Methods that do the asynchronous I/O bound processing by convention have method names that end in Async

## Dependency Injection

- **Dependency Injection** (DI) is a design pattern
- A dependency is an object that another object depends on (e.g. EF Data Context)
- ASP.NET Core MVC controllers request dependencies via constructors
- DI makes apps easier to test and maintain because they are more **loosely coupled**

## Ensuring the Database Exists

- The EnsureCreated method of the Database property of the DatabaseContext ensures that the database for the context exists
- If it exists, no action is taken
- If it does not exist then the database and all its schema are created
- Optionally, the OnModelCreating method can be implemented to seed the database when it is being created

## Entity Framework

- Changes to entities are tracked automatically by the context
- We can easily save all changes to our entities using the SaveChanges method of our DbContext derived context
- We can query our entities using **LINQ** to Entities using **Query Syntax** or **Method Syntax**

## Connection Strings

- **Connection strings** in .NET Core are the same as in .NET Framework
- Instead of the web.config file, they are stored in appsettings.json

## SQLite

- **SQLite** is a small, fast, self-contained, high-reliability, full-featured, SQL database engine
- **SQLite** is the most used database engine in the world
- **SQLite** is built into all mobile phones and most computers and comes bundled inside countless other applications
- [SQLite](https://www.sqlite.org/)

# Status Code Pages

- By default, an ASP.NET Core app doesn't provide a status code page for HTTP error status codes, such as 404 \- Not Found
- When the app encounters an HTTP 400-599 error status code that doesn't have a body, it returns the status code and an empty response body
- To provide status code pages, use the status code pages middleware

## \_ViewStart.cshtml

- Code that needs to run before each view or page should be placed in the \_ViewStart.cshtml file
- By convention, this file is located in the Views folder
- The statements listed are run before every full view

## \_ViewImports.cshtml

- Views and Pages can use Razor directives to import namespaces and use dependency injection
- Directives shared by many views may be specified in a common \_ViewImports.cshtml file

## Hierarchy

- \_ViewImports.cshtml, \_ViewStart.cshtml are both hierarchical
- If a \_ViewStart.cshtml (or \_ViewImports.cshtml) file is defined in a view or folder, it will be run after the one defined in the root of the Views folder

## Custom Validation

- Recall the list of [possible annotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-5.0)
- To be consistent with the DRY (Don't Repeat Yourself) principle, validations should be specified in the Model
- The custom validation class inherits from ValidationAttribute and overrides the IsValid method

# Introduction to Testing

- Testing is a key part of developing any professional application
- Testing has traditionally been a manual, tedious and expensive process
- Agile development methodologies place even greater emphasis on testing than traditional approaches
- Agile development requires lots of repeat (regression) testing so the need for test automation is very high
- Visual Studio has extensive support for building and executing automated tests
- ASP.NET MVC, Razor Pages and Web API were designed to be very testable

## Testing Frameworks

- Visual Studio provides an open testing architecture that supports several different test frameworks including
  - **Nunit** - Original JUnit port
  - **xUnit** - Updated version of **Nunit**; Emerging as preferred testing tool
  - **MSTest** - Microsoft's unit testing framework
- The principles for these unit testing frameworks are very similar, so transitioning from one to the other is straightforward

## xUnit

- Test actions (methods) are annotated with \[Fact\] or \[Theory\]
- Facts are tests which are always true
- Theories are test which are only true for a particular set of data
- Initialization code that runs before each test is placed in the test class constructor

## xUnit Instructions

- Add a Test Project to your solution
- Add a reference to the project you would like to test to your test project
- Implement your unit test
- Build your unit test project
- Open Test Explorer and Run the test
- Review the results and modify as necessary

## Unit Testing Web Controllers

- Automated testing depends on known data
- Testing against an actual database is problematic because the contents of the database are expected to change over time
- A good alternative is to use a **fake** database
- Microsoft.EntityFrameworkCore.InMemory NuGet package provides an in memory simulation of the database context

## Naming Tests

- By convention, the name of a test should consist of three parts:
- The _name_ of the method being tested
- The _scenario_ under which it's being tested
- The _expected behavior_ when the _scenario_ is invoked
- Examples: Index\_NoInput\_ReturnsMovies or Details\_MovieId\_ReturnsMovie

## Unit Tests

- Unit Tests normally consists of three major steps:
  - **Arrange** - create and initializes the object(s) to test
  - **Act** - do something with the object(s)
  - **Assert** - check that the object(s) or result changes to the expected state

## Test Explorer

- Test Explorer is Microsoft's dedicated Test Tool
- Test Explorer provides support for running tests
- Part of Visual Studio

## Assert

- The Assert class has many useful methods for assertions including:
  - IsType\<T\>
  - True, False
  - Equal, NotEqual
  - Same, NotSame (to compare references)
  - Null, NotNull

## Order of Tests

- Tests are not guaranteed to run in any particular order
- It is best practice that test shouldn't be ordered
- If it is required to have tests run in a particular order, you must implement the ITestCaseOrderer or ITestCollectionOrder interfaces

## Live Unit Testing

- As you are developing an application, Live Unit Testing automatically runs any impacted unit tests in the background and presents the results and code coverage live in the Visual Studio IDE in real time
- As you modify your code, Live Unit Testing provides feedback on how your changes impacted existing tests and whether the new code you've added is covered by one or more existing tests
- Only Visual Studio Enterprise supports this
- This will gently remind you to write unit tests as you are making bug fixes or adding new features
- ✅ Code passes test(s)
- ❌ Code fails test(s)
- ➖ Code isn't covered by a test(s)

# Database Migrations

- A data model changes during development and gets out of sync with the database
- You can drop the database and let **EF** create a new one that matches the model, but this procedure results in the loss of data
- The migrations feature in **EF Core** provides a way to incrementally update the database schema to keep it in sync with the application's data model while preserving existing data in the database
- Database migrations are handled through the **Package Manager Console** with two commands:
  - **Add-Migration**: scaffolds a migration script for any pending model changes
  - **Update-Database**: applies any pending migrations to the database

## Maximum Length

- Configuring a maximum length provides a hint to the database provider about the appropriate column data type to choose for a given property
- Maximum length only applies to array data types, such as string and byte\[\]
- Avoiding varchar(max) when possible, will produce more **efficient SQL**

## Relating Entities

- **Navigation property**: a property defined on the parent and/or child entity that contains a reference(s) to the other entity(s)
  - **Collection navigation property**: navigation property that contains references to many related entities
  - **Reference navigation property**: navigation property that holds a reference to a single related entity
- A List\<T\> can be used on the **parent** entity
- A ForeignKey attribute is used on the **child** entity

## Loading Related Data

- With navigation properties, **EF Core** allows the model to load related entities
- There are three common O/RM patterns used to load related data:
  - _Eager_ loading (achieved with Include method)
  - _Explicit_ loading
  - _Lazy_ loading (_default_)
- **Eager** loading means that the related data is loaded from the database as part of the initial query
- **Explicit** loading means that the related data is explicitly loaded from the database at a later time
- **Lazy** loading means that the related data is transparently loaded from the database when the navigation property is accessed

# Scaffolding

- The scaffolder can reverse engineer a model from an existing database by using the Scaffold-DbContext command from the **Package Manager Console**
- This is the **database first** workflow

## Scaffold-DbContext

### \-Connection \<String\>

- The connection string to the database
- The value can be name=\<name of connection string\>
- In that case the name comes from the configuration sources that are set up for the project (appsettings.json)
- This is a positional parameter and is required

### \-Provider \<String\>

- The provider to use
- Typically this is the name of the **NuGet** package, e.g. Microsoft.EntityFrameworkCore.SqlServer
- This is a positional parameter and is required

### \-OutputDir \<String\>

- The directory to put files in
- Paths are relative to the project directory

### \-ContextDir \<String\>

- The directory to put the DbContext file in
- Paths are relative to the project directory

### \-Context \<String\>

- The name of the DbContext class to generate

### \-Schemas \<String\[\]\>

- The schemas of tables to generate entity types for
- If this parameter is omitted, all schemas are included

### \-Tables \<String\[\]\>

- The tables to generate entity types for
- If this parameter is omitted, all tables are included

### \-DataAnnotations

- Use attributes to configure the model (where possible)
- If this parameter is omitted, only the fluent API is used

### \-UseDatabaseNames

- Use table and column names exactly as they appear in the database
- If this parameter is omitted, database names are changed to more closely conform to C# name style conventions

### \-Force

- Overwrite existing files

## Data Annotations vs. Fluent API

- Data Annotations example \[Required\] \[StringLength(160)\] public string Title { get; set; }

- Fluent API is an alternative way to configure the data model; entity.Property(e \=\> e.Title) .IsRequired() .HasMaxLength(160);

## Running

- Visual Studio offers 3 ways to run your site
- **F5** offers full debugging which allows interactive breakpoints
- **Ctrl+F5** runs without debugging, which runs slightly faster
- Both approaches permit making changes and refreshing browser without relaunching
- The 3rd way is to use the dotnet command in the **Package Manager Console** (PMC)
- dotnet watch run \--project projectname
- dotnet watch will automatically refresh the browser whenever a file change is detected in the project

## No Tracking

- The AsNoTracking method returns a new query where the change tracker will not track any of the entities that are returned
- If the entity instances are modified, this will not be detected by the change tracker and SaveChanges() will not persist those changes to the database
- Disabling change tracking is useful for _read-only_ scenarios because it avoids the overhead of setting up change tracking for each entity instance
- You should not disable change tracking if you want to manipulate entity instances and persist those changes to the database using SaveChanges()

## Form Tag Helpers

- Tag Helpers enable _server-side_ code to participate in creating and rendering HTML elements in Razor files
- The asp-route-\<Parameter Name\> attribute, where \<Parameter Name\> is added to the route values

# Paging

- If the Index method of a controller returns many items, unlike Web Forms, ASP.NET Core MVC does't have a built-in ability to paginate them
- A PaginatedList class will be used to implement this functionality

## Generic Type Parameter

- In a generic type or method definition, a type parameter is a placeholder for a specific type that a client specifies when they create an instance of the generic type
- This allows the method to be flexible and useful in a variety of scenarios
- In the method signature, it is denoted as \<T\>, meaning any type

## IQueryable Interface

- Provides functionality to evaluate queries against a specific data source wherein the type of the data is known
- Similar to IEnumberable in that it allows the creation (and consumption) of a list of stuff, but from a **LINQ** source
- This interface itself implements IEnumerable

## LINQ

- **L**anguage **IN**tegrated **Q**uery is a query syntax in C# that allows retrieving data from a variety of data sources
  - Object collections
  - ADO.NET DataSets
  - XML documents
  - Entity Framework
  - SQL databases
  - Other data sources

## LINQ Methods

- Skip bypasses a specified number of elements in a sequence and then returns the remaining elements
- Take returns a specified number of contiguous elements from the start of a sequence
- FirstOrDefault returns the first element of a sequence, or a default value if no element is found

# Web API

- Used for building **HTTP** Web Services
- Great for **RESTful** services
- Methods return raw data which will automatically be converted into either **JSON** or **XML**

## Web API Usage

**![][image7]**

## Web Services

- Web Services allow computer to computer **function calls** over the Internet
- Allow you to create/use best of breed or authoritative services from experts (credit card validators, weather reports, etc.)
- Two main approaches
- **REST** - Representational State Transfer
- **SOAP** - Simple Object Access Protocol
- _Cross-platform_ communication (OS, language)
- This was a big problem in the past
- Security administrators have had to close many ports
- Web services use **80 (http)** and **443 (https)**
- Using **REST**, web services can pass complex objects back and forth

## Web API Controllers

- Controllers inherit from Microsoft.AspNetCore.Mvc.ControllerBase
- A base class for an MVC controller without view support
- Method names match HTTP verbs (**Get**, **Put**, **Post**, **Delete**)
- ASP.NET Routing maps URIs and HTTP Verbs to Controller actions (methods)
- This default routing can be overridden by annotating method names with attributes such as **\[HttpGet\]**, **\[HttpPost\]**, etc
- Method parameters are automatically bound to the request's parameters by the _Model Binder_
- Actions must be public, can't be static, no ref or out parameters

## Web API Routing

- Not specified in Startup.cs
- Specified in controller itself
- Uses Route attribute

## HTTP Verbs

| Verb   | Meaning                         |
| :----- | :------------------------------ |
| GET    | Retrieve existing data          |
| PUT    | Update or replace existing data |
| POST   | Add new data                    |
| DELETE | Remove data                     |

## Common HTTP Status Codes Groups

| Group | Meaning         |
| :---- | :-------------- |
| 2xx   | (Successful)    |
| 3xx   | (Redirected)    |
| 4xx   | (Request error) |
| 5xx   | (Server error)  |

## Common HTTP Status Codes

| Code | Reason         | Meaning                                                |
| :--- | :------------- | :----------------------------------------------------- |
| 200  | OK             | Success (Use for GET response)                         |
| 201  | Created        | Used on POST request when creating a new resource      |
| 204  | No Content     | Used for DELETE or PUT response                        |
| 400  | Bad Request    | Invalid Request                                        |
| 401  | Unauthorized   | Authentication                                         |
| 403  | Forbidden      | Authorization                                          |
| 404  | Not Found      | entity does not exist                                  |
| 406  | Not Acceptable | bad params                                             |
| 409  | Conflict       | For POST / PUT requests if the resource already exists |
| 500  | Internal       | Server Error                                           |
| 503  | Service        | Unavailable                                            |

See [HTTP Status Codes \- Wikipedia](https://en.wikipedia.org/wiki/List_of_HTTP_status_codes) for more details.

## Model Binding

- Maps incoming data from the HTTP Body and/or query string (URI) to method parameters
- MediaTypeFormatters transform input and output data to/from .NET objects
- Transforms supplied data (even if it is JSON, XML or Form data) to the parameters

## Returning Results from Controller Actions

- Often the return type for _Web API Controllers_ is an IActionResult as it is for standard _MVC Controllers_
- The base controller class defines several useful methods for returning the most common status codes

## Return Status Codes

| MVC Method       | Code | Meaning                                                            |
| :--------------- | :--- | :----------------------------------------------------------------- |
| Ok()             | 200  | GET response                                                       |
| Created()        | 201  | POST response                                                      |
| CreatedAtRoute() | 201  | POST response, along with the new location and data for the object |
| NoContent()      | 204  | DELETE or PUT response                                             |
| BadRequest()     | 400  | General nonspecific request error                                  |
| Unauthorized()   | 401  | User lacks permissions for request                                 |
| NotFound()       | 404  | GET request with invalid Id                                        |

## Testing Web API Services

- For testing/sniffing Web API there are many options:
- [Fiddler](https://www.telerik.com/fiddler)
- [Advanced REST Client](https://install.advancedrestclient.com/install)
- [Swagger](https://swagger.io/)
- Developer Tools in most browsers (F12) (monitoring only)
- Writing client test code in .NET or JavaScript

## Web API Documentation

- Your partners will need to know how to use your web service
- Help documentation can be generated using [Swagger](https://swagger.io/), also known as [OpenAPI](https://www.openapis.org/)
- **Swagger** can also be used for testing
- **Swashbuckler** is an ASP.NET Core implementation of **Swagger**

## Unit Testing Web API Controllers

- Very similar to unit testing of _MVC Controllers_:
- Create a Test Project
- Add Microsoft.EntityFrameworkCore.InMemory package
- Use the constructor to initialize the test database
- Write test methods to test the actions of the _Web API Controller_

# Making a Web Service call

- Add a model class for the data that the web service is expected to return
- Add a controller or controller action to your MVC project as necessary
- In the controller action using the HttpClient to call the remote service asynchronously
- If successful, return the results

## Parsing JSON

- Parsing JavaScript Object Notation (JSON) manually is tedious
- System.Text.Json provides capabilities to process JSON
  - **Serializing** objects to JSON text
  - **Deserializing** JSON text to objects
- Makes parsing JSON easy

# Authentication vs. Authorization

- Authentication is a process in which a user provides credentials that are then compared to those previously stored in an operating system, database, app or resource
- If they match, the user authenticates successfully
- Once authenticated a user can then perform actions that they're authorized for
- The authorization refers to the process that determines what a user is allowed to do

## Encryption vs. Hashing

- Sensitive data (like passwords) should never be stored in clear text in a database
- Two approaches: **Encryption** or **Hashing**
- An **encrypted** value can be **decrypted** back into its original clear text value
- **Hashing** is a one way process, a hash value can not be reconstituted back into its original text
- **Encryption** is not secure for long term storage
- It is secure for short term situations (HTTPS sessions)
- **Hashing** (with **salt**) is more secure and is used by ASP.NET Core **Identity**
- [https://codebeautify.org/encrypt-decrypt](https://codebeautify.org/encrypt-decrypt)
  - Enter a key (random string for scrambling and unscrambling data)
  - Encrypt some short text
  - Encrypt some longer text, notice the difference
  - Encrypt password, then decrypt it
- [https://codebeautify.org/sha256-hash-generator](https://codebeautify.org/sha256-hash-generator)
  - Hash some short text
  - Hash some longer text, notice the output is same length
  - Hash using the **sha512** algorithm, notice output is longer

## How Not to Store Passwords\!

- Adobe password crossword [http://zed0.co.uk/crossword](http://zed0.co.uk/crossword)

## Open Web Application Security Project (OWASP)

- Best practices related to security
- [https://www.owasp.org](https://www.owasp.org/)
- [Password Storage Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html)
- [Forgot Password Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Forgot_Password_Cheat_Sheet.html)
- Many more...

## ASP.NET Core Identity

- ASP.NET Core **Identity** is a membership system that adds login functionality to ASP.NET Core apps
- Users can create an account with the login information stored in **Identity** or they can use an **external** login provider
- Supported external login providers include **Facebook**, **Google**, **Microsoft** Account and **Twitter**
- **Identity** can be configured using a **SQL Server** database (or any other database) to store user names, passwords, and profile data
- Alternatively, another persistent store can be used, for example, **Azure Table Storage**

## General Data Protection Regulation (GDPR)

- In 2018, the _European Union_ introduced the GDPR
- ASP.NET Core **Identity** is compliant with GDPR
- One of the stipulations of GDPR is that users can manage the information that an enterprise holds about them

## Authorization

- Once authenticated, authorization can be handled
- A method or a class can be **decorated** with the \[Authorize\] or \[AllowAnonymous\] attributes
- If a method is **decorated** with \[Authorize\], the user must be authenticated to access it
- If a class is **decorated** with \[Authorize\], all **methods** require authentication, unless decorated with \[AllowAnonymous\]

## ASP.NET Core Identity

- When ASP.NET Core **Identity** is added to a project, it is added as a _Razor Class Library_ (RCL)
- A _Razor Class Library_ contains Razor views, pages, controllers, page models, Razor components, View components and data models

## Customizing ASP.NET Core Identity

- ASP.NET Core **Identity** has many points of customization
- IdentityOptions allows configuring password, lockout, user options and more
- In order to customize one of the provided endpoints, the identity scaffolder must be used to generate the appropriate code

## Select Identity RCL Endpoints

- /Identity/Account/Login
- /Identity/Account/Logout
- /Identity/Account/Manage
- /Identity/Account/Register
- Many more...

# Customizing the User

- The user object in **Core Identity** is based on the [IdentityUser](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.identityuser?view=aspnetcore-5.0) class
- To customize the user, a new model is created that _inherits_ from IdentityUser
- By convention, this new model should be named ApplicationUser
- **Decorating** any new properties with \[PersonalData\] ensures that they are downloaded from the **PersonalData** page
- IdentityUser will have been used throughout the project and will need to be updated to ApplicationUser

## UserManager Class

- Provides the APIs for managing user in a persistence store
- Using the [UserManager](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.usermanager-1?view=aspnetcore-3.1) instead of interacting with the database directly allows less impact if the persistence store should ever change

## Role Based Security

- In an enterprise of any size, there will be a variety of employees performing a variety of tasks
- People change jobs, get promoted, leave the company on an ongoing basis
- Considering these changes, security best practice is to assign security privileges based on role
- Each enterprise would define roles that made sense for it, e.g.
  - Admin, Manager, Clerk, Trainee
  - Executive, Manager, Support, Faculty, Student
  - Admin, Full time employee, Part time employee
- It is possible for an employee to participate in more than one role, for example Admin and Manager

# Razor Pages

- Razor Pages provide an alternative to MVC in .NET Core, introduced with ASP.NET Core 2.0
- A project can mix and match MVC with Razor Pages
- Razor Pages are just as testable as MVC
- Razor Pages also support dependency injection
- The initial setup of a Razor Pages project has similarities to MVC and differences
- There is a wwwroot folder for static content
- Instead of a Controllers and View folder, there is a Pages folder
- The Pages folder represents the root of the project and all Razor pages will be placed here
- Razor Pages don't define endpoints through Controllers and Actions
- Each Razor Page endpoint is a .cshtml file with a .cshtml.cs backing file that provides logic
- The Razor endpoints are determined by the structure of the Pages folder
- This is similar to the approach that WebForms takes
- A Razor page looks very similar to a Razor view file used in MVC
- A key difference is that each Razor page begins with an @page directive
- The @page directive makes the file into an MVC action \- which means that it handles requests directly, without going through a controller

## Razor Pages Initialization

- The initialization of a Razor Pages project is similar to an MVC project
  - The Program.cs files are identical
  - The Startup.cs files are similar
    - The ConfigureServices methods are similar
    - The Configure methods are also similar
    - A Razor Pages project doesn't establish a default routing

## PageModel

- The **code behind** file (.cs) derives from the PageModel class
- The PageModel is extended to have properties relevant to the task at hand
- The PageModel class typically has two methods: OnGet and OnPost
- The OnGet method prepares the page
- The OnPost method handles the form submission
- There are OnGetAsync and OnPostAsync versions of the methods
- Most of the MVC functions like ModelBinding, Validation, and ActionResults work the same with Controllers and Razor Pages
- Typical OnPost flow:
  - Check the ModelState
  - If not valid, return the page with any appropriate error message
  - If valid, save the data and redirect

## Data Binding

- Razor Pages, by default, bind properties only with **non-GET** verbs
- Binding to properties removes the need to writing code to convert HTTP data to the model type
- Binding reduces code by using the same property to render form fields (\<input asp-for="Course.CourseName"\>) and accept the input
- For security reasons, you must opt in to binding **GET** request data to page model properties
- Verify user input before mapping it to properties
- Opting into **GET** binding is useful when addressing scenarios that rely on query string or route values

## MVVM

- The Razor Pages approach is sometimes known as **MVVM** (Model-View-ViewModel)
- The **Model** is the data
- The **View** is the Razor page
- The **ViewModel** is the fully realized **PageModel**

# What is Blazor

- Blazor is a **Single Page Application** development framework
- The name Blazor is a combination/mutation of the words Browser and Razor (the .NET HTML view generating engine)
- Blazor allows us to write C# instead of JavaScript for client-side code
- First released in 2018
- The implication being that instead of having to execute Razor views on the server in order to present HTML to the browser, Blazor is capable of executing these views on the client
- Blazor does not require any kind of plugin installed on the client in order to execute inside a browser
- Blazor either runs server-side, in which case it executes on a server and the browser acts like a dumb terminal, or it runs in the browser itself by utilizing **WebAssembly**
- Because **WebAssembly** is a web standard, it is supported on all major browsers, which means also client-side Blazor apps will run inside a browser on Windows/Linux/Mac/Android and iOS

## WebAssembly

- **WebAssembly** (abbreviated **Wasm**) is a binary instruction format for a stack-based virtual machine
- **Wasm** is designed as a portable compilation target for programming languages, enabling deployment on the web for client and server applications
- [Languages that can target Wasm](https://github.com/appcypher/awesome-wasm-langs)

## Blazor Hosting Models

- Blazor Server
  - Blazor Server provides support for hosting Razor components on the server in an ASP.NET Core app
  - UI updates are handled over a **SignalR** connection
    - **SignalR** is a library that simplifies adding real-time web functionality to apps
    - Real-time web functionality enables server-side code to push content to clients instantly

## Blazor Server

- The runtime stays on the server and handles:
  - Executing the app's C# code
  - Sending UI events from the browser to the server
  - Applying UI updates to a rendered component that are sent back by the server

![][image8]

## Blazor Hosting Models

- Blazor WebAssembly
  - **WebAssembly** code can access the full functionality of the browser via **JavaScript**, called **JavaScript** interoperability, often shortened to **JavaScript** interop or JS interop
  - .NET code executed via **WebAssembly** in the browser runs in the browser's **JavaScript** sandbox with the protections that the sandbox provides against malicious actions on the client machine
- When a Blazor **WebAssembly** app is built and run in a browser
  - C# code files and Razor files are compiled into .NET assemblies
  - The assemblies and the .NET runtime are downloaded to the browser
  - Blazor **WebAssembly** bootstraps the .NET runtime and configures the runtime to load the assemblies for the app
  - The Blazor **WebAssembly** runtime uses **JavaScript** interop to handle **DOM** manipulation and browser API calls

![][image9]

## Server vs. WebAssembly

| Feature                                        | Server | WebAssembly |
| :--------------------------------------------- | :----: | :---------: |
| Complete .NET Core API compatibility           |   ✅   |     ❌      |
| Direct access to server sources                |   ✅   |     ❌      |
| Small payload size with fast initial load time |   ✅   |     ❌      |
| App code secure and private on the server*     |   ✅   |     ❌      |
| Run apps offline once downloaded               |   ❌   |     ✅      |
| Static site hosting                            |   ❌   |     ✅      |
| Offloads processing to clients                 |   ❌   |     ✅      |

**\***Blazor **WebAssembly** apps can use server-hosted APIs to access functionality that must be kept **private** and **secure**

## Blazor Server Project Structure

- Program.cs is unchanged
- Startup.cs is similar
- Pages/\_Host.cshtml, App.razor, Shared/MainLayout.razor, NavMenu.razor all work together to setup the overall page structure
- The Razor components are in the Pages and Shared folders

## Razor Components

- Blazor apps are built using Razor components
- A component is a self-contained portion of user interface (UI) with processing logic to enable dynamic behavior
- Components can be nested, reused, shared among projects, and used in MVC and Razor Pages apps

## @inject

- The @inject directive enables the Razor Page to inject a service from the service container into a view
- The service container is established in the ConfigureServices method of Startup.cs

# Blazor Wasm Project Structure

- Program.cs calls WebAssemblyHostBuilder which sets the default page to index.html; it also specifies to use App.razor as the **root** component
- Startup.cs does not exist
- wwwroot/index.html, App.razor, Shared/MainLayout.razor, NavMenu.razor all work together to setup the overall page structure

## Binding

- We have been using **one-way** binding with the @ symbol and a variable name
- For example, in the Counter.razor component, @currentCount is used to display the output
- **Two-Way** binding allows the reading of values

## ASP.NET Core Hosted

- When creating a Blazor **Wasm** app, the opportunity to select ASP.NET Core hosted is presented
- When this option is chosen, 3 folders are created in the solution
  - Client
  - Server
  - Shared
- The Client folder contains the Blazor **Wasm** project, which will be the same as a **regular** Blazor **Wasm** project
- In the Solution Explorer, this will be named ProjectName.Client
- The Server folder contains a Web API project which will be the same as **regular** Web API project
- In the Solution Explorer, this will be named ProjectName.Server
- This part of the project will run server-side and have access to all of the resources normally available
- The Startup class points to the client's index.html page
- The Shared folder is where any files are placed that are needed by both the Client and the Server projects
- In the Solution Explorer, this will be named ProjectName.Shared

## Progressive Web Application

- **PWAs** are web apps that are installable on _Desktop_ and _Phone_ OSs
- They can potentially work offline
- They make use of service workers and manifests
- Service workers act as proxy servers that sit between web applications, the browser, and the network (when available)
- A manifest is a JSON file that provides information about how a web app would be downloaded and be presented to the user similarly to a native app

## Blazor PWA Project Structure

- Nearly identical to **regular** Blazor **Wasm**
- The wwwroot folder will contain additional files
- icon-512.png
- manifest.json
- service-worker.js
- index.html will contain references to the _manifest_, _icon_ and _service-worker_ files

# EditForm

- The Blazor framework supports web-forms with validation using the EditForm component
- Two of the EditForm properties are Model and OnValidSubmit
- The Model property is used to bind the input elements and validation
- The OnValidSubmit property provides a callback that will be invoked when form is submitted

## Parameters

- Any Razor component can accept parameters
- Parameters are specified in the code block and are decorated with the Parameter keyword

## Form Components

- The Blazor framework provides built-in form components to receive and validate user input
- Inputs are validated when they're changed and when a form is submitted
  - InputCheck
  - InputDate
  - InputFile
  - InputNumber
  - InputRadio
  - InputRadioGroup
  - InputSelect
  - InputText
  - InputTextArea

## NavigationManager

- Provides a way for querying and managing Uniform Resource Identifier (URI) navigation:
  - `https://`
  - `mailto://`
  - `file://`

## OnParametersSetAsync

- Method invoked when the component has received parameters from its parent in the render tree, and the incoming values have been assigned to properties

[image1]: <data:image/webp;base64,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>
[image2]: <data:image/webp;base64,UklGRlZSAABXRUJQVlA4IEpSAADQggGdASrHA9kBPpFEnUqlo6YiI3V7KMASCU3fhUMiaYzxh+z7Kr1urmLtWQ7n/l/4b07+V+972b96/ZXx+/xfLv4v/peUN0N/1/7v+bHzR/1/qv/pn+M/9PuD/rd+w3rq/tn7x/7t/5PUV/V/89+6Xu5/8D9tPdV/bf9F+2HwB/2D/S/+32pfU7/zv/m9h79xf//69P7s///5c/7z/5P3L9sz//9n/0w/U3/E+kzwr/A/279qv7v6W/kXz396/Kv/Efth8X2Wf03+s80f5T9vP0P97/cD/De3H+R/xf7l/2z0h/NP3X/s+oR+XfzL/S/3n92/7/6vuzQ2//f+gR7K/Tv+T/nPyn9IX++/w3ql9mP+d9zf2Afzj+z/8P7kfmn/f/9PxaPyP/F/7HuC/0X/Af+f/B+7Z/cf/D/X/mz7jPrX/3/7b4EP59/ePTp///ug/eH//+8D+3hSlYb3hsWvkpb60yVhveGxa+SlvrTJWG94bFr5KW+tMlae+tMlYb3hsWvkpb60yVhveGxa+SlvrTJWG94bFr5KW+vQxhAe1GnhFBKwvjLOaKCVhfGWc0UErC+Ms5ooJWF8ZZzRQSsL4yzmiglYXxlnNFBKwvjLOaJWAp6a5Sdxw3zqX/RnzpcqPEhP3z104as161WnSoT50uIpCQnmJUeD+dLlR4kJ++eunDVmmWLJmYqItistVR167V8SEP4EjttMT5PJrOnWGlnGuyWhRQ3YvaVviXUEkraLwqLqSf1tqBoUPqVct2xJYIJI3slP6ZOCAHdxcK52guGXvjxA/WVapyjo9CjovoyrHRm21FAgpvRltEgJx9exYe9Ic8XfQGILNunq1uD68mBCnBh9WLFr5jKfZSi3yLZRecE6X2+6dRvuSH1mU1VMS3XqF7rZxtNx7VJPcsCq7lt6UePSWwlI0TWfRI8lIH5ucYD0phz6vetaufM/M3QWX9U8Ea91LcsOClh1BFQRDcK7fui46BQ2lBIH8D6kv8VvQWOC6JHAeeSiVVw7kXRfynzkfXGOznvE7c2ZDRp5eMrfd3oZZE0k5JY7tpJj2jnWi+surwb4pViS6V8FUMIc80gtm02Ynxl6gxlYiBlvvDYtfJS31pkrDe05B+EW9yHXQ/Bybgyh+2D/wbJN7pIaJzSKUmgWyB7CHBrbjZv0kUnLT5GAKoF74zNKSX0WNRcYYbANQHIdQTmgIlMNWmhvufLxX3WwcItkrDe8Ni18lLfWmSsN7TkH4Rbu7iGnw5QSsL4yzmiglCQZaVs05cN8koS+MmRooJQl8ZZzRQSsL4yzmiglYXxlnNFBKwvjLOaJdmD4j6w5VUottzu2odm7Duvn9/OSjlVo2ntOEp9lHex86+bQzLFv/5hQSVfkP6AdfV7MWIEQMwr+VTyobzzzzzzzzzzuUZC0rVKrH8YTwlhxVIMzbpL5bux/wb4lDW15YZYIZE6OEgzEOGMYyLF+cqTPkvhYE3kSIGW8pl7Bdw+SCJzVZViRc8nnZbzxWtSAGwIK5rokbd+tPa0jPLcw0quziq3l8maGlhfmyNUriN3z7XaTROsbG4WQ1V0XPkPQ4AFdi18lLfWmSsN7w2LXqWtYn5DV326HvjlCQ6QxZziS3adWoEAEoSC42Pb46VR3+8Ni18lLfWmSsN7wxazMx0sXrdeqAj9UUErC+Ms5ooJWF8XhDPdgTjGWc0UErZUErC+Ms5ooJWF8ZZzRQSsL4yzmiglYXxlnNFBDkN3i3swVKzJlSuJZ8M5Mt8L4yzmiglYXxlnNFBKwvjLOaKCVhfHncTQANeekyPtQveNkaKCVhfGWc0UErC+Ms5ooJWF8X/4dgUo7mdVwOvRw3TqlA3W521F4T8hoicky+SAEqm2o3byhhZSgewUHtP86s1sUNLXbUUfYCyUj4QJEUTcumeOGvYlLTywsfFSUJclG1zcw02KVzTWKS1c+XZTeUJLYbStfDAxJcYXOcRRyjMCL3Pb5z9OSPm3ojkxCUkAgsxaZPRERpMIeIYDtMw5pEPv4StlJFxQcVgvA7M2l2qsjyXBQu+L4uW8R1b1IErcwJNdPo5SFu8p8oZCXO4lhMXPiYG34Vpj8/FQ8TDSi2WyNyNTvREuNezwwe2vRQu0et1r1Z9uaLBzxgJRu7qUEXTmJtaxgXSRtWL4A8iAlmNfzO9+G9HMWow6NGo4yEeSTwyIdx6mv7dNK0ZTdnhuB9eHmXOo3L6isdR+BG2/5X44PeIIMHb2XkpHghUNEIltVSTISZqVDhyUHjzaBaUciu8F4eMgEemKnBIip15jTP+A+hwfYsYXJQhOcdLGsZFwuJ+9Ebaij7bQrTJWG/c5559Tkm51ZaG3K45T/yyyli18nX1vgHeKRkewJNXmdVwMSGa7OA9hL4yzmiglYXxkyM/JKwvjLOaKCVhfGTIxsNV/pBhgicI2L3nAy6RajTwiglYXxlnNFBKwvjLOaKCIfoUWuI9ixVLvdJ4NF+vzbX+RPUCtoGiAExaO0QAmLR2iAFLx/1YLNEXzpcqPEhP3z104as161WnSoT50uT8UXXBtU1n9GwplI6Q9EcFxfFIuKIMwdnhdHL6vnA8axkKAVLzZe7gEcdGz88imgrg7Pntd1gnZjC8fT/fxh6/wVMRMFduLzEJaa1NPQIyzx4x3OZb7wxdG4xJid4D2t1vC+V0ZXLypH3+ryGyzYf5+nALSMXMMZZX3xBfC6Hoh3J7Vt0rx2wm3oFNLvC41kVFe0qu6Kq/0JHNYuXDaInVscCCpESsyIco/JYVlk3stfqXiX/iPh4lP2szZl4RilL+JXHxFp6LkWRzpOHri/KXAfYxbTLNvO1CFGrRSLlp0beqtW53P+CsRzPpN5kI1p+sy1AE/OVfKawr4/ETr/wCE9HGZDJBe9LbZUzyJmYrWmdaMjGpb2nJB5ACapo6WEckD2Wee3ALtZLEtaDe7cNUqexjNRgHCa53XKW6CpCHGbhdDWQ0oWtHkTNWcF2RaecQfiLCWcBXpeLcdY2UjVFhveGxaqUWlPHJ6aFU889QqWw56M31nsrohkpbgo/PARljhOYg91Oeh3znOc50A8HDRrMiABFmu8JbpMeqKCVhfGWc0UErC+MtvuYx6ooJWF8ZL609d/vxlnNFBKwvjLOaJ+VhBmWc0UErC+Ms5hPas2suyC1rmkKtvXy65WYIOkSF6jwByQ6H5QGA03pUw0KOtF1HxhpVvEkbGYiq8hWN67dysCjldxf9xItNAFmdEBxyuHxz4RcE8vM4DGIX1suzZKqdKdOvWq06VCe8J4NrXxIT988pr8yigoi7r3FZ7EYiV/llsUX/aXVdzpA0Wa7g4tskofwZg2ABjLHwgf2yxa1gRQdAtcCwbb1P0JgsOZbyhB4R+H2J7J5marBgxKiw9Iuq03/BDWdNsT5OcnxdQYxbm4OM9Kw3vDYtfJS31pkrDe8Ni18e107pQURdUq3ZN/X2JdJezDT3euqLTaAZkivHqiNoNCxdbbPwnErGg6U7EcRnetvGZQi3tt/AOR/7QoYgK25ZMPqNnfhExb63DFZJhv7ydwWeyS3k5aH3tAQZoE470fReSSZk4DHDy9TiMRJxdzSlE0ZOTFRljgEK8Fm3Bc2TecMUp60QH2LH+ZSNrArhJ1PpvYrGccZTE59zek5LO1c0encfAaDlwsJD3giUkpbojZkEvBL7FuieUHP0Yt7+t847+PpoKZCTfHmBZC79yA4uKK+6JHVmUt9aZKw3vDYtfJNvSy7ILROHCYKqXszz170m3pj2+OUJBcbAdrEKVUF0iwwhAkBmoOshGzTLH71xWiGIbzlIN2rv0KO5FftQPW2Pk3qPK3Q+SrisJj1RQSsL4yzmiglYXxlnNFBF2sOPeMuk89U8OTLtxdGfJ9ppH/1jJj2+SUJiz6B7K3sHUac98crCDMs5n5JWF8eeyuNwV1tVbpM9lcbgrraq3SZ7K4Y7BNcJzmvmyl5T7KUW+VDeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeea/hEDLfeGxa+SlvrTJWG94bFr5KW+tMlYb3hsWvkpb60ye09QTr/KHuSCYW3dUPckEwtu6oe5IJhbd1Q9yQTC27qh7kgmFt3VD3CmAAP7/H5gAAAAAAAAAAApNKKXyqgAAAAAAAAAACOZ3r9oHDyZKYTyrp1pM286po6wZE+CrY4tQeoPUHqD1B6g9QeoPUHqD1B6lWqA2wFL8y3Bsg/G4goIZB+NxBQQyD8biCghkH43EFBDIPxuIKCGrOJrEsVB9mrTdQR2CY7Mc7TgzZaA4tkYfO4n8U+HmG8OxgQ004nCr1gAAAAAAdFP0KNQ9vLUPJzRLOxb03eOOoIWfS7PAdNNcLPIOloLWxQAcK4C2uTI7AZ29VHIruq0T2uPaEmp1PN4P46Cwsj3czlcLBZ1+XuqHXg+zp650rkMpYQ9PDi/i0MVnI9mCxKY26Avtb5d/L+MdSl2zlXpp7EWc1VSlTNaksoLHRnG4oYOTkj10HSfP8FRZBIiSqp9lGAVOTy4BMJYW1tBeq0jJhmFTEsoE3mhe5dM9PAlJ+gkXft01F3rC4W3+Yg0eUudcjKuT5vzBiVHYbzoE3xsVC8eu6qSDL3InFakAnrOCZfzqt1/12dahmr10khdzcOIV9rCtRwhs9txEKoM9r272RmAyymkyw3+MiegGcP6Tk26ZGbn2XJ1yDdC1tI9Xezw5ijt23aW9qeE4Dv1jEsn6/Z/j8YJ3Dv0rSaaBZ/F7Hwav/frfZmA5ZR2lMK0W2RV9Jaq6jWmpFmGN9LehkqL98oArXz6voUI9Dhvu2ICy8UQUYvJ8X1diOAhpVo0IYwg+tH5BSj2qTGr4fLgducg6WQoxCCy4t2/V46NtwB4ulxR1h/j91XaAGmUQ3+Y+LVfYc39swWB3PDw/wn4P0tS2MvAOND3GwutqcWyVg0VTcrC6wSmIwjICOXjSe2+/o3BGz8n13hBgOKWFyx+Xb1mKaSBVD+VYg1AdcHYR6lFrEIaRKOgmrR3MRFOTOEIwe8JAG7J2cwohQ8o8bP/XTaZtx0tBnEH6bdBPPtNHrMqgWqdE9B2KMxWniAZIiL5IYHOQhB2YUItZbQfyBbj/vcqZjEnE9fiPQBf0VoZ/dd1Z57lc9LGxcLm1X0fxv4vBaHvLkJnjr3e0I2Ae7FNnNVh081dzWqE9Y+oMnblA6pBUjYq1+Gbt2hKCk1fzk4H7xffk6njFxggXXcSnYOcn8AQM2rzGwfQA+ei+l+sGjPBATrq5EQCxiXzcjjslrladV23VVTBlzGWkPw+Fkn1BT98Iyk/saMol0CNuN/cqSqBe4p0AitIFQuVZXqdXV/MHlAwdmqPlqnH5AGMmjZj2zrY9Jy8x+wpQz78NaQ/shMJ9fFuMvlIWmB2PTGITHF5SzzV9qbTbMCkdZ8Q8sqH/bWKpXInO7J/2Q7WV3Yy2KxbG4XO8hHuT795nX7tDV2POeVUd0Ius9vN4fnWm1TZ5pDkn05rc/j9pO+orejesiin7l1SJPpe298/0Km99DbIU96ddXZfDV4uWjVT18xfLvdb3GBReIjS2s/lyEoiVh70wjL7Fv5mUs1lwx0xWSNiL94XgqXY9rmYbjqqMM7LMMdc4m1yXucgWvYQnmRYzYS6Wmj6LYYSuof9WYJ4RI6cfbtSl53ew4fuVpFXXkcmJd8y//Gcrcv64zpfErjEyj9rl2PpuRifveEIxpD709GkdsMXUsTuMzH9eEYdZww6VIkjwSbrMtQxwRRjE7sYgOsq9kTpNOtdFwcfe+BI/9ThbcqJ3g1vOExoQ+FNJ934/+o5QXaa7F5gDEdA20xLOo8AHrXvfwAAAAAAMXpIMj7R8w3warbwiMuDtl5Mx2v2oNEjKrcvokrWMU1tpHvsPAFxtS8AdeGkFITxjSfN2X55Lk4x+yj6996feHB8WGkaZoCXq9Qw46NLyrVqu5fgD7KMkr8ha7BASxBEpKxjcC9WskKb9g0Ze5k5HclGnk1zA0mFxzV3ATEaK2HIqZsgpbPdZb+AqM7cq4DwCKL7hZcdx7ZIwA7hkmyGF1XiG68hibxq0ZUHk7KlivZr2bBte1YcuBsPBhyxqEohIsq5lBvl1j4FMPPakcHX4HcjVCyeGG2ta5M11VsmedZThKPA1siQ4FLE/ym9FDdxQPxko/gXNYtOYDOfDxlrPNDb4n3RnJW5JuTfOeCZgP4Fqpr6smOqxS6lwhhW8bZq/Tvz55pplBrpXR7zOdbXBe6+qIsR2CgSwrp3tNYXgTQ0l4gjrFgrIykmJtFaAfu+FxeGHc2DwsZSxH5afBPWGrh01TxvkE2DSIDQ348jhNHh//XixXJawf0eEpv1UgramdjBjlHZCyouwcuMLTLTAAhCO8zQJABXidxAKVnr/7dcmuYf/vTGvxgsCpregHEGYCm44HBK8mv1tTRJvAnJdQ6eFwTfylL3ES3V48++h65pvztea6zBwTPiLob2ejmveD2dMpzVmxZJi3y30NOEwwoN+705eIExOXcaY1d3ul6nT98kvBayLiXYkayqnoeIpOtShHNN2lxDT8CjBQUIG71j9wA8uqJ6uT/Ec650K7DB9m0OeSkEyIbiAub+zZg8SozAVgaRE73fmE2do0P/LbuYmEnFpgzdrZd4CbzIboRJMaeTsLTNW3URWCapzBNAlSVYJShSMOb/haMygpZLcI4+h398yOlPbAJpvMp8hfEfdJnGvRc46nWD0HnqRIc0ytor7a1LVMj1Dbsn3xNyzOM7VD8mEcNlnBLiTLRp8Q3ahLHEq48hj8bkrsNSbQJXJUTF1MmNoI9Tzz/z7D+MLy3o5CiqZf/wvQJDBGYy8vOjmc7M+nnRT1FB6QzmzYefq1Y6aRw6b4Vbc8Bn67GqVeXjxHpS9JspfVBayuS+4G2MBMu9eHi6bst0WQx/zqH0JS2upKWFiWYXGR+8J6BEiz1ODCgOlsu5EWHVBxHI4AwokyR+dgPrhaoeqq+6wZTFATO0W3eQOBbaBQFI6qz0pUZ/bT8c1WixcULK17DYarHoF+n87bIH+cXhkzmbDrvgQgyJwDd7dlt+CNr0+0jwB08sl+40Jn3MX199uSRhoMScCAcDi1g8qpgXRMAyN6iK53fV7sNrW6Tv8UTBOt7e210Smp8hXP+k11vavRfAFcH1Ym4tNNsOPlHiw1dblMSK4fZxOI/emKu5d/Nc0ut0hfpesn4CkVJEX0d4T4p7gKigmfZ3GpkhV1Ldq3S8zyEjC5LMpfRzPR9wofHsrEcq4NO+v6wlDjD1HxErK80wfGvAf1SOBo8fjXeSQuVr8HYBf08V8zsW0chZlUY/8Y/gXumEtfERNdl07AyhzdT/b7kCxZ2YXKYBgkT8DjviXZJSmnZdmCJlT2OhDOt15y9PVudZ1POesn67iQpPMW+92pGZB5ocukp9ROpMrgNzKNRUO++aRGQO60/tKW+LpRYvmsvk4RR0afeGuExhF9EeW1UNPTDas7Tdn/UmoCi6TQiO2fa0g5802s9dcAdwbo9DQFVTGI6w7erEZ9s5Sx03U7qdmHYxq3ljJz+Gds/6RsOQk6HM83j74gMOL+4XO+9DEvLB/n5Sd2eyDTl8PxPm0VYJ81xoh4QuANyUumDNu3Y9BlFsI7BL3Uiw8ZzVpxHASkO/k5W3hORRfdIf3Z+q0zFYn382RHBoOpW5wTX5MJfR0Qi/5VnejyWWliMuIE09x2wEO41QocOxzH+KoUvOVbDgMwFTq0JK4E3MbPRzBJWtlqtRMrHu4KEZrXKGjtZbMdtpy3MXghd2NQVXHJbV1qZfKnIQko1VyHMDGnyYreHyE5usZBrQcX22ltX7FXrOo1AFhqYnaRRy1AX2icvcXpY0Yl6u7Jq1h+I/pzsq8C0I5SuX8cAAAAANXh7BtxUIKVaeVpcRL4ZbXBlNLxP/ycghDrI7xelAcdmV7hRjGDjHY9TcmNKHzp9hNzvMzd+U9Mj7G6rB3laFOGPA5qyJ6Y2eWDt/OHp1xwH3uxpk/vtpiH+eprwyDBFbo9TSOslDVPuroyeWtJcjlhlzkWkzpk01hj6T940lxMbcdOtjVJh7Dwyb0MEWBl47dt8X1sW0PRGR48JghDRuT3ruuRWqSTgZRrvSlCzIvJ5pUfMiZZEnA5DvwGvnVsjG/Qfm0DtiP2yb5aGarskOaKYxgrf7zX+2Ljn7oCy3vLi0+hQwonINeykpvLltAjtv+P9CPvgR/s9+Gjpa1KjhGJePXWSu6LG32rALaXDqb9wUqyGGSjQUpn4oC8JE7sgwwtcH2dcU48kHSlIk9M1F48OCRTR6OOz4GFR9h9+q0yH4xrrt3/+BzaheEXAyLN/3HJnzkvlDU678fCk9z0sGrjWmzTtB4FUFRZoO+nSgxXM7WPH/fgpQl7I3BIOIOYpEuQuy3bhJq6MQJwM8qoxXE2LNSRh4si2TH5qll2ZOtqK5sQn4HfktCGXj4WcemADjL3SEeIDFGLPUE9MlsVzZQsH44hpuzJCDlfSMSGcAIdC7P710hzWdixfs52MxgpA1IvBrj3sR7PaDnFm3PNKHFxD/KYsw/1Ev50OLcfu7nKjK7cfFigJ3DcmOIWeQVGpfpvrAjB5faPhDXkr80UU/MsVzL70RkoAw8mu08sR6WW9VQ3RDcCgCmmxV5sAAAAApudUB0Q+YknArWXPYgCpFwMKiYNGdK9/H82pAv8r/AKVvP0OTHwG855QrRAN5bqgre0AeLf/3Tk61rm95TugoICArBQN7NdCYjTvqmPIaXDChOQPC4IIKUZFX3PNZz1/B17L/He8lmiXBRrORGSgG9QtX6aPV532Ax5zKwfLNxhJqRyeNgB6DWPQhOxyb4CHOCIJNDxpMSGpXFHGkxIalcUcaTEhyUPqRjdb3+B5tTQq/O4TrT1JdniGUqOY0ugK3ZUk30/fpQylbvH2Ssv+5FqvESxbaw536oHxZtcaNw74Vf1DTTcMvJ4rCc7sklLRx6Mi2x+b+9w+MW0d/4W7vXC59crNLEa7moDlRGMgowPZ4jI2hfftv5FO5OqZIahUvbJjbSwIQQYYgBZXyKkL8xB6BmZ6dd1Ir0qM7TD44nyvjetKXUR+QPm3mqFHn113Mx0BKcJnVZ6CdFWKcP+XHan4y6t/VRC7OyOEulguzmg3pgcnuwDDxHcBhfXtEF05h1hE3LuUab05EU7k4FXogUxNXhsCQv2/1JQh5QpSDqnZabhO487/84i658X/kyEAAAFq5trF2YnJT194tQNN5LtpqJ0rnufhNC6JDxVs5of/vYRDpjGyqC37Y7B1Of6mhavp09oZEp4z4h7CrYAc9OtJ3trT0eeXDvFlHdaD+eg+bidg6bZHOQW4ZASakK1H1MwL/Gvwkqo+uq13BCeSp5a741WNsQUXntp6OhRRShJBJceAtFc0HOWSjx24dcCbWQ9AmT87Ug4pbL5g9SzLm4vgvYLwbf31eLpN5HaCaUWKCx5zgCK7vptLqihi8p3p4DvkrkeK8tNVecjRkCoQ/JbEDvk9RAaHrk6h549s2mONJ0yng+UTmLQ3PIQxyi1sPMBZ1pKK0871OMfP7ekbHo2DluwxqYd/8ZmyKM4idNEiuiNv29WgXIq0OKsElQjbf9sKb5w4m/xy9NpGL+wSquvuYyofoEXiJokLr7Nv39Xq7uCfwgsSBLXXIgVcAxSFnR7ukjoHVAWyDthQ3iBzuWpkCAbzDRSj0gcoj8ViqoGlFszn1vLvSHiuZonQ8zCVEXWHeg+4rqw7ELw9DopVD3dkra0ISmQADwZmumpjmyDAyIZytAxY3jC72G0Dk2Xbej5Zd7gThCACoDPJba4kBTvDPh91BHLFpqo7OR2Dzl5o6OUlyP8UvS9L7mhdJvF3p2Y0JWFm8fCDDjETN8jDcWeeNfLTjYa53CTOksQf2zdAG7CJzMGhdG1WMFnk1JkTihlUY/DcU+HVlUiQTHVjvl6On0WauxbOPizEI/XjAjqUsq9YiJ1+JXy3wcQCR3U6DVdMuZK24m7UI4d7iCCup+UUir4qyyXniQuf8UTeoZpVLd6m9YHjOjiZ7+JiLRM7ul8u7wEGTzX9y1nhwHwwC80+e0FRsg8Iz6FCOuA6ftJrY82XH50K6wW1MnWFbjwmVuT0aezUQCty0VT5om8mAxVOjNk2xHm6DpsrFH7iYpZBXXt+AaXQ6pLmn4UVnEfZRn4LYl7q7IZOcqdUTz2DNjgR92tb5HzurCtlLejHw2BJ6uyz66CbISZu9DRIt4+sbo0xDjqSMz0GSaMoSmgsee7Xy/5C1De8Ed0JwkpH9tB+Ue3kvusCYv1GB49I5WrDOfjQ2pBz2z0dhwQ2qZ8s3qLkMf5xF8SoD8V3fJ3Wzm1BQOaYcL3jDhinqblaMteVCW/il2ucrzVAAAABjMpAJsR5EicvaIPguOq1N/x8WK8UAebOZNiy6Amhzu9b8ghi2sVd0pHRJcVfdgAAABajrNTt5tWKzkdMS//8m3yw6v+ecpEUOl13J0iZo8ljkdtO9MJe3HwcTp1kAGDFeISLRNKgksF9Ju6Ll8+mGx11ZiyThLszDpiubIQLnMNdT2REbA/CRfs52feHTLvrKYtm78rlAATsf1+ZcMuGXDLhlwy4ZcMsfxmepOXIEyWkMmDmLiBPMfQgB9HCE0VdbNirNPd/aidLACuygclrf82l4FUweVwhrtG9y8BzIsCKwBiZFgKjhtiVRGA2jaPGM/orRKh9+j13zoJgZVCdWCMGdxjlkrhUEkFb+GvK9MKUHHAVL1mAu2WP0RtCNCHm1mrWsG4ymTHOZysrseDP/gQ0J5iTVe4efr8fKog03l9UckYOE1as8qCGJCVECkTTshBmIX1uFfoE7HLPSi3g0cDC8Ic1Qm2MpItBOz/4J6Kor5G41LJQDLkzrrK3nMxhmR8omO9YsJbzbiJAOjp+WthfdPO4/rvV9B+W1YzhqwmCcbIfQoUu5ZEwnoxIjqfzyZzCQisbI++CLRvAtfubZD8leLCr3N6b9E90i/ZjL4sDUi/fZE8Zljiso7hgvUHCycCiQykzmvGcbJrYzgRjSMF5Ww28j/HUar2Df4mAKy0VB7csGRsUJ9FSZmQSXuGPa5rYHhW/qHUunf8htfP+QkZjdkxhrugSgfxVvBrtuOK8UXx49XaHzvBPD/aNY0zuB3lFXXdLTkjvZcSfj0RTa8R+4ZQ+rpszyCPvm8ENe8K1KSla8S3jsJQf7yzlEaQCNRVAf+9mM9XGASvXHc+uhcyMdMrZ1eZ3Tvmnxllr9Jy19ShL2QNKFdQNVTICQzuog1vd5U60WoJZirFG79jQjv19Cwf3JUBNND8otbmC5HYeRTGCUhpeW8GcicC6OUfzjyewttFg4Wy1K5pb2/+aXoWj8fI27kQ90qoweUhy9YGWnFnpRTUnCRm9tJFxJ7D7TMBRElfYkNpJghtJvjRK4WWboqO/kY7+C4znXrnwqswCFOvXLTFZ3Awq90MqAbi6dBmfhOkUmxqLKvbxEguOx4PG+k29umiH6EIlt5h/TBWZOaq/8utsldMRb7s/I73wooULhR5Q81I48kyxNbmOULgu2m+5T5ogFqeiLm5HC2pWHn6I6mneCA+9L68mb6dtIpy3Cjd0PfSsFJJWtkQtNGensEOK5a8SjHFnfrtsdGqSciO/Qu6dWjMsLJ9miy5Y1j1z65+oUl5QcZCNv5WekvZHmZqI6RxIdLKA0/70YCO2spM0e8xRiivX5FHoFhxEm7supxECfGyvCTv1E9IRuBsswvH1NOhz6me5aG8Gp8Z1mcaVsHNd1ofu/P6ypcLDy2cpT1rCsUOP7u5dsG2XmJH1g5WlNqewbljzS0WWxTDJtF0eVnZLTibMEWxP2W1YMw5YNwOVtWHxSOJF+IFUWoK/9vQDKXmrZ+gbmor+oiIkjwMZgF5qhKulEZxO4e9/vHc1GeggbUtggxbomqoR8ow45mMrmH9+3y90geAFqQd8At+HykP8+Axs+FKRvmcH/CGYEX7Iu1qEM/Zgk3PdNvcCPDRD2UtBe5JWYE8SirqONEwXdtgE2xms3FQwS+M3ErART6iID7PjS2OVu+dhXfWGLlAcI+HjRo/SfyDEjTPX8H1c/6Luk9yv3DExRQ34QhV3K94cVq8HUX6PfBHhCigy2q1xtu6Gg8bEZ+7WJ57KpSOamK5VlX4+iHYAv4iauhiugyUIkOkpr8ePYo/DRCub0smjZI59jBSMREk/o/TGHwji70s4dYbttFVOWuQtLbhxPKWLDuegOPgZYAnVAguvJoPJQk/kANAaG70ugOSqst+O1ZpUTLgncb083GOFrd93jEbOD5/Dn/Rdd+8hP/+rvfHRLUf8LwrwoP6V5vPHB3Pq4h0ZoHwKRiLf0zvT4dPRs9pm1ijedTNu/u3r6SuCkkh8vOL5E2REFrzgxOEcOFuHtCuo7BJR2rGvX98WWO4gN9KByDxOfy3Z8mbUQTqTJURYvzxBDXJUbrNicilpZTVTbWkfroytyUCrSrosBjJyxY4XqFo9L9m7Xltjy30FHE4oN7DIIhbd3DTH6x6OAHUozB8IyNDYOo0S+N9Ps+T7OAzPkV8nwgOykg8OE5NSNIFhoAbIq250oKEaqA8Cf8OxUPiYg+Wn9EclsFlGDGfbo6q2QdMt7fWug15Sxf9wrehGCUda5OnbCc2NgN8fkH6eLy8s+ZDgl2uyjdETWustKPdLda+7f5vurl1uQa+qjtLf0c+IOHmIJ/nF3K9BfWdNCyPrwEJQVKd4U4IMBmS+JJFNMfpa8vAPf4V9RK8qEzY7/gBuqFftMfCBTVEQBVLlV5V+8JmQIMzYfr+wwJI2CL+tw1SUIlcc1Vaw4rposLYn44b4ZF7HQEEEWfe/20SggcrobVi3hANDnyM6bcX7YVAFmbIT8G1C9CifubA9ridxS9XhAbx8plTmeoZwBXL7+SisxyhpHzxdH5fspTSB16uOZYcN9ZgpDLoVbUm1k5MzPQc++OE7PoapwMjpmYfpVC+kmA/bba2EXhVT87T8nigN/cGEfCIoL+fp5r3kZltHxwZaVHsHJXYJD3DmLc83l4OZssWQ+dY/7DrOurOQYgOddfSYhS+dsnFUMtOQsS4XTxzYPUYUyf3p4M3qvQfdjGQMeuTKBzrDVcyDPSjVZLNpAmksM2S5+PmPccrwx4x/aqrzEOgQQhxJDZ4g0PgiGu0qKDoDWdPSVN0HdTP6TDjZV/tEez+9VPEGWWFmjLydSrAAL5RV/IXItEItGnW9xdXAGRRk6/6F2uH5X7qBOlPcHaSzzXhrQqf3vvIoLW1EHWrEgBPCMUA8ER3u/h7afL3PI4Gwzl0yIocWLp794DGl2J3kPKlvEZAjrVn9r/0GmMzKw0q+OVEshLu3rx3KMVnMp6glVG9DxwO3fBGH17y0eM6NUGLcEn9DXN75Ykm3sn0mZPau3lQoDLM23sTOfJz74rlKS8Tse6+i6kiCzjBwZXR113Ry20Ez49LRvjBm1vb0dXq1tHcIjC4VYVteRHThip0IeZMdntW+inqkIAjQjliuGaPhXRrczwW4EGN80FE8NjtyLb4luQORfRxlkkB2Q2+H0q5uPqPnBMlwiRUH1s3mug2no5Ig1xZp/o6DQGoFji43Jhq+s0YBuRt0uyDGVZANB0CHAP1zdiZk3jwspoIh3tdla3RMRQ/Asm76mYNHFEvuqNdfpp7zm2dro/r2f6iyfRZZficIEX00056bLA1fpGpNCEK/CVx7BNqu+AlcaGFMIImfXqG33WGi5XwEzoU5G6Ny7+5c54Lqr4iSP/fdsHi0O60N+QYviugWa8/thar+a6OF2VYF3oqpKrULkrJyvbDlgqneTG/Z5x0TuvYWoPUCdnBVD+0vtsA60Pe2u2tYcxdDlHcpFuH3EhguBxZYf+BoxKJ2nLmP82niutzR4Lt+ONOEJPfN7X5oxWK6GJbgjYyScsHYgtGFdSejnF4OA4+ypnWcdjGHMOFF/BJlVlhImepQROoFSbFe8digeV8dUnH4KV5e5Ihp5HV4oWkgWhK+UdCqpS245TvquPPU8xu2ma7MXWp4q/hISgumrmkVrZkzXMwAkXWljbKaG0hKa7fOlQvVH9+RmhaaUGL5czSM+yWGjFChRW6GgbRdkPISTJJMRHd90sU2ZDQmw+yjKcVL3Afjp+CYIDQ3ykT1hJopCKTJMvbskRu8YMky+JD3C0LIryKbX7JHGzFZ+jbvhIj36rGCwvXCP79IhcVqNNurCxEYxgBeDnOcBv0tjuG5xX62HOy0kSBSFkOl2ooVP26y65Sfin8llID/glpMBhquFG2JaCWk8wWKS+t3OkJ0O9LJ0p9/HViKgD+NSpeCnoNlsuAN6VvajmGDaq+0OJfUwGij0lDaRGC/O1B/RVZ4du9OuVwun0H3vF0njxNKY88t+2Fs1R/dCik4ahgwOBhrK4o3AyDTZVNhZW/IaJO5QdlOtctBCEUHkoLELV+Bqkvkpfx4BhfuoPkmaFyExqeFscRDY4DITvl87dPg2U3oaKp5NV4Icymn6evc1W11iCml0WE5NJUHgwQaKOyUXD5h0l0Mc5vSGOU2Kg3H+q7CirpNwEn9G5qm2le0QPCNC9FWMOg3GdbnuQ25EoXdGA4NtKSJiorvSvBseItNuGNfKcxWJ62rnb9VMXqDnLp5Rco2471ykXsSZ0rwAVypow1QWdcTZPox81RNTAUIn7G6lGK9OJ7FYqSrVYP5yC5++ckh4Q8mhpPlYSBQz4MjnmkH8lnmCnYFbsNTaN9Ey5J3wgx0M8zDgYsXLW/aZQ9LSSCOwieyR2A29bjuVkKZ42dQ88qRf0+8J2wTPhhpztYawvX0V4hWI80oloL4VsbRhRBGbohCdrTbKo7sRQUdYVlOFh03D+Lvp5Bs0/y0RyyHpOAePctdJSEeeYwCaK5e6u8XiOvdVGwG+tGbjll1wsXIo8caeMPtkncO0mTgIgnY6lVWYa4WzzRsf20qS2WqcsWjuWV4Szrq3BNQT2Bt3X3CogfnTThM2AcaQmJvX+3U1xF2Yde4WaCHBrqjte+MHRN6ojVdHfxqfDFj0Mt2AX/eWF0LpoSB768c5XGwrvnDLn/1NoUDKVlzum4eKrwbqF52GhPcPa/roHEiGDCd6qBfSTm+HA6vbBbMzttP0AE780cvdd3i0TG0N+GBt7840zi02YKUP52UGSczp2qq0wK3I/O2X0MvcnybwFZfnKEnYsJC/Kqciww4nLeAOaoEkJ9y9rVmMIyBmkVZ+JZX+PcGFIL8Ap2PdCpxl9MqOYLfkE8IhHWhhERVe/oCLigNIfeCRIFD0wPsX1zGLI3iyp1vE3rgAYl3tfgiVI2pv5nTYmCDTMIA9sb/Ol+XyptoY4zdBY4qOL56dpzvsoGC1rrpavUucpiscDZIsEnYezV9/l/9iavXWVOuEL5tsdSGi5sdcCXsGBdhJHPWOLEFTaEU/DIuVkNiemviso9VwpFyY5Y6uX/n39HwDWAxTtQS93LSwHK7vFdBzPAT/wKFefo2J+rXeCmP7yswRaAl/jPX2OFjMV+ZeZhwwJ29/dFP8mkMVeI7NymmkZ8vrfCzFHnw5zOtZP/cNYl7NN0KYxeWnL4SC1SFw2bPrQ/biOv/ow0fVWYVaTEnm5wCo2mHhu6xcK9UVU2AkMM9edi5xQAAACiAiQqLzXgi+L+OzONoZ3wfaxX9JH9TT83XYUfAsJiaHuAJvaOpiaGuFjwJjioAZmDEnzZiOevetjwzN0oVkMd0Yx/gRRGyDDDNT4Z3CwnoJ7RQJ6NhK4Hwo8DnSUC1MKR13jDtAb6h3uOBuY+s/7X4cBvqHe44G5j6z/nPXgcPlfeEceQf2ix6TYkNPwWgrTd6fgdvpdeRDFzJXqQ739aEs29WAuvbgAAABwtlfv3OT7XxvZB1Sy2bmWqWX4usudDyhfq4AAGBV2wlqyEA1MRUAAAAAACO0sw+1g8d92m475PDlSySg7OSr0zBjN/FSoDJyDooQACHHpGNZ+6PCaILlRvOv9N2hzk+giUPusDAn0qb/eJWWo6W68vjI9CP7++2r5w+IcWmV6EGZn+6LYqYIVMj4ilULMjc2r2HcahDobgaqhfVqaZUGpiBgNhRAXpasnx7AMqt7Abo3Mq8tTXC0odsRX2tLkOVFhqVs5l6i70oBgHc2MAvajIMzcEU1g91i4bKO+VgaMirZ3Ak8NXakIPRYTHp7vfQewcigyv1w4ltc227kH/SY/ZkrbSRNbfkYTzPb9z+NiFDkbnWSfliTUKQZP2NmQi6jWCi+Ls64rw00dfOFTUarHNttYyqLTYfzB5/qONjEUF27L08k3BRkAF/vDlrbMXsd7IVPz6eElKuQ8w+qvIxBFs/kDfC3mzPEFToIaL3dpyBbNW38GCZbsNt4W/mK+/TBco4vnmsJGfpCPDdmut4QVHU+E9MYQ/3gD5xsqFL4/mA1MVLhLDa2Th52bdG8XJXaY6ObbuNC0DCza+/HcWTiz19MeQvk5mcSuk6Te2qq5CgSytQUOc7kiA1e0kzMGyMoKHTK+/8//iwh7hzSJMsXEB6ByGXHOexbOA0QWPvC2LHRq4h4vP89H3uOWL8jW51zdvp8iiGlc1uCX7EoP842/zSet2E951ytl+eUdfGNhhwmBh2AcrMB6i3mmwIamTvSVfZQ8sD1s2OvmPivVxobY4DgjPo3ds6hEknXCSWrmRoob0CjNIIKFcrfdquZfkzflpjky8R4hh4x3qB13xcJ1M+cGxNznSlCQPdQ3JIQn364a622ehQV+X4TzqZrf4ykjC/+ifxPdcH4U6gMtYx468Xuj0zbhBoKG8c3FZKvlgopvH0ySp0kvfD8XMF3XVmZkK/rCOr7jLKWS2bw+gP3KyHbu8C8F0k4HdnFoDW3Sond46W/epzGlgxMX7Dn7bEt+/Lyml1Di/8bf9gQpy/9hT7O3ubqiHnz2LN+ckdm8rPn/ik4GjmWa/pHdY59fpbxrq4QSN2JY6ggJubEKsQH4777WEethgsdShQ95c6+WYqaU4RtT3XVawRilKtIpLD0+Bvpu8C7WpNqAEK4ac/eXa402QM/MdabT/Xbp6B0AZgAcSTCj6oKKrOnRbKUfJ/P6qpU1HBIoyts3A8I6ebB2yJZ+DTKwBTQV1MzKLBPRLwye18Kd0WBAjc3Xpq3nN7AiJWdIzjhUhszvPVg0/ErpvJzBCENBVqagpOnJiroNAky299i59ugoIYdnqHmqc/U9TCX0xxYY0hqa//c+dd6kUpx8oeV0UXER5FYT6EG6TN85gOsnxumlsH3JmKAwVM1U18cCNXlPfg7kB8xwKBdd44nDWq6QuCLZOrGU2cmvoaZUcSnpbd9Q94vOtLbq5l30BRI/fUhlRg7pul3JA87yCoEz+Agt5huTpa4Ka/nFs6TGg94lf+IZaIHzBNPa9P35Tq1jzP8qvVLgAg1/0fYg0g7AP4AwQxp4zLygNsZxxYwCHI3wkh+a1LSlcUqhxrNknzl8LW51+I9m145jOITvWImjI0N+sU1/trbvsYwkl9taURsP0fQCe2oThKCMdkTDGz22So194jP8Tbtexxyz9crhRcbub81SYHxUEKq2kKlNR/L4jJ3lxiUFTdG+DhcfLB8iCM8l/1pPCRXXqgBb0XfHfajmUFySGfBwOam7QDkc6vD909JsxgWMFR3jt3WmWycmwD4iv4T85jl8Vk2DKUBt4mWIGnM3c0zM+BMIFMKmmpghCITwo69ezrVMiBIZcUAoGjC2L3Vh84CeG9S4ASjdj1iL+2S1J5TNhBSz9RiWycskoJ95gtOyqo/dZLwFHornDKt6iQA8QriYYk+AYaCGFGG10WHyacwIUaIvcLkjOUHYF2wWBoqn9dhJ3tkqGTzIh5Y9TxVwC1M6jfm5nq/7O7Evg4xJAOlrEWM7SbT9jmJMOZPFjO2x+elBgu3usqb3BT5N/U+5jnbVPhU55TYY2yaUq6/Zkhh19w1cF6tu8ct0ssnFPuNXo9oj42wLhx5sysEGLOvlbxGlH86vh6APM6fwTdTGD6y4KBbDSunKnqavqRWiqyGDESTI/fQV2tDOdoC02aj1k6pbNht4Eu+3slTL5t7cu1JffS8js8hDjAtjZvFclVmetKZP/iY5uTkBZSkDIrmSu2H+zvlghap8DC+rK3z8gcBMylURjIe4AmdvFkksq6d/CejdC6oMEykr/cJSa+UY4mFQ1VkGviZVZIyMex2RXvN64HR3dgaBIdnUV4iFA5mIM1ES+ap7xvp5upHRsupRF2Fuol6vJzgFF1ihYU8oPsvS16L5LaKgXEjbjTzhIG3QgmFWPtnOX3/0ppvm/jILRWN638PNC6yVIII9/wNX2dpaATTj4yQBj485c68QBqiOofQmk1VBcjZtX8EkA4m+MwQrD3X2n+u2pbhE+OSDA79IkooR5dw99aa4ZWJmGUMOikj+JhdbTzWQSprvcNx7noBvT9txkI9uTETBXkUFWdz5qZ5NzorFVlZILH6QHweNhaF9TobWYLSsyTj5kievGfGCT0ZwS4mNuPAhShpKek6mCAhFXB8TDR1vNOGZOTO7JOVlUxnr0OV0xfKRLxisyIrNe14X+koUa7llMbeSqjNewIOmS2KgaQqXJVQh+LvsvxYNsedDT/oShPRQRoaDLmgZbzupAp48Sb61meX9Ntc+BwzdY+fSH/eNbtgrmyTqoo26pOCDnOj5qlaUPqf8JiXcw5sNZ2N2iVniEWgNsQRU4oE+8E67HeJlDYSGa+jtS2BUwPaOMkfbLC6ipLDlQoSjkD+5iNTa89zI5eEnZuw4OsXx2SXdNSwqhOJiyUNQfleXQ66IA2+pg0iZqZQgwbZVmOvNxPNA3t5cOVCz6eB+g3LNTyM0D+xBdTzy6DJxd/gDeUTs81LpBu/ya4sGefxwKUWYIV85UVcyXtwQMbgmxi7dPSXyAfFhnajS94ftWovQCTVoMory0Y9YLFIB7yvPdfE5z6+sbE+inCXnaR1LIvfxmRtZ38Tlt8tu3us21WSHIlhXEy8Ld9fFSTGCTdQZD7EeCF+CmSiFYT2/fNmGRXlGZXP78i8BBAY/G4r2VsTrN7btEKd5yUkTHtQUO1Pu62QopmrLO/h6iXO5J14i5fSQhxDrrecSgck4GoyYbhvrwidSvJpPniynvL6v2x1xsJej3J0xlfONV2EJ9LhXlCNHZjS0cadvTj9OUTqbH6TCYPU+6oEvzXckm89TTiaxDV89bsiXJQKZsxA/F2h769W9Ay7s2WtiZalJNhfyf2RK7ZKrLKd2P065VITXS+VxsS2Pukdo7FMbHnIXht4reuV+8c/a18Ihtkkq+ZmB4MKkL/jTM8Fh0EEi5M14MztfPxpBN4Dnhh+5aD8tPUBMBRcn/ZSrHrPmGO7jt7W9LDRU5DhdxO0fmaadvkd8XFNS2iDR49jKgVePteTYp6saP2gW8eQ81sEEsW90hI7hLdSBuXB6/vBQL50uH7ee4vrYsqgaq3ZkCMsMGLkhy08lKp8SUHaAn/UVG28g2frL4byDaFp5le/AWFgeJitjd6XxAWI9G2ZKZSVvJguunoTofi482JhkL3vuaZkipv/l/jawKAEZD5OPTj+ioTD1NnqIdZL2hCZ7o4kmduC3rKuB3vYgMve84kgydZVNV2o+EpvWudeVbQN3wfhFqCYL3JTFbajZ3XOhcT2ciW3cZW22svL+TxQ9lwKvJHpVniq62uKFQDia04j2QCx9N43mI3BezMo2XVFSjNFsXRzMlOoKoMTjoC+lRJvqLalbqurkGW8KbWdBv+vK5vCbDGMZ1PeDHoDcG4uNoFCGe87mRGPCIUfhwtgStlCIXJjxjmrzjFwrDEI3SMuS3kaZG/IS8xvRjduAf7dMgQnlRgX5hAErg8tT8C4i7QE2/zTBwPL8YB/CDY1maSNTWc+q0OK4gv4j1krfi7M35UQDi6FzgHHPZfFvzbuSkj3PtYmMuE42jW3WMPVhJ/jHFb6oX5Ro390ZxzThFmfKkkSyRL3BmywrlgKPnu2/TxDslAxRUoppUHp4Vb5ZPe7a5ElVPLgxzUPohLikC9258/XmQS3eb/c6QqNsdTJxqHoK/oRFma4NvC88wppmKg4+SH04r4Pyb8rnEfxskxSLgKvPVxTApNTJX2TqFUyQqCuFYpokETXdeOvcF5lCtriXwyBUUEtQn4h5gqFtbrMj1YEcqJoneZ4runOenzU6LeJvXgvaajqpzxoBhqhiyFdMOyL7z2urTa19xPp5CLlxWdnPmXYDzjj1ZWUqYipFCqrHzOMWqVCssH12ZSSGQcDd6lmZDBbueN0GRqdhl6D32Ika4cDXtvIqpAtDFy4F3t/mC9q3w3+MGY/EXiR/HuMbN2UTEMxsSViuxkkuOtq0MZ9eb1HzTmXoJ8f11bpnMlA6eN7fUhg0tJ9RSUnw+oBIJ0vWO0YO5kacrU1GlRX15kf8Wn7nT7CWOsGNyYDLI+An51iA7hE9Ch9n5oIdoFr38N73DizcU9FZd5LINd0xoARHnIGQ016eQGlZ17ZtKFLPAIfmcE109VwlcgGENBFr+nv5zdpRSIqnVvGv5XzHUeBFJEOQem69i8Pml/YjGNT4fT30uZ5JFrC/RHWod9+BotC5/BklpvBWwNOmXK9/8DgJ42d+D4FrCcC/eOdy4dlQ1B3HqS4QzGcjvOIxDu7+2ZrACCDvbhiNrE/ulTKmf2R9nd6+ydy+/CnNk3jV7pBoAABQESuPrApdtBzrIyMq3bMNAi2yxowKgYoAAAAA/VgACSgAUPBTNIof44hUXMjIruNNQYVbgX/OYTpBBXDURtBebFL/LiBJqMPfbTXZoRakloFCpzRPQGiuD2QyaXhvB33X/C1ijh8oPKp9e6DoEyxHw14tunO8W8yimsnqSDyXH7UnTwgmdIfZoYkwGqwVc6wNq7TLD+4im6rgxKAkReozFXk1/qRcUvp0Jrk6FMyVKeCssYW6BSk2M6CyydjdAa3evDGI94Ge3KRjOQMzggPLai6YHVh7SWuQjQ14vUHvKY/g+kOLxPWDfJLGDNQU4anFmsry8Ct9/ObTyH6c8YwTAvIed3cez/TjxcIZPJvFoobbO2G8VhvRe/oR/Awqzl4cCb6iDZEHmp0Dm+xTxomTyPnSZ33ALjjCAn65Q0fkwnoG5daO4TJWYLe7dCoe3Vz8KRi8+3JQvOBSbfx18Zi6dhtiTvTl48oKimorwnTyLhGirYr4rta8TRdwVEo7G6VuW+em/oaMXBfPJF60fsBdKZw/TAyGaVV+Uc/h2ZGWZ1oXOkuDO/R+2Hv8y/6ILt99Yqj8a+zgHNEAGVGepHn2hGtB6AkgUt8A99U76ceu3LA89wwwD8NHeMxDeTZlJ/3vBbnvCPklbQ3+JdaGNJWhd6Mfp7ghBH3EmYB7LiambXnnSr9l3Z8UZ+2nBYtzI2C8oG1ML6Lwx/bzRPam8UGhXsRrmMEyuEZzAS6GTCoqPSbubXhBvyq9C/8x+w8+YOz/weMB0gv1/SVzZ0DzXXqDbk3n8O36CF5JmifieNQlwzVYHdVhmS0HGAbpvyd5/u0zkXHFTqwf8s4OP8lmc4srMXgoo0Ul/VOQD/xa5Jixwxm+WtfihdGHq1VDmSwOPzZ21iXFPbC96YiiP6998DH0A4utfRczHpy3/2qrZoAtrYNn5v09Iy3lia/sn01i5eZOwnyMBs49wt2JNrai17wCCiHRUauxjYBOMsmw6L5oUrJm2PYEAz+UuRD176O3G5L+UKI3OAdlKP2pQE7WLPYZsI+6uzmLb0qsih/19Nt7x6Xw9sOJ4Vt/MjFQlklobEQ9MN9utl5JTYgnwxrd99/dVJwObP6oLkVnlkmAA8d/9crZZgZqqlh6Hk/N+2/kzxoRfAzAfcLBPyJtpa+ZWnbd0ZOH5ypXBCqI5uP0ZT1KWO2qeGum75Oz/Zio87qboPcNypdh4N3Gy+VqOVpvNgm8Lr8yfiFcVsDKRTY5aI5IkAfP2ZscposiRAoEna0+i5NtSKehrbZgshjZapqjNJjR60tqD6pM3LnOrVDIufprhQm3bXCWLwAPQorqT3AWegESxrF4i9rWaUc3bIKSDpvLyOX9QWMWTHvjtD7mk+kVGJ4VkeNgq8UjGiWgdsFA7hIWTUAAAAAF3y/1NOmQ1FgIbcWjIIM2zsoVytyqB91prCLAYUA4ps3a+Nv0ZxAtAeMlW78sbiubhjfk39BZ76fHXsE0QISypJO+KboDbmYt4ExzTglN3TYktvdttgPS7lpsYp2Io4Q7a4tgb99wKkHyXDnl4LNHoP1PmcnLwB0dkNKIRqrRcCYfFOMCYMYU86CWrZc9Sj933hKvDDaslt6AWre3xIXGx3pxRNTzjGLg0E+fvWeowg7NtoL6jnh5VXFQL3QLkp0PNJzmJ0g0TT4qmup3Az6J7ANEj880BiKmPNpDl5tYDVj64u/Jro+TriOwmZPY5Sv/vwVwvVcdS3ONvMUrYXx+rd7cBAEIi3KoFlY9nbQ8th5nwguvPl9S6iKYC217qFgP4LoF23ONp9ezlGzWYzE0ovc70VSpEZT3SSj67qMnZo/oFo+WFkRxNJx+K59+xlnQAnYt/zEqmBkAclFcYmA2rJ6VOKTXHF+0OZnBjrkN7zkNOcz6zTh8EHgNiV/hWpyBz+65LUDFztVtnH2s5CSpFNu1VaMkg+lf7pPj7iVpPbUdii84VGuMfpC+0ODBFcUSbTyJHkAedRQHShmK19pJ33qmgEOHEDf9OC9EMLfhd2NpZed2LVLASudxTbZn94GCdUGn9lkWmw8KcLfSIU57QsDC2Nb1tUtXrmKmHXwWBAlU4ZjmU/cOkoLtAgAPEvSWRXGb1wpvzBpftBFYdmAPFf4drjoGEgVqqrVfBeREQ0iPI3i9Dr9Enpl/T5rkoH8+vg515NvkwqHxOlVIm54Z7g/Et6IOFTVXB/AVFH9w5DF79c2sR5g+p1noF2pSqhcb+jaHUBY2Lt2gQAERYAMxznPwEMvUGbJGUaRWdCQL3x/4cO8lN9zoYNQp00WVKuLMjXDrexfEODXp3+iqIxzwcxdBE3CVSj691nQwIOfnebY3uBFes0XX7OFquN41f7MPiAqhr71IeNeLN+/cQZw+Yi9sfHcmxbjAm8GfrgN7Yx+AV5H8aq0tnIZ+VCu9S/8FfXw+hbo3aeHCiOvaobl2gnQIM4ChMnS3o9EH1DXJuSltrmRwyjZ95QYIaWf7j1lbITgXqtNcobGJXlKGg4TI4h9IqxbkqgC96P8DeAD0CjGSqP9C9+OxFCwu3Vn2UWot1+zf2TTZlqf4dBSDQ06u53LLlPhkciL7RGTWx6DBAZxU70nkqYf6CW35HV0tPXXXXxePi0r+BDognjpY2qQFSvQm8WW2sPpVDovM8ijbxUDeIPavqePvjtS4iUTwrcGRr8YhZNYx1uFKnh5yRO/4/oFHXg0B8wQEf09pNpGBgCzya7c89ldWmImbtMxg51jmbbzjYEeLX/dn7WdSFGVkwCMaGjtQKaKSKBy16DJn28jZFQTFBSr7hUhCNz5+qBqlkAtXiMe6qBWHeXMqZF2l/2jf7Wc1NF96BOhWz+CCXv5nYcxllVALggum68EdCxDER5UF04zMLrEHRUQAAAAAAAAAH4XzkHbSbAu64A4VwPORfMUNVcQrGnPXDnfvfSkYILbjKTUGQFz2Poy7gLlwOH1EZgFo6zRVAMqSIEMS8mdWjycoOj0QPxulV5a0UgwJKIuu5v3zvfcSgfqoR6bRwUfPPBsYEPNea/sKw9PFZCZM05DEUdkv2vK7uGhn9lQl6HS3rQeCjjHMxVyBFWXl/ZBjKDvgH+nhtZEtMAJSQuc1Z4SJCKovNX4ZOoz2kiJzr42XVccAgXujEzeOIuZRsEp0y8/5P0LSTBU1U96XsHGZ5JLs2WuxbZ07sO1EtaeW7Q6iDTz+BNSiOT99nFmHIzViOQWI+cb3NX3kqjx1fSb0Ue1mSzJiaNQZwN+XE8pumVKtf8pQo4lu4Vp3IE1k52T1HYEsrA+HdYmIokgXr6FMwj9Y797AW52v04N+1BLiT5Dmk2YL4/2qDYzP4MPPAm6HU/8h76sO+x04ygwNRx9IcprEANtexIXq9UKHAp9cIy0ygGzg7NXhRzI/DPafOr/ag867nBEDjCl8vyefHixCpPZVaVqtBXpPpq8o4gYNEQsgAHRfJVzC225QL69/va2V8Po4vl4G1eB65J45SXqMLz6bAx8gw8O+BK6hKu9u85LLNPdFhpy6OY2UUGUm1Vg28INzWyr4XEUI8XkCzY9lkjGN6krdKOlFsI5ZOV0Qtk5KHGfQ2JpjG405NIsfay2JAnlbHd6MFkIqEWAepCdhFMmDPZC/5ZDx/3Uq4fkoVtSnO7wNeN3howYe422MDXNKbkdGZmrPLNzJQJgIIvn/AVZPcIk97bQDVBw64/dV2nfa/MHD728PLKvsMVmxHd+/LPd0iGk/wTvwCUuQZ77XZuBfhzDcLRTEbmX7Q4ZBF79N73VYy5H7NVPlqX8Ai6C16n+cqNOo9+kuaMHKtG8F9nIH8XP/mnPKEaiEIFTO5LxRIhKv2oZRbQN8vv/UoqygmaqTj+ZwSZYZ6cqNILyFK0JheGwq1AaBg+up88K/ylVjk0c7kwxnFSYChYm3P0oGWd7kLX7hyZ2KxRVYwufyW1MbBKlINyx58eyxBKOE6kogUMmtAVgR/6rFWg9E/IjRVjSp+2WcrZzCHN0l1t5DNXcjuyO+PjTn+TciUwXPewlHgSj3gvrcSSdwQGApHx+7zxsnnMtNQdhGi/yj/G2l/rZyWlPIFfu4osnM1nBX1Haewwhu9GyyPDiUXPzHQOsrb9jIAekL745t/RYXFf8TONv6+5JCTsn2R3ZHfH05wKbrBRHbQiXPDyiVve2B/jlyHnQZA8JIWAcBqXpHJLRSdeb57hQbwi1DcnzAvfdsHL79w5WuTRNXegLPob6kVBs09bZfQxTd07by18iM5dd3MthV8NTTTYu0j7vFGZdrCnuwMqPEqEWYeEJ6dNSbN3Yfiezw7HrPPOp97NLXe20GpaF7buSudxFNTtxVzQVL9vo+6k/GjfPT2+HxqoO2auP+q7BAI/6C63EZp+6L5WA5Y6n9COoUWBkgZNghXnUvfY0tRak3AfmWWSqH8zdVjcXIUmxNwWRkvgepb7t+DYfGJsG/QsNal3UYD0YNFzhzl58tJsr89vaJi9BxMamtJUyJNMV8CSF8Qnn4OGhgXVIrNeuxadLtGWHltXItLAF12MKqCRVIMOLmXX815tOX/Vipz8owVeAjJq2cr807t7lyIDp3ailALmIOgffOvnpW9XOg7bJ6IqjZQu5/aAPJj8ogYoWBapu8/yuwSwIoDTG5de7ZJ6oPUyJOkEtqSNIkA+DoloeQSJYN18tXfiur87WpY9ZmgpN9NA122RvgtjOeAc0NM8+Aq9TDcphvkfmBFy4kbFh1jOC5bScSvHLok0gW3TxfR1uRqoOKZPr/y3DhzleR/NFsrWfQ9IFMql27kaKCmC8OAsSvnLR3+nZaSRR0n+s8YgZsibukJ9LerKM/+hBN/e75YhGWcaA1uXTcDIv+mvUfXKVPxNOKnbyY2hgRM/r6Wzf2c01dfHycLB+c02Je9NZt/kSh3HWGYTtnHiO1lJAVp5HAHlFdSvn/4zvnAQ47u+37e7C7cnyicfxyo+mS3PKUcTE/QiUoACtf+cXF8e8kmx7Rvwq7OzGmrt1j5pZlfQTRnQlHZev6uKUE1HTHHMNWoq3o9C0YhC78os7LuD8g1XYc6FDV1rGuGjaZZQx6EDbNCG5NaKCC1LBz0oFZn7w886O1a2+4R0ElvIGtoUXeT5fry8Tr/cwvfYNxRvvSb1Naa4+OpXWMOKx5cMdt6ajqPBJWvhbTdjJN1m2Kt0eaAf7FTPB6GEx6xJAZW697mp2ArPBf1HgIcAohgnklW1hiY43wFE9W8lciIwCkmkCeMSzBLTuj/+tjVNyq+P98pAUJglG/cxQQ8bursKlX1hHcqvwmSGlZuoE/3ilrcuNPhPl5HxVPCwX41PHsBnHj8OHqsKFAbbjHgminJBTTmqtlHfq5YmKn8owEEc12natvKhQ4kK2gZfkIkCPC4WRrIcpT05+5fSsaZGyKG9vWgadOG35xnuGjvkWKquzGo8RlfIR5dze7/sJL7l2vbiopXhaaR7AXi8Os1xwiJ9ZWYrFKiNONwUPUSdfaHX7fnHVcDwQqeM6w9BxsmPtSmwZdnJNqfW2QrF8p/kkgMvqsHnqf9M+uLzJ/NBbOMWhT6q01ZPIR3b7KR1HQrcO4xpwjT8wh41goQcjXhA/KvgyPtab/t9sGdPSEXQb8zULlNq7q3cCbkXzzmnKktj2/2AJkj2bW+CVkoyh6z0sIUADsNpcK3/ilhvwhLxc7v1UrBWo9f/jCcqfKjT+T+AnR0IPLsTHQOenBgmY070ks4bIm/nIchqE07ejnC7TxZJs48XOsS9X2VoEkjST+0cXTQ/MbF1LyeuyDcRAv2+Qw4BjuHIY0fvPjbTVlIo17td5NtRoPf8EGcrAAo+qKoxSOHX2IZ4brBD3idkXBJYjL/RVl1zmUDgHuMUb1RHQfAHdLbpoEyWGvdRrWZp5Agxgw6gLi0zH0NaUkQGIkajKs0fkfCoVeVkt1XFXlVwcSDVo5k4k3UMHS6R7O+dHY0Qube4DVPRXeF591q7uTRagcFb9yICNria/wF6ckrRnakcHndpCU9hEdo8+ZwH/sI3IOY7rrsa/yPnSULDX4j3uFSfcYopR6mZ+GOrlm/vodUzd1EfjSwI4nAsMyBR44brQFEdnoQMvzeC7DjWo4LBVNVdH7RqRSlk0sNp8LDpJK6FZirZwzcDvCY8JvNb1a1/lNlTPcEUmSr3J29po6u6mO2LsZdKGJmJpHPinxB9nhft9/Pb5kNVx/LqH7zmGYhpO9evh5tJCVUkNo5onPrtGR/s5xZzalqa2nCxFkv6+/YTrug+E4m2KdmzTgUY44vPeJbFMSoAaza9aDwUcWAq4bMpr9w9nz8JFgTEZCvGoKXutgcsrjKtn3+2Jdow2KbkF9Ncz0CaovNYfETb4POqSNikI9KCZCgg3kfDnz+rRRCc0Mik5IhAhxYdjLMtpTMPRyHDNfvLb0FZUED/9jOF9t97//qAsH2GPvmDWwk7Cbzt8O6XaioS9fLnP/bV/osXB0WA2dU6qbGWwGYxuwuy9Dpra18U3xI/jC2DoM0JmlhVfHFxJPLUfmdMSoEOUe2gJIXEE2dkcJzJV8wxdfvTB1DfuYt11sIcIybakFgeTOGxElMHANRPB2TBn9nUMMwJF6UiTsSThKZJXHLZvmQ1dGWRJV/WEMLWsSKIf7VZnFp83sM2KUoTXmTB/XSJQ/+GruwIwUsWGrCK6S5UIs/L8mqhsBGKOHiZ2zD5ciRRz9WUnx6AvARafYBMeAAAE4I9/8+ruutAgdNDDmDuyf887CZhesjH5nnx3z27PNx/kJRphhwWavC/0UC2cTtAIPJYN8ilfjBTrwcz8SxFsUcIBE8O/F7oAgO1yGqwJt0775RxMQ24XxLfRHB0rFyNAMu6x6l8KZMWpVbec7RwD1r0bpXwd14XB4quButSYQ2xqQcqjy4lJQvmGsGK5XXWeDWjV3i1s6a4neOv1QQttJ6zwXeCeIipHcJkRmGxNl5knVrtuWqNjE6vaUMU6c4KcFOCnBTgpwU4KcFOCsE/ACC6AagGwTeEn9NkjlEP/z87IvfLQYmkSTvaliPx4bEAU9lKMubomguJMkPhkMENz6NdSXJbCekKWzkVnS7OHpcZCOMx+3Jl0bMH5EqYEzsJ81ePa/FWufdym6eVE3nh4TeeHhOXgije8SrXjyyvf7zXhbWr0oAAAAAAAAAAAAAAAAAAAAAAAAAAAAdF1ALDnhzw54c8OeHPDnhzw54c8OeHPDnhzw54c8OeHPDnhzw54c8OeHPDnhzw54c8GAAAA=>
[image3]: <data:image/webp;base64,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>
[image4]: <data:image/webp;base64,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>
[image5]: <data:image/webp;base64,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>
[image6]: <data:image/webp;base64,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>
[image7]: <data:image/webp;base64,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>
[image8]: <data:image/webp;base64,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>
[image9]: <data:image/webp;base64,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>
