# ASP.NET

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
- Pages were interpreted at run time and HTML was rendered and delivered to
  client
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

- Open source, cross platform framework released in 2016, works on Windows,
  macOS or Linux
- Architected for testability
- Higher performance than ASP.NET
- More "modern" approach to web development
- The future of .NET web development

## ASP.NET Version History

- ASP.NET 1.0 – 2002
- ASP.NET 1.1 – 2003
- ASP.NET 2.0 – 2005
  - Master pages (Templates)
- ASP.NET 3.0 – 2006
- ASP.NET 3.5 – 2007
  - LINQ
  - ASP.NET AJAX
- ASP.NET 4.0 – 2010
- ASP.NET 4.5 – 2012
  - MVC
- ASP.NET 4.6 – 2015
- ~~ASP.NET 5.0~~ ASP.NET Core 1.0 – 2016
- ASP.NET Core 1.1 – 2016
- ASP.NET 4.7 – 2017
- ASP.NET Core 2.0 – 2017
- ASP.NET Core 2.1 – 2018
- ASP.NET Core 2.2 – 2018
- ASP.NET 4.8 – 2019
  - Last version with full .NET Framework
- ASP.NET Core 3.0 – 2019
- ASP.NET Core 3.1 – 2019 – LTS (Long Term Support)
- ASP.NET 5.0 – 2020
- ASP.NET 6.0 – 2021 – LTS
- ASP.NET 7.0 – 2022
- ASP.NET 8.0 – 2023 – LTS

## .NET Standard

- The .NET Standard is a formal specification of .NET APIs that are intended to
  be available on all .NET implementations
- The motivation behind the .NET Standard is establishing greater uniformity in
  the .NET ecosystem
- [.NET Standard](https://docs.microsoft.com/en-us/dotnet/standard/net-standard)

## ASP.NET Framework

- Collection of technologies
- ASP.NET is not a language, it is a framework to develop web applications and
  web services
- Many supported languages and tools, main languages include C#, VB.NET and
  JavaScript
- Mature and considered feature complete

## ASP.NET Core

- Newer, portable version of ASP.NET that also runs on Linux and macOS
- Some features of ASP.NET were not brought forward to ASP.NET Core
- WebForms, WCF, and more
- All future development will take place here

## Mono/Xamarin

- Mono is an open-source project to create a .NET Framework-compatible software
  framework, initially released in 2004
- Mono’s goal is to be able to run Microsoft .NET applications cross-platform
  and bring better development tools to Linux developers
- Mono can be run on Android, most Linux distributions, BSD, macOS, Windows,
  Solaris, PlayStation, Wii and Xbox
- Mono was initially developed by Ximian
- In 2011 all Ximian staff working on Mono were laid off and formed Xamarin
  right away
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
- Rich UI controls included – datagrids, charts, AJAX, and more
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
- Content is typically JavaScript Object Notation (JSON), can also be eXtensible
  Markup Language (XML) or other schemes

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
  - Render the UI as HTML and CSS for wide browser support, including mobile
    browsers

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

- Standard: Buttons, text boxes, labels, …
- Rich: Calendars, wizards, …
- Validation: Client-side validation
- Data: GridView, Repeater, …
- Many more…

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

- ASP.NET simplifies web programming by automatically maintaining state on page
  when posting back to the same page
  - Known as a postback
- Most other web development environments force you to manage state yourself
  - Non-trivial
- State is not stored on the server
  - Good for scalability
  - Implemented using a hidden HTML field: \_\_VIEWSTATE

## Master Pages

- When creating a site with many pages, master pages provides an easy way to
  control the look and feel of all pages
- A master page is a "template" that will frame how all pages on the site look
- Master pages can contain controls and code like any other ASP.NET page
- A web site may have multiple master pages
- A style sheet can be linked to any page, but linking to a master page causes
  its effect to spread to many pages

## Content Placeholders

- `ContentPlaceHolders` define regions for content in an ASP.NET master page
- This is where content will show up on actual pages
- `ContentPlaceHolders` should be empty on the master page
- `ContentPlaceHolders` will contain HTML, controls, etc. on content pages

## SqlDataSource

- The `SqlDataSource` data source control represents data in an SQL relational
  database to data-bound controls
- Can be connected to a variety of controls including the `GridView` and
  `DropDownList`
- Uses a connection string to connect to the database

## Connection Strings

- Connection strings should be stored in web.config
  - More secure
  - If it needs to change, only one place
  - Better performance due to connection pooling
  - Default behavior
- SQL Server connection strings can be specified in a variety of ways, the most
  common approach has three required parts:
  - Data Source (where is the database server)
  - Initial Catalog (which database is being used)
  - Authentication Credentials (how does the application login to the database)

## GridView

- A GridView renders as a table to the end user
- Like many ASP.NET controls, clicking \> will render a task window
- Selecting Edit Columns… task will allow control of what columns are displayed
  and how they are displayed

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
- The server implementation listens for HTTP requests and surfaces them to the
  app as a set of request features composed into an HttpContext
- Microsoft is working hard to make Kestrel fast
  [TechEmpower Web Framework Benchmark](https://www.techempower.com/benchmarks/#section=data-r21&hw=ph&test=composite)
- Kestrel can be used by itself as an edge server processing requests directly
  from a network, including the Internet

![][image3]

- Kestrel can also be used with a reverse proxy server, such as Internet
  Information Services (IIS), Nginx, or Apache
- A reverse proxy server receives HTTP requests from the Internet and forwards
  them to Kestrel

![][image4]

## NuGet Package Manager

- Packages are compiled code that we add to our projects to increase
  functionality
- Many are written by Microsoft, many are written by 3rd parties
- You can see what packages are installed by right-clicking the project and
  selecting Manage NuGet Packages, you can also search for and install new
  packages from the browse tab

## Package Manager Console

- If the name of the package is known, it can also be installed from the Package
  Manager Console (**PMC**) using the Install-Package command
- The PMC is accessed from the Tools / NuGet Package Manager menu
- The PMC also has a variety of commands for working with the project, databases
  and more

## HttpContext

- Encapsulates all HTTP-specific information about an individual HTTP request
  such as:
  - Request, Response, Server, Session, Item, Cache, User's information like
    authentication and authorization and much more
- Every HTTP request creates a new object of HttpContext with current
  information

## ASP.NET Core Host

- ASP.NET Core apps configure and launch a host
- The host is responsible for app startup and lifetime management
- At a minimum, the host configures a server and a request processing pipeline
- The host can also set up logging, dependency injection, configuration and much
  more

## CreateDefaultBuilder

- The CreateDefaultBuilder method of the Host class is responsible for the
  following:
  - Configures Kestrel server as the web server
  - Sets the content root path
  - Loads host configuration
  - Configured logging
  - And more

## Startup.cs

- The Startup class typically has two methods ConfigureServices and Configure
- ConfigureServices is an optional method called before Configure and sets up
  classes for dependency injection such as a database context, identity and many
  more
- Configure specifies the middleware pipeline

## Middleware

- Middleware is code that gets called in between an initial request and the
  response to the client
- Each component:
  - Chooses whether to pass the request to the next component in the pipeline
  - Can perform work before and after the next component in the pipeline

![][image5]

## ASP.NET Core Middleware

- Middleware delegates are configured with Run, Map and Use methods
  - Run adds a terminal (or final) middleware delegate to the application's
    request pipeline
  - Map branches the request pipeline based on matches of the given request path
  - Use adds a middleware delegate defined in-line to the application's request
    pipeline

# Model View Controller

- **MVC** is a design pattern
- **Model** contains domain classes and business logic
- **View** displays models to the user using HTML, JavaScript, CSS, etc.
- **Controller** handles user input, builds the model and passes it to the view
  to display the model

## Standard MVC Architecture

1. Incoming request directed to **Controller**
2. **Controller** processes a request and forms a data **Model**
3. **Model** is passed to **View**
4. **View** transforms **Model** into an appropriate output format
5. Response is rendered

![][image6]

## ASP.NET MVC

- Provides another approach to ASP.NET development instead of Web Forms with the
  following benefits:
  - Available in both .NET Framework and .NET Core
  - Improved testability (ideal for TDD \- Test Driven Development)
  - Removes ViewState (\_\_VIEWSTATE) and it's overhead
  - Complete control over HTML markup
  - Better separation of concerns
  - Embraces web standards (HTML, JavaScript, CSS)
  - Open Source code available for modifications

## ASP.NET Core MVC Applications

- ASP.NET Core MVC applications are Console applications which start in the Main
  method of the Program class
- Startup code is placed in the Startup class. It configures:
  - Request Pipeline
  - Default Route
  - Logging
  - And more

## ASP.NET MVC Routing

- URLs in MVC do not map to a specific file but instead to a specific controller
  action
- Routing Engine parses URL, looks up the corresponding controller action and
  directs the request to it
- The action corresponds to a public method in the specified controller
- A Map Route provides a name, URL pattern and default values
- Configuration of the mapping happens in the Configure method of the Startup
  class
- Default Route: {controller=Home}/{action=Index}/{id?}
  - HomeController is the default controller
  - Index is the default action method
  - The id parameter is optional
- By default URLs map to controller/action/id
- https://my.shopping.app/Products/Electronics/5 would:
  - Instantiate an instance of the Products controller
  - Invoke the Register method of the controller
  - Pass the id parameter set to 5
- Provides clear and simple URLs which is great for Search Engine Optimization
  (SEO)
- HomeController
  - public Index()
- OtherController
  - public Index()
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

## Passing Data from Controllers to Views – Part 1

- ViewData
  - A dynamic dictionary object (key/value pairs) that can be passed from a
    controller to a view
  - It can contain nearly any type
- ViewBag is a wrapper for ViewData
- The two can be used interchangeably

## Route Customization

- Any controller method can be customized to respond to a different route
  pattern than the default
- A Route attribute is placed before the method to indicate the pattern

## Route Constraints

- Route constraints can be placed on the route place holders to limit the URLs
  that the route will match
- **Example:** {controller=Home}/{action=Index}/{id:int?} indicates that the id
  parameter must be an integer, but it is still optional
- A few other route constraints: double, datetime, min(value), range(min, max)
- These constraints should not be used for input validation

## ASP.NET MVC View

- Responsible for displaying the model to the user
- cshtml files are used and can be considered templates with static html and C#
  code (prefix with @ symbol) for dynamically inserting the model’s properties
- Use Razor View Engine to display the View

## Razor Syntax

- Razor is a markup syntax for embedding server-based code into web pages
- The Razor syntax consists of Razor markup, C#, and HTML
- Consists of implicit Razor expressions, explicit Razor expressions and Razor
  code blocks
- @@ to escape the @ symbol
- Comment Syntax: @\* comment \*@
- @ Implicit Razor expression
  - \<p\>Today: @DateTime.Now\</p\>
  - Renders as HTML, spaces not allowed (_Except when used with await_)
- @() Explicit Razor expression
  - \<p\>Week ago: @(DateTime.Now \- TimeSpan.FromDays(7))\</p\>
  - Renders as HTML, spaces allowed
- @{} Razor code block
- Doesn’t render as HTML, semicolons required @{ var name \= "Bob Loblaw"; var
  len \= name.Length; }

- Then in the same file:

\<p\>The length of @name is @len bytes.\</p\>

## IActionResult

- The most common return type for Controller Actions is IActionResult
- It is an interface that provides for a very flexible set of possible return
  types including
- ViewResult, ContentResult, RedirectResult, JsonResult and many more

## URL Helpers

- Simplifies HTML coding in Views by using metadata and annotations
- @Url.Action("Action", "Controller")
- Many more

## Layout

- Special Razor pages that help define the layout of the overall site
- Similar to Master Pages in Web Forms
- By convention stored in a folder named Shared
- @RenderBody and @RenderSection are placeholders for content on individual
  pages

## Passing Data from Controllers to Views – Part 2

- A Model property
  - Typically a class
  - Cleaner and clearer
  - Support IntelliSense

## Project Structure \- Folders

- Properties contains launchSettings.json file
- launchSettings.json contains some environment settings including which IP
  ports to use during development
- wwwroot contains static files, such as HTML, CSS, images and JavaScript
- Controllers contains the MVC controllers
- Models contains the model classes for the app
- Views contains a folder for each controller, which contains the views for that
  controller
- Views also contains a shared folder that contains views used throughout the
  app
- Views also contains \_ViewImports.cshtml and \_ViewStart.cshtml which are used
  to help structure all views
- appsettings.json is the project configuration file which contains settings
  related to logging, connection strings and other items
- Program.cs is the file which starts the app
- Startup.cs configures the app’s services and request processing pipeline

## Data Annotations

- Data Annotations are rules applied to model properties
- MVC will take care of enforcing them and displaying appropriate messages to
  users
- Selected annotations: \[Key\], \[Required\], \[Range\], \[Display\],
  \[DataType\]
- [View complete list of annotations at docs.microsoft.com](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-5.0)

## MVC Scaffolding

- MVC Scaffolding uses a Wizard that can automatically generate much of the
  standard code for our Controllers and Views
- VS Scaffolding is a great example of Code Generation tools

## HTML Helpers

- Similar to URL helpers
- @Html.DisplayNameFor – plain text to display the read-only field name
- @Html.DisplayFor – figures out the best HTML5 for read-only display of the
  actual field value
- @Html.ActionLink – action link with anchor tag

## MVC Controller Actions

- Typically a controller will have the following actions (or methods)
  - Index which is the "first" page that shows a list of any existing items, and
    allows access to the other actions
  - Details which shows one item in full
  - Create which allows for the creation of a new item
  - Edit which allows for the editing of an existing item
  - Delete which allows for the deletion of an existing item
- The Create, Edit and Delete actions are comprised of two methods each, a get
  and a post

## Controller Method Attributes

- **HttpPost**
  - Typically two controller actions are required for modifying data (edit,
    create, delete)
  - One controller action for the HTTP GET displays the blank form (or the
    existing data)
  - The other controller actions for the HTTP POST validates the data and
    updates the data store if necessary
- **ValidateAntiForgeryToken**
  - Use for HTTP POST to ensure that the data is originating from the correct
    user to stop Cross-Site Request Forgery (CSRF) attacks

## Model Binding

- Request fields are mapped to controller action parameters automatically
- Can be almost any data type including objects
- Parameters can be automatically validated, if any errors are found they are
  added to the ModelState
- Sources of model binding values...
  - Form post values
  - Route data / URL parameters
  - QueryString parameters
  - User cookies

## ModelState

- When updating the model, the ModelState property (of the controller) indicates
  if the model is valid
- The IsValid property checks all properties of the model to ensure correct data
  type, appropriate values and any other specified attributes are correct
- There are other useful properties, such as ErrorCount, Keys, Values, etc.

## Tag Helpers

- Similar to HTML Helpers but rely on attributes for well known HTML tags
- Makes views look more like standard HTML
- Easier to read for HTML developers \<label asp-for\="DateSeen"
  class\="control-label"\>\</label\> \<input asp-for\="DateSeen"
  class\="form-control" /\> \<span asp-validation-for\="DateSeen"
  class\="text-danger"\>\</span\>

# Introduction to ORM (Object-Relational Mapping)

- There are major differences between relational and object oriented models
  including:
  - **Types** (e.g. varchar vs. string)
  - **Identity** (keys vs. address)
  - **Relations** (foreign keys vs. references)
  - **Many to Many** relations require linking table in SQL
  - **Inheritance** and **Polymorphism** are not directly supported in SQL
- **Object/Relational Mapping** (ORM or O/RM) maps between these two models
  allowing us to work in the object oriented model without having to worry about
  the underlying data structure
- ORM tools allow us to easily save our objects to the database and load them
  from the database
- ORMs typically provide support for the full set of CRUD operations
- Why would one use an ORM?
  - To save time and money (time to market)
  - To focus on the business logic rather than database/persistence logic

## Introduction to .NET ORM

- First major ORM for .NET was **NHibernate** which is an open source solution
  which is still around
- .NET provided LINQ to SQL as its first ORM
- .NET now includes the **Entity Framework** which effectively replaced **LINQ**
  to **SQL**
- **Entity Framework** can be used with many different databases including
  _MySQL_, _Oracle_, _SQL Server_, _SQLite_ and more
- Complete list of
  [EF Core Database Providers](https://docs.microsoft.com/en-us/ef/core/providers/index?tabs=dotnet-core-cli)
- [Entity Framework alternatives](https://stackshare.io/entity-framework/alternatives)

## Entity Framework

- Microsoft’s latest ORMs are **Entity Framework 6** (EF6) and the lighter,
  portable **Entity Framework Core 5** (EF Core)
- **EF6** is more mature and slightly richer in functionality than **EF Core**
- However, just like .NET Framework, future development will primarily be with
  **EF Core**
- Good tutorial site:
  [EntityFrameworkTutorial](https://www.entityframeworktutorial.net/)

## Performance Considerations

- In general **EF** generated database access code performs very well, although
  perhaps not quite as well as native SQL and stored procedures
- For the vast majority of applications, **EF** performance will be more than
  adequate
- On the occasion when **EF** performance is the bottleneck, it is still
  possible to write native SQL, stored procedures, views and indexes and
  configure **EF** to use them

## EF Workflows

- **EF Core** has two workflows and the choice would be depend on the situation
  - **Code** first can be used for a **new** application
  - **Database** first makes sense if a database already exists
- **EF6** also has a **Model** first workflow that is similar to **Code** first,
  but uses a _graphical editor_

## Important Classes

- Main Context container class provides access to all of our entities
  - Derives from DbContext
  - Uses DbContextOptions for configuration information
    - **Database provider**: UseSqlServer, UseSqlite, etc.
    - **Connection string**
  - Contains DbSet\<T\> members for our strongly typed entities (AKA tables)
  - Provides support for change tracking to our entities
- Entities with the DbContext API are **POCO** (Plain Old CLR Objects) and
  therefore are persistence ignorant

## MVC EF Scaffolding

- **Scaffolding** in MVC can generate most of our standard CRUD code
  automatically from a model
- **Scaffolding** will install necessary packages, build the **Controller** and
  **Views**, register the database and configure its connection

## Asynchronous Programming

- Makes heavy use of asynchronous code, lambda expressions and interfaces
- To improve efficiency, the scaffolder makes extensive use of
  [asynchronous programming](https://docs.microsoft.com/en-us/dotnet/csharp/async)
  which is helpful for I/O bound code such as file, database and network I/O
- Two C# keywords to note for asynchronous code:
  - **async:** marking a method as making an asynchronous call
  - **await:** relinquishes the current thread to the thread pool while the
    awaited (asynchronous) task operation completes
- Methods that use async must also return either a:
  - Task\<return type\> \- generic object for methods that would otherwise
    return a plain return type e.g. Task\<string\> instead of string
  - Task – object for methods that would otherwise return void
- Methods that do the asynchronous I/O bound processing by convention have
  method names that end in Async

## Dependency Injection

- **Dependency Injection** (DI) is a design pattern
- A dependency is an object that another object depends on (e.g. EF Data
  Context)
- ASP.NET Core MVC controllers request dependencies via constructors
- DI makes apps easier to test and maintain because they are more **loosely
  coupled**

## Ensuring the Database Exists

- The EnsureCreated method of the Database property of the DatabaseContext
  ensures that the database for the context exists
- If it exists, no action is taken
- If it does not exist then the database and all its schema are created
- Optionally, the OnModelCreating method can be implemented to seed the database
  when it is being created

## Entity Framework

- Changes to entities are tracked automatically by the context
- We can easily save all changes to our entities using the SaveChanges method of
  our DbContext derived context
- We can query our entities using **LINQ** to Entities using **Query Syntax** or
  **Method Syntax**

## Connection Strings

- **Connection strings** in .NET Core are the same as in .NET Framework
- Instead of the web.config file, they are stored in appsettings.json

## SQLite

- **SQLite** is a small, fast, self-contained, high-reliability, full-featured,
  SQL database engine
- **SQLite** is the most used database engine in the world
- **SQLite** is built into all mobile phones and most computers and comes
  bundled inside countless other applications
- [SQLite](https://www.sqlite.org/)

# Status Code Pages

- By default, an ASP.NET Core app doesn't provide a status code page for HTTP
  error status codes, such as 404 \- Not Found
- When the app encounters an HTTP 400-599 error status code that doesn't have a
  body, it returns the status code and an empty response body
- To provide status code pages, use the status code pages middleware

## \_ViewStart.cshtml

- Code that needs to run before each view or page should be placed in the
  \_ViewStart.cshtml file
- By convention, this file is located in the Views folder
- The statements listed are run before every full view

## \_ViewImports.cshtml

- Views and Pages can use Razor directives to import namespaces and use
  dependency injection
- Directives shared by many views may be specified in a common
  \_ViewImports.cshtml file

## Hierarchy

- \_ViewImports.cshtml, \_ViewStart.cshtml are both hierarchical
- If a \_ViewStart.cshtml (or \_ViewImports.cshtml) file is defined in a view or
  folder, it will be run after the one defined in the root of the Views folder

## Custom Validation

- Recall the list of
  [possible annotations](https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-5.0)
- To be consistent with the DRY (Don’t Repeat Yourself) principle, validations
  should be specified in the Model
- The custom validation class inherits from ValidationAttribute and overrides
  the IsValid method

# Introduction to Testing

- Testing is a key part of developing any professional application
- Testing has traditionally been a manual, tedious and expensive process
- Agile development methodologies place even greater emphasis on testing than
  traditional approaches
- Agile development requires lots of repeat (regression) testing so the need for
  test automation is very high
- Visual Studio has extensive support for building and executing automated tests
- ASP.NET MVC, Razor Pages and Web API were designed to be very testable

## Testing Frameworks

- Visual Studio provides an open testing architecture that supports several
  different test frameworks including
  - **Nunit** – Original JUnit port
  - **xUnit** – Updated version of **Nunit**; Emerging as preferred testing tool
  - **MSTest** – Microsoft’s unit testing framework
- The principles for these unit testing frameworks are very similar, so
  transitioning from one to the other is straightforward

## xUnit

- Test actions (methods) are annotated with \[Fact\] or \[Theory\]
- Facts are tests which are always true
- Theories are test which are only true for a particular set of data
- Initialization code that runs before each test is placed in the test class
  constructor

## xUnit Instructions

- Add a Test Project to your solution
- Add a reference to the project you would like to test to your test project
- Implement your unit test
- Build your unit test project
- Open Test Explorer and Run the test
- Review the results and modify as necessary

## Unit Testing Web Controllers

- Automated testing depends on known data
- Testing against an actual database is problematic because the contents of the
  database are expected to change over time
- A good alternative is to use a **fake** database
- Microsoft.EntityFrameworkCore.InMemory NuGet package provides an in memory
  simulation of the database context

## Naming Tests

- By convention, the name of a test should consist of three parts:
- The _name_ of the method being tested
- The _scenario_ under which it's being tested
- The _expected behavior_ when the _scenario_ is invoked
- Examples: Index\_NoInput\_ReturnsMovies or Details\_MovieId\_ReturnsMovie

## Unit Tests

- Unit Tests normally consists of three major steps:
  - **Arrange** – create and initializes the object(s) to test
  - **Act** – do something with the object(s)
  - **Assert** – check that the object(s) or result changes to the expected
    state

## Test Explorer

- Test Explorer is Microsoft’s dedicated Test Tool
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
- It is best practice that test shouldn’t be ordered
- If it is required to have tests run in a particular order, you must implement
  the ITestCaseOrderer or ITestCollectionOrder interfaces

## Live Unit Testing

- As you are developing an application, Live Unit Testing automatically runs any
  impacted unit tests in the background and presents the results and code
  coverage live in the Visual Studio IDE in real time
- As you modify your code, Live Unit Testing provides feedback on how your
  changes impacted existing tests and whether the new code you've added is
  covered by one or more existing tests
- Only Visual Studio Enterprise supports this
- This will gently remind you to write unit tests as you are making bug fixes or
  adding new features
- ✅ Code passes test(s)
- ❌ Code fails test(s)
- ➖ Code isn’t covered by a test(s)
- \* Icons here are not the same as Visual Studio.

# Database Migrations

- A data model changes during development and gets out of sync with the database
- You can drop the database and let **EF** create a new one that matches the
  model, but this procedure results in the loss of data
- The migrations feature in **EF Core** provides a way to incrementally update
  the database schema to keep it in sync with the application's data model while
  preserving existing data in the database
- Database migrations are handled through the **Package Manager Console** with
  two commands:
  - **Add-Migration**: scaffolds a migration script for any pending model
    changes
  - **Update-Database**: applies any pending migrations to the database

## Maximum Length

- Configuring a maximum length provides a hint to the database provider about
  the appropriate column data type to choose for a given property
- Maximum length only applies to array data types, such as string and byte\[\]
- Avoiding varchar(max) when possible, will produce more **efficient SQL**

## Relating Entities

- **Navigation property**: a property defined on the parent and/or child entity
  that contains a reference(s) to the other entity(s)
  - **Collection navigation property**: navigation property that contains
    references to many related entities
  - **Reference navigation property**: navigation property that holds a
    reference to a single related entity
- A List\<T\> can be used on the **parent** entity
- A ForeignKey attribute is used on the **child** entity

## Loading Related Data

- With navigation properties, **EF Core** allows the model to load related
  entities
- There are three common O/RM patterns used to load related data:
  - _Eager_ loading (achieved with Include method)
  - _Explicit_ loading
  - _Lazy_ loading (_default_)
- **Eager** loading means that the related data is loaded from the database as
  part of the initial query
- **Explicit** loading means that the related data is explicitly loaded from the
  database at a later time
- **Lazy** loading means that the related data is transparently loaded from the
  database when the navigation property is accessed

# Scaffolding

- The scaffolder can reverse engineer a model from an existing database by using
  the Scaffold-DbContext command from the **Package Manager Console**
- This is the **database first** workflow

## Scaffold-DbContext

### \-Connection \<String\>

- The connection string to the database
- The value can be name=\<name of connection string\>
- In that case the name comes from the configuration sources that are set up for
  the project (appsettings.json)
- This is a positional parameter and is required

### \-Provider \<String\>

- The provider to use
- Typically this is the name of the **NuGet** package, e.g.
  Microsoft.EntityFrameworkCore.SqlServer
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
- If this parameter is omitted, database names are changed to more closely
  conform to C# name style conventions

### \-Force

- Overwrite existing files

## Data Annotations vs. Fluent API

- Data Annotations example \[Required\] \[StringLength(160)\] public string
  Title { get; set; }

- Fluent API is an alternative way to configure the data model;
  entity.Property(e \=\> e.Title) .IsRequired() .HasMaxLength(160);

## Running

- Visual Studio offers 3 ways to run your site
- **F5** offers full debugging which allows interactive breakpoints
- **Ctrl+F5** runs without debugging, which runs slightly faster
- Both approaches permit making changes and refreshing browser without
  relaunching
- The 3rd way is to use the dotnet command in the **Package Manager Console**
  (PMC)
- dotnet watch run \--project projectname
- dotnet watch will automatically refresh the browser whenever a file change is
  detected in the project

## No Tracking

- The AsNoTracking method returns a new query where the change tracker will not
  track any of the entities that are returned
- If the entity instances are modified, this will not be detected by the change
  tracker and SaveChanges() will not persist those changes to the database
- Disabling change tracking is useful for _read-only_ scenarios because it
  avoids the overhead of setting up change tracking for each entity instance
- You should not disable change tracking if you want to manipulate entity
  instances and persist those changes to the database using SaveChanges()

## Form Tag Helpers

- Tag Helpers enable _server-side_ code to participate in creating and rendering
  HTML elements in Razor files
- The asp-route-\<Parameter Name\> attribute, where \<Parameter Name\> is added
  to the route values

# Paging

- If the Index method of a controller returns many items, unlike Web Forms,
  ASP.NET Core MVC does’t have a built-in ability to paginate them
- A PaginatedList class will be used to implement this functionality

## Generic Type Parameter

- In a generic type or method definition, a type parameter is a placeholder for
  a specific type that a client specifies when they create an instance of the
  generic type
- This allows the method to be flexible and useful in a variety of scenarios
- In the method signature, it is denoted as \<T\>, meaning any type

## IQueryable Interface

- Provides functionality to evaluate queries against a specific data source
  wherein the type of the data is known
- Similar to IEnumberable in that it allows the creation (and consumption) of a
  list of stuff, but from a **LINQ** source
- This interface itself implements IEnumerable

## LINQ

- **L**anguage **IN**tegrated **Q**uery is a query syntax in C# that allows
  retrieving data from a variety of data sources
  - Object collections
  - ADO.NET DataSets
  - XML documents
  - Entity Framework
  - SQL databases
  - Other data sources

## LINQ Methods

- Skip bypasses a specified number of elements in a sequence and then returns
  the remaining elements
- Take returns a specified number of contiguous elements from the start of a
  sequence
- FirstOrDefault returns the first element of a sequence, or a default value if
  no element is found

# Web API

- Used for building **HTTP** Web Services
- Great for **RESTful** services
- Methods return raw data which will automatically be converted into either
  **JSON** or **XML**

## Web API Usage

**![][image7]**

## Web Services

- Web Services allow computer to computer **function calls** over the Internet
- Allow you to create/use best of breed or authoritative services from experts
  (credit card validators, weather reports, etc.)
- Two main approaches
- **REST** – Representational State Transfer
- **SOAP** – Simple Object Access Protocol
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
- This default routing can be overridden by annotating method names with
  attributes such as **\[HttpGet\]**, **\[HttpPost\]**, etc
- Method parameters are automatically bound to the request’s parameters by the
  _Model Binder_
- Actions must be public, can’t be static, no ref or out parameters

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

See
[HTTP Status Codes \- Wikipedia](https://en.wikipedia.org/wiki/List_of_HTTP_status_codes)
for more details.

## Model Binding

- Maps incoming data from the HTTP Body and/or query string (URI) to method
  parameters
- MediaTypeFormatters transform input and output data to/from .NET objects
- Transforms supplied data (even if it is JSON, XML or Form data) to the
  parameters

## Returning Results from Controller Actions

- Often the return type for _Web API Controllers_ is an IActionResult as it is
  for standard _MVC Controllers_
- The base controller class defines several useful methods for returning the
  most common status codes

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
- Help documentation can be generated using [Swagger](https://swagger.io/), also
  known as [OpenAPI](https://www.openapis.org/)
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
- In the controller action using the HttpClient to call the remote service
  asynchronously
- If successful, return the results

## Parsing JSON

- Parsing JavaScript Object Notation (JSON) manually is tedious
- System.Text.Json provides capabilities to process JSON
  - **Serializing** objects to JSON text
  - **Deserializing** JSON text to objects
- Makes parsing JSON easy

# Authentication vs. Authorization

- Authentication is a process in which a user provides credentials that are then
  compared to those previously stored in an operating system, database, app or
  resource
- If they match, the user authenticates successfully
- Once authenticated a user can then perform actions that they're authorized for
- The authorization refers to the process that determines what a user is allowed
  to do

## Encryption vs. Hashing

- Sensitive data (like passwords) should never be stored in clear text in a
  database
- Two approaches: **Encryption** or **Hashing**
- An **encrypted** value can be **decrypted** back into its original clear text
  value
- **Hashing** is a one way process, a hash value can not be reconstituted back
  into its original text
- **Encryption** is not secure for long term storage
- It is secure for short term situations (HTTPS sessions)
- **Hashing** (with **salt**) is more secure and is used by ASP.NET Core
  **Identity**
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

- Adobe password crossword
  [http://zed0.co.uk/crossword](http://zed0.co.uk/crossword)

## Open Web Application Security Project (OWASP)

- Best practices related to security
- [https://www.owasp.org](https://www.owasp.org/)
- [Password Storage Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html)
- [Forgot Password Cheat Sheet](https://cheatsheetseries.owasp.org/cheatsheets/Forgot_Password_Cheat_Sheet.html)
- Many more...

## ASP.NET Core Identity

- ASP.NET Core **Identity** is a membership system that adds login functionality
  to ASP.NET Core apps
- Users can create an account with the login information stored in **Identity**
  or they can use an **external** login provider
- Supported external login providers include **Facebook**, **Google**,
  **Microsoft** Account and **Twitter**
- **Identity** can be configured using a **SQL Server** database (or any other
  database) to store user names, passwords, and profile data
- Alternatively, another persistent store can be used, for example, **Azure
  Table Storage**

## General Data Protection Regulation (GDPR)

- In 2018, the _European Union_ introduced the GDPR
- ASP.NET Core **Identity** is compliant with GDPR
- One of the stipulations of GDPR is that users can manage the information that
  an enterprise holds about them

## Authorization

- Once authenticated, authorization can be handled
- A method or a class can be **decorated** with the \[Authorize\] or
  \[AllowAnonymous\] attributes
- If a method is **decorated** with \[Authorize\], the user must be
  authenticated to access it
- If a class is **decorated** with \[Authorize\], all **methods** require
  authentication, unless decorated with \[AllowAnonymous\]

## ASP.NET Core Identity

- When ASP.NET Core **Identity** is added to a project, it is added as a _Razor
  Class Library_ (RCL)
- A _Razor Class Library_ contains Razor views, pages, controllers, page models,
  Razor components, View components and data models

## Customizing ASP.NET Core Identity

- ASP.NET Core **Identity** has many points of customization
- IdentityOptions allows configuring password, lockout, user options and more
- In order to customize one of the provided endpoints, the identity scaffolder
  must be used to generate the appropriate code

## Select Identity RCL Endpoints

- /Identity/Account/Login
- /Identity/Account/Logout
- /Identity/Account/Manage
- /Identity/Account/Register
- Many more...

# Customizing the User

- The user object in **Core Identity** is based on the
  [IdentityUser](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.identityuser?view=aspnetcore-5.0)
  class
- To customize the user, a new model is created that _inherits_ from
  IdentityUser
- By convention, this new model should be named ApplicationUser
- **Decorating** any new properties with \[PersonalData\] ensures that they are
  downloaded from the **PersonalData** page
- IdentityUser will have been used throughout the project and will need to be
  updated to ApplicationUser

## UserManager Class

- Provides the APIs for managing user in a persistence store
- Using the
  [UserManager](https://learn.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.identity.usermanager-1?view=aspnetcore-3.1)
  instead of interacting with the database directly allows less impact if the
  persistence store should ever change

## Role Based Security

- In an enterprise of any size, there will be a variety of employees performing
  a variety of tasks
- People change jobs, get promoted, leave the company on an ongoing basis
- Considering these changes, security best practice is to assign security
  privileges based on role
- Each enterprise would define roles that made sense for it, e.g.
  - Admin, Manager, Clerk, Trainee
  - Executive, Manager, Support, Faculty, Student
  - Admin, Full time employee, Part time employee
- It is possible for an employee to participate in more than one role, for
  example Admin and Manager

# Razor Pages

- Razor Pages provide an alternative to MVC in .NET Core, introduced with
  ASP.NET Core 2.0
- A project can mix and match MVC with Razor Pages
- Razor Pages are just as testable as MVC
- Razor Pages also support dependency injection
- The initial setup of a Razor Pages project has similarities to MVC and
  differences
- There is a wwwroot folder for static content
- Instead of a Controllers and View folder, there is a Pages folder
- The Pages folder represents the root of the project and all Razor pages will
  be placed here
- Razor Pages don't define endpoints through Controllers and Actions
- Each Razor Page endpoint is a .cshtml file with a .cshtml.cs backing file that
  provides logic
- The Razor endpoints are determined by the structure of the Pages folder
- This is similar to the approach that WebForms takes
- A Razor page looks very similar to a Razor view file used in MVC
- A key difference is that each Razor page begins with an @page directive
- The @page directive makes the file into an MVC action \- which means that it
  handles requests directly, without going through a controller

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
- Most of the MVC functions like ModelBinding, Validation, and ActionResults
  work the same with Controllers and Razor Pages
- Typical OnPost flow:
  - Check the ModelState
  - If not valid, return the page with any appropriate error message
  - If valid, save the data and redirect

## Data Binding

- Razor Pages, by default, bind properties only with **non-GET** verbs
- Binding to properties removes the need to writing code to convert HTTP data to
  the model type
- Binding reduces code by using the same property to render form fields (\<input
  asp-for="Course.CourseName"\>) and accept the input
- For security reasons, you must opt in to binding **GET** request data to page
  model properties
- Verify user input before mapping it to properties
- Opting into **GET** binding is useful when addressing scenarios that rely on
  query string or route values

## MVVM

- The Razor Pages approach is sometimes known as **MVVM** (Model-View-ViewModel)
- The **Model** is the data
- The **View** is the Razor page
- The **ViewModel** is the fully realized **PageModel**

# What is Blazor

- Blazor is a **Single Page Application** development framework
- The name Blazor is a combination/mutation of the words Browser and Razor (the
  .NET HTML view generating engine)
- Blazor allows us to write C# instead of JavaScript for client-side code
- First released in 2018
- The implication being that instead of having to execute Razor views on the
  server in order to present HTML to the browser, Blazor is capable of executing
  these views on the client
- Blazor does not require any kind of plugin installed on the client in order to
  execute inside a browser
- Blazor either runs server-side, in which case it executes on a server and the
  browser acts like a dumb terminal, or it runs in the browser itself by
  utilizing **WebAssembly**
- Because **WebAssembly** is a web standard, it is supported on all major
  browsers, which means also client-side Blazor apps will run inside a browser
  on Windows/Linux/Mac/Android and iOS

## WebAssembly

- **WebAssembly** (abbreviated **Wasm**) is a binary instruction format for a
  stack-based virtual machine
- **Wasm** is designed as a portable compilation target for programming
  languages, enabling deployment on the web for client and server applications
- [Languages that can target Wasm](https://github.com/appcypher/awesome-wasm-langs)

## Blazor Hosting Models

- Blazor Server
  - Blazor Server provides support for hosting Razor components on the server in
    an ASP.NET Core app
  - UI updates are handled over a **SignalR** connection
    - **SignalR** is a library that simplifies adding real-time web
      functionality to apps
    - Real-time web functionality enables server-side code to push content to
      clients instantly

## Blazor Server

- The runtime stays on the server and handles:
  - Executing the app's C# code
  - Sending UI events from the browser to the server
  - Applying UI updates to a rendered component that are sent back by the server

![][image8]

## Blazor Hosting Models

- Blazor WebAssembly
  - **WebAssembly** code can access the full functionality of the browser via
    **JavaScript**, called **JavaScript** interoperability, often shortened to
    **JavaScript** interop or JS interop
  - .NET code executed via **WebAssembly** in the browser runs in the browser's
    **JavaScript** sandbox with the protections that the sandbox provides
    against malicious actions on the client machine
- When a Blazor **WebAssembly** app is built and run in a browser
  - C# code files and Razor files are compiled into .NET assemblies
  - The assemblies and the .NET runtime are downloaded to the browser
  - Blazor **WebAssembly** bootstraps the .NET runtime and configures the
    runtime to load the assemblies for the app
  - The Blazor **WebAssembly** runtime uses **JavaScript** interop to handle
    **DOM** manipulation and browser API calls

![][image9]

## Server vs. WebAssembly

| Feature                                        | Server | WebAssembly |
| :--------------------------------------------- | :----: | :---------: |
| Complete .NET Core API compatibility           |   ✅   |     ❌      |
| Direct access to server sources                |   ✅   |     ❌      |
| Small payload size with fast initial load time |   ✅   |     ❌      |
| App code secure and private on the server      |   ✅   |    ❌\*     |
| Run apps offline once downloaded               |   ❌   |     ✅      |
| Static site hosting                            |   ❌   |     ✅      |
| Offloads processing to clients                 |   ❌   |     ✅      |

**\***Blazor **WebAssembly** apps can use server-hosted APIs to access
functionality that must be kept **private** and **secure**

## Blazor Server Project Structure

- Program.cs is unchanged
- Startup.cs is similar
- Pages/\_Host.cshtml, App.razor, Shared/MainLayout.razor, NavMenu.razor all
  work together to setup the overall page structure
- The Razor components are in the Pages and Shared folders

## Razor Components

- Blazor apps are built using Razor components
- A component is a self-contained portion of user interface (UI) with processing
  logic to enable dynamic behavior
- Components can be nested, reused, shared among projects, and used in MVC and
  Razor Pages apps

## @inject

- The @inject directive enables the Razor Page to inject a service from the
  service container into a view
- The service container is established in the ConfigureServices method of
  Startup.cs

# Blazor Wasm Project Structure

- Program.cs calls WebAssemblyHostBuilder which sets the default page to
  index.html; it also specifies to use App.razor as the **root** component
- Startup.cs does not exist
- wwwroot/index.html, App.razor, Shared/MainLayout.razor, NavMenu.razor all work
  together to setup the overall page structure

## Binding

- We have been using **one-way** binding with the @ symbol and a variable name
- For example, in the Counter.razor component, @currentCount is used to display
  the output
- **Two-Way** binding allows the reading of values

## ASP.NET Core Hosted

- When creating a Blazor **Wasm** app, the opportunity to select ASP.NET Core
  hosted is presented
- When this option is chosen, 3 folders are created in the solution
  - Client
  - Server
  - Shared
- The Client folder contains the Blazor **Wasm** project, which will be the same
  as a **regular** Blazor **Wasm** project
- In the Solution Explorer, this will be named ProjectName.Client
- The Server folder contains a Web API project which will be the same as
  **regular** Web API project
- In the Solution Explorer, this will be named ProjectName.Server
- This part of the project will run server-side and have access to all of the
  resources normally available
- The Startup class points to the client's index.html page
- The Shared folder is where any files are placed that are needed by both the
  Client and the Server projects
- In the Solution Explorer, this will be named ProjectName.Shared

## Progressive Web Application

- **PWAs** are web apps that are installable on _Desktop_ and _Phone_ OSs
- They can potentially work offline
- They make use of service workers and manifests
- Service workers act as proxy servers that sit between web applications, the
  browser, and the network (when available)
- A manifest is a JSON file that provides information about how a web app would
  be downloaded and be presented to the user similarly to a native app

## Blazor PWA Project Structure

- Nearly identical to **regular** Blazor **Wasm**
- The wwwroot folder will contain additional files
- icon-512.png
- manifest.json
- service-worker.js
- index.html will contain references to the _manifest_, _icon_ and
  _service-worker_ files

# EditForm

- The Blazor framework supports web-forms with validation using the EditForm
  component
- Two of the EditForm properties are Model and OnValidSubmit
- The Model property is used to bind the input elements and validation
- The OnValidSubmit property provides a callback that will be invoked when form
  is submitted

## Parameters

- Any Razor component can accept parameters
- Parameters are specified in the code block and are decorated with the
  Parameter keyword

## Form Components

- The Blazor framework provides built-in form components to receive and validate
  user input
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

- Provides a way for querying and managing URI navigation
- Uniform Resource Identifier
  - https://
  - mailto://
  - file://

## OnParametersSetAsync

- Method invoked when the component has received parameters from its parent in
  the render tree, and the incoming values have been assigned to properties

[image1]: ./resources/imgs/1.dot-net-architecture.webp
[image2]: ./resources/imgs/2.asp-dot-net-architecture.webp
[image3]: ./resources/imgs/3.kestrel.webp
[image4]: ./resources/imgs/4.kestrel.webp
[image5]: ./resources/imgs/5.middleware.webp
[image6]: ./resources/imgs/6.standard-mvc-architecture.webp
[image7]: ./resources/imgs/7.web-api-usage.webp
[image8]: ./resources/imgs/8.blazor-server.webp
[image9]: ./resources/imgs/9.blazor-hosting-models.webp
