# Walkthrough 3 - Introduction to MVC (part 1)

## Setup

This part of the walkthrough will introduce MVC.

- Open Visual Studio, and click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `ASP.NET Core Empty` template, click `Next`.
- Set Project name to `IntroMVC`, and click `Next`.
- Set version to `.NET 5.0`, **unselect** `Configure for HTTPS`, click `Create`.
- Run the site.

## Program.cs

- Break apart the `Main` method to better understand what it's doing.

```cs
public static void Main(string[] args)
{
    var hostBuilder = CreateHostBuilder(args);
    var app = hostBuilder.Build();

    app.Run();
}
```

- Create a method `CreateHostBuilder` that accepts `string[]` into args, and return an `IHostBuilder`.

```cs
public static IHostBuilder CreateHostBuilder(string[] args) => Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder => webBuilder
        .UseStartup<Startup>());
```

- Convert the `CreateHostBuilder` lambda function call into a "regular" method.
- Break apart the `CreateHostBuilder` method to better understand what it's doing.

```cs
public static IHostBuilder CreateHostBuilder(string[] args)
{
    var hostBuilder = Host.CreateDefaultBuilder(args);

    hostBuilder
        .ConfigureWebHostDefaults(builder => builder
            .UseStartup<Startup>());

    return hostBuilder;
}
```

- Put the cursor on the `CreateDefaultBuilder` method of the `var hostBuilder = Host.CreateDefaultBuilder(args);` statement.
- Right-click it and select **Go To Definition (F12)**.
- Expand `public static IHostBuilder CreateDefaultBuilder(string[] args);` and peruse the comments.
- Close the tab.
- Put cursor on the `var hostBuilder = CreateHostBuilder(args);` line of the `Main` method.
- Press **F9** to add a breakpoint.
- Press **F5** to run in debug mode.
- Press **F11** to step through the code until the browser appears with the output.
- Press **Shift+F5** to stop the debugger.
- Remove the breakpoint.
- Press **Ctrl+F5** to run without debugging.
- In the browser, navigate to `http://localhost:<port>/hi`, replace `<port>` value to match your project.
- The page won't be found, because the existing middleware is only looking for the root page.

## Startup.cs

- Inspect the existing middleware and notice where the `Hello World!` response is written.
- Delete the existing middleware.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapGet("/", async context =>
        {
            await context
                .Response
                .WriteAsync("Hello World!");
        });
    });
}
```

- Run the site.
- A `404` will be returned because there is no middleware to respond.
- Add a terminal middleware delegate to the application's request pipeline.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.Run(async context => await context
        .Response
        .WriteAsync("<h1>Hello from Run</h1>"));
}
```

- Run the site, the heading will be returned.
- In the browser, navigate to `http://localhost:<port>/hi`,
- The URL above won't matter, because the existing middleware responds to all requests.
- Add another terminal middleware delegate to the application's request pipeline.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.Run(async context => await context
        .Response
        .WriteAsync("<h1>Hello from Run</h1>"));

    app.Run(async context => await context
        .Response
        .WriteAsync("Text"));
}
```

- Run the site.
- The second middleware delegate isn't invoked because the first one terminates the pipeline.
- Comment out the first middleware delegate.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // app.Run(async context => await context.Response.WriteAsync("<h1>Hello from Run</h1>"));

    app.Run(async context => await context
        .Response
        .WriteAsync("Text"));
}
```

- Run the site.
- The other delegate responds.
- Uncomment the first delegate and delete the second one.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.Run(async context => await context
        .Response
        .WriteAsync("<h1>Hello from Run</h1>"));
}
```

- Add a `Use` middleware delegate.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.Use(async (context, next) =>
    {
        await context
            .Response
            .WriteAsync("<h1>Hello from Use</h1>");

        await next();

        await context
            .Response
            .WriteAsync("<h1>Hello again from Use</h1>");
    });

    app.Run(async context => await context
        .Response
        .WriteAsync("<h1>Hello from Run</h1>"));
}
```

- Run the site, the headings appear in the browser.
- Add `UseFileServer` which will serve static pages from `wwwroot`.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseFileServer();

    app.Use(async (context, next) =>
    {
        await context
            .Response
            .WriteAsync("<h1>Hello from Use</h1>");

        await next();

        await context
            .Response
            .WriteAsync("<h1>Hello again from Use</h1>");
    });

    app.Run(async context => await context
        .Response
        .WriteAsync("<h1>Hello from Run</h1>"));
}
```

- Run the site, the headings still appear in the browser because there are no static files yet.

## wwwroot

- In Solution Explorer, right-click the project and select `Add / New Folder`, and name it `wwwroot`.
- Download content of [wwwroot](../examples/IntroMVC/wwwroot/) folder from and paste it into newly created `wwwroot` folder.
- Run the site, `wwwroot/index.html` appears.
- Click the link for **Page One**, `pageone.html` appears.
- Click the link for **Page Two**, since there is no `pagetwo.html`, the other middleware responds.

## Startup.cs

### Error Handling

- Add some middleware that will throw an exception if the request contains the word invalid.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseFileServer();

    app.Use(async (context, next) =>
    {
        if (context.Request.Path.Value.Contains("invalid"))
        {
            throw new Exception("ERROR!");
        }

        await next();
    });

    app.Use(async (context, next) =>
    {
        await context
            .Response
            .WriteAsync("<h1>Hello from Use</h1>");

        await next();

        await context
            .Response
            .WriteAsync("<h1>Hello again from Use</h1>");
    });

    app.Run(async context => await context
        .Response
        .WriteAsync("<h1>Hello from Run</h1>"));
}
```

- Run the site, navigate to `http://localhost/invalid`.
- A `500` will be returned indicating a server error.
- Add some middleware to present a more detailed error message, while in development mode.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseFileServer();

    app.Use(async (context, next) =>
    {
        if (context.Request.Path.Value.Contains("invalid"))
        {
            throw new Exception("ERROR!");
        }

        await next();
    });

    app.Use(async (context, next) =>
    {
        await context
            .Response
            .WriteAsync("<h1>Hello from Use</h1>");

        await next();

        await context
            .Response
            .WriteAsync("<h1>Hello again from Use</h1>");
    });

    app.Run(async context => await context
        .Response
        .WriteAsync("<h1>Hello from Run</h1>"));
}
```

- Run the site, navigate to `http://localhost/invalid`.
- A diagnostic error page will be returned.
- Add middleware to handle an exception.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseExceptionHandler("/error.html");

    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseFileServer();

    app.Use(async (context, next) =>
    {
        if (context.Request.Path.Value.Contains("invalid"))
        {
            throw new Exception("ERROR!");
        }

        await next();
    });

    app.Use(async (context, next) =>
    {
        await context
            .Response
            .WriteAsync("<h1>Hello from Use</h1>");

        await next();

        await context
            .Response
            .WriteAsync("<h1>Hello again from Use</h1>");
    });

    app.Run(async context => await context
        .Response
        .WriteAsync("<h1>Hello from Run</h1>"));
}
```

- Run the site, navigate to `http://localhost/invalid`.
- The error diagnostic page still appears.
- Notice the first if statement in the method is checking for development mode.
- In **Solution Explorer**, right-click the project and select `Properties`.
- Click the Debug tab.
- Change the `ASPNETCORE_ENVIRONMENT` environment variable to `Production`.
- Close the `Properties` page.
- Run the site, navigate to `http://localhost/invalid`.
- A friendly error page now displays.
- Change `ASPNETCORE_ENVIRONMENT` back to `Development`.
- Refresh the invalid page to ensure it is changed back correctly.
- Delete the default exception handler, the middleware that throws an error and "Hello" middleware delegates.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    app.UseExceptionHandler("/error.html");

    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseFileServer();
}
```

- Add middleware to specify routing and a default route.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();

    app.UseFileServer();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

- Run the site.
- A different error occurs.
- This one indicates that a call to add controllers is missing.
- Update the `ConfigureServices` method to configure **MVC**.

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();
}
```

- Run the site, its functional again.

## HomeController.cs

- Create the `Controllers` folder under the project.
- Right-click the `Controllers` folder and select `Add / Controller...`.
- Select `MVC Controller - Empty` and accept the default name of `HomeController.cs`.
- Run the site, a hard refresh may be necessary.
- A new error will appear.
- This error indicates that the view for the `Index` action wasn't found.
- Change the return type to a string and return one.

```cs
public string Index()
{
    return "Hello from HomeController / Index";
}
```

- Run the site.
- The output from the `Index` method appears.
- Change the return type back to `IActionResult` and return a `ContentResult`.

```cs
public IActionResult Index()
{
    return new ContentResult {
        Content = "Hello from HomeController / Index"
    };
}
```

- Run the site, the output is unchanged.

## Startup.cs

- Note that the **Home** controller responds before the static files.
- In the `Configure` method, move `app.UseFileServer` statement before the `app.UseRouting` statement.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseFileServer();

    app.UseRouting();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

- Run the site.
- Note that the static page is served.
- Move `app.UseFileServer` statement back to follow the `app.UseRouting` statement.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app.UseRouting();

    app.UseFileServer();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

- Run the site.
- Note that the **Home** controller responds again.

---

# Walkthrough 3 - Introduction to MVC (part 2)

## Setup

This part of the walkthrough will continue the MVC introduction.

- Open `IntroMVC` from the end of the previous walkthrough.

## OtherController.cs

- Add another `Controller`. Right-click the `Controller` folder.
- Select `Add / Controller...`.
- Choose the `MVC Controller - Empty` template, mame it `OtherController`, and click `Add`.
- Put the cursor in the `Index` method.
- Right-click and select `Add View...`.
- Select `Razor View - Empty`, and click `Add`.
- Accept the default name `Index.cshtml`, click `Add`.
- A folder named `Views` will be created. Inside the `Views` folder.
- Another folder named `Other` will be created.
- This is where `Index.cshtml` will reside.

## Index.cshtml

- Delete the existing code.

```cs
@*
    // For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
*@
@{
    // Empty block
}
```

- Add a heading.

```html
<h1>Hello from OtherController / Index</h1>
```

## OtherController.cs

- Add a Post method that accepts a string and passes it to the view and then returns the view.

```cs
public IActionResult Post(string id)
{
    ViewBag.id = id;

    return View();
}
```

- Put the cursor in the `Post` method.
- Right-click and select `Add View...`.
- Select `Razor View - Empty`, and click `Add`.
- Name the view `Post.cshtml`, click `Add`.

## Post.cshtml

- Delete the existing code.

```cs
@*
    // For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
*@
@{
}
```

- Add two headings.

```html
<h1>Hello from OtherController / Post</h1>
<h2>id=@ViewBag.id</h2>
```

- Press Ctrl+F5 to launch the site.
- Navigate to the following URLs and note the results.
  - `http://localhost:<port>/other/post/1`
  - `http://localhost:<port>/other/post/`
  - `http://localhost:<port>/other/`
  - `http://localhost:<port>/`
  - `http://localhost:<port>/other/post/abc`
  - `http://localhost:<port>/other/post?id=def`
  - `http://localhost:<port>/other/post?param=def`

## OtherController.cs

- Change the `id` to an integer.

```cs
public IActionResult Post(int id)
{
    ViewBag.id = id;

    return View();
}
```

- Save the file and refresh the browser.
- Note `id` is now 0, because that is the default value for an integer.
- Change the URL to `http://localhost:<port>/other/post/10` and note the output.
- Change the `id` to a boolean.

```cs
public IActionResult Post(bool id)
{
    ViewBag.id = id;

    return View();
}
```

- Save the file and refresh the browser.
- Note `id` is now `False`, because that is the default value for a boolean.
- Change the URL to `http://localhost:<port>/other/post/true` and note the output.
- Change the id to be a nullable integer.
- This makes the default value `null` instead of 0.

```cs
public IActionResult Post(int? id)
{
    ViewBag.id = id;

    return View();
}
```

- Save the file and refresh the browser, note the output.
- Change the URL to `http://localhost:<port>/other/post/10` and note the output.
- Change the `id` to have a default value.

```cs
public IActionResult Post(int? id = -1)
{
    ViewBag.id = id;

    return View();
}
```

- Save the file and refresh the browser, note the output.
- Change the URL to `http://localhost:<port>/other/post/abc` and note the output.
- Customize the route that the `Post` method will respond to.

```cs
[Route("stuff")]
public IActionResult Post(int id = -1)
{
    ViewBag.id = id;

    return View();
}
```

- Save the file and refresh the browser.
- The page won't be found because it's route is now different.
- Change the URL to `http://localhost:<port>/stuff` and `Post` will respond.
- Customize the route that the `Post` method will use to look for a year, a month and string value name key.

```cs
[Route("stuff/{year:int}/{month:int}/{key}")]
public IActionResult Post(int year, int month, string key)
{
    // ViewBag and ViewData can be used interchangeably
    ViewBag.year = year;
    ViewBag.month = month;
    ViewData["key"] = key;

    return View();
}
```

- Save the file.

## Post.cshtml

- Update the view to use the new data.

```html
<h1>Hello from OtherController / Post</h1>
<h2>year=@ViewBag.year</h2>
<h2>month=@ViewBag.month</h2>
<h2>key=@ViewData["key"]</h2>
```

- Save the file and refresh the browser.
- The page won't be found because it's route is now different.
- Change the URL to `http://localhost:<port>/stuff/2025/10/Hello` and note the output.

## OtherController.cs

- Add route constraints to ensure recent years and possible months.

```cs
[Route("stuff/{year:intmin(2019)}/{month:intrange(1,12)}/{key}")]
public IActionResult Post(int year, int month, string key)
{
    ViewBag.year = year;
    ViewData["MONTH"] = month;
    ViewBag.key = key;

    return View();
}
```

- Save the file and refresh the browser, output doesn't change.
- Change the URL to `http://localhost:<port>/stuff/2021/99/abc`.
- The routing won't be satisfied and controller won't respond.
- Change the URL back to `http://localhost:<port>/stuff/2021/10/abc`.

## Post.cshtml

- Add some Razor code to show the current date/time and a week ago.

```html
<h1>Hello from OtherController / Post</h1>
<h2>year=@ViewBag.year</h2>
<h2>month=@ViewBag.month</h2>
<h2>key=@ViewData["Key"]</h2>
<p>Today: @DateTime.Now</p>
<p>Week ago: @(DateTime.Now - TimeSpan.FromDays(7))</p>
```

- Save the file and refresh the browser, note the output.
- Add a Razor code block.

```html
<h1>Hello from OtherController / Post</h1>
<h2>year=@ViewBag.year</h2>
<h2>month=@ViewBag.month</h2>
<h2>key=@ViewData["Key"]</h2>
<p>Today: @DateTime.Now</p>
<p>Week ago: @(DateTime.Now - TimeSpan.FromDays(7))</p>
@{
    var name = "Bob Loblaw";
    var len = name.Length;
}
<p>The length of @name is @len bytes.</p>
```

- Save the file and refresh the browser, note the output.
- Add a list using Razor.

```html
<h1>Hello from OtherController / Post</h1>
<h2>year=@ViewBag.year</h2>
<h2>month=@ViewBag.month</h2>
<h2>key=@ViewData["Key"]</h2>
<p>Today: @DateTime.Now</p>
<p>Week ago: @(DateTime.Now - TimeSpan.FromDays(7))</p>
@{
    var name = "Bob Loblaw";
    var len = name.Length;
}
<p>The length of @name is @len bytes.</p>
<ul>
    @for (int i = 0; i < ViewBag.month; i++)
    {
        <li>Month @i</li>
    }
</ul>
```

- Save the file and refresh the browser, note the output.
- Month 0 doesn't really make sense, update the code.

```html
<h1>Hello from OtherController / Post</h1>
<h2>year=@ViewBag.year</h2>
<h2>month=@ViewBag.month</h2>
<h2>key=@ViewData["Key"]</h2>
<p>Today: @DateTime.Now</p>
<p>Week ago: @(DateTime.Now - TimeSpan.FromDays(7))</p>
@{
    var name = "Bob Loblaw";
    var len = name.Length;
}
<p>The length of @name is @len bytes.</p>
<ul>
    @for (int i = 0; i < ViewBag.month; i++)
    {
        <li>Month @(i + 1)</li>
    }
</ul>
```

- Save the file and refresh the browser, note the output.
- Add some more Razor code to display the month name.

```html
<h1>Hello from OtherController / Post</h1>
<h2>year=@ViewBag.year</h2>
<h2>month=@ViewBag.month</h2>
<h2>key=@ViewData["Key"]</h2>
<p>Today: @DateTime.Now</p>
<p>Week ago: @(DateTime.Now - TimeSpan.FromDays(7))</p>
@{
    var name = "Bob Loblaw";
    var len = name.Length;
}
<p>The length of @name is @len bytes.</p>
<ul>
    @for (int i = 0; i < ViewBag.month; i++)
    {
        <li>Month @(i + 1) is @(new DateTime(2021, i + 1, 1).ToString("MMMM"))</li>
    }
</ul>
```

- Save the file and refresh the browser, note the output.

## HomeController.cs

- Revert the Index method to return a view.

```cs
public IActionResult Index()
{
    // return new ContentResult { Content = "Hello from HomeController / Index." };

    View();
}
```

- Save the file and change the URL to `http://localhost:12345/`.
- An exception will be thrown, examine it.

## Index.cshtml

- Create a `Home` folder under the `Views` folder.
- Move `index.html` file from `wwwroot` to `Views/Home`.
- Rename the file `Index.cshtml`.
- Refresh the browser, the page should appear.
- In the footer, near the bottom of the page, replace the JavaScript year with some Razor code.

```html
<!-- ... -->
<footer class="border-top footer text-muted">
    <div class="container">
        &copy; @DateTime.Now.Year - MVC Intro
    </div>
</footer>
<!-- ... -->
```

- Save the file and refresh the browser.
- Nothing will look different, but the year isn't generated with JavaScript anymore.
- In the `navbar-collapse` div, update the `Page Two` link to link to `Index` action of the `Other` controller.

```html
<!-- ... -->
<div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
    <ul class="navbar-nav flex-grow-1">
        <li class="nav-item">
            <a> class="nav-link text-dark" href="/pageone.html">Page One</a>
        </li>
        <li class="nav-item">
            <a> class="nav-link text-dark" href="@Url.Action("Index", "Other")">Other</a>
        </li>
    </ul>
</div>
<!-- ... -->
```

- Save the file and refresh the browser.
- Try the updated link. Click the browser back button.
- Click the `Page One` link, note that it's menu is the old menu.
- Click the `MVC Intro` link.

## _Layout.cshtml

- Create the `Shared` folder under the `Views` folder.
- Copy `Views/Home/Index.cshtml` file into the new `Shared` folder and rename it `_Layout.cshtml`.
- Update the title tag to use `ViewData`, so that each page can have a customized title.

```html
<!-- ... -->
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>@ViewData["Title"] - MVC Intro</title>
    <link rel="stylesheet" href="/css/bootstrap.min.css" />
    <link rel="stylesheet" href="/css/site.css" />
</head>
<!-- ... -->
```

- In the main section, delete the div that displays the welcome message and replace it with a call to the `RenderBody` method.

```html
<!-- ... -->
<div class="container">
    <main role="main" class="pb-3">
        @RenderBody()
    </main>
</div>
<!-- ... -->
```

- Save the file.

## Index.cshtml

- Open `Views/Home/Index.cshtml`.
- Delete all of the code and re-create the header.

```html
<div class="text-center">
    <h1 class="display-4">Welcome to MVC</h1>
</div>
```

- Add a Razor code block to specify the `Layout` and set the page title.

```html
@{
    Layout = "_Layout";
    ViewData["Title"] = "Home Page";
}

<div class="text-center">
    <h1 class="display-4">Welcome to MVC</h1>
</div>
```

- Save the file and refresh the browser, page should look the same.

## IntroMVC.csproj

- Sometimes, after the `Index.cshtml` manipulation, the `_Layout.cshtml` isn't found.
- If this error occurs, click the project in the **Solution Explorer** and the `IntroMVC.csproj` file will open.
- **Delete** any `ItemGroup` tags.

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
  <PropertyGroup>
    <TargetFramework>net6.0</TargetFramework>
  </PropertyGroup>

  <ItemGroup>
    <Content Remove="Views\Shared\_Layout.cshtml" />
  </ItemGroup>

  <ItemGroup>
    <None Include="Views\Home\Index.cshtml" />
    <None Include="Views\Shared\_Layout.cshtml" />
  </ItemGroup>
</Project>
```

- Save the file and refresh the browser, page should look the same.

## Index.cshtml

- Open `Views/Other/Index.cshtml`.
- Add a Razor code block to specify the `Layout` and set the page title.

```html
@{
    Layout = "_Layout";
    ViewData["Title"] = "Other Page";
}
<h1>Hello from OtherController / Index</h1>
```

- Save the file.
- In the browser, click the Other link, the new view is presented.

## OtherController.cs

- Add a new method to serve page one.

```cs
public IActionResult PageOne()
{
    return View();
}
```

- Save the file.
- Put the cursor in the PageOne method.
- Right-click and select `Add View...`.
- Select `Razor View - Empty`, and click `Add`.
- Name the view `PageOne.cshtml`, and click `Add`.

## PageOne.cshtml

- Delete the existing code.

```cs
@*
    // For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
*@
@{
}
```

- Add a Razor code block to specify the `Layout` and set the page title.
- Also add the same heading that was in `pageone.html`.

```html
@{
    Layout = "_Layout";
    ViewData["Title"] = "Page One";
}
<div class="text-center">
    <h1 class="display-4">This is page one</h1>
</div>
```

- Save the file.

## _Layout.cshtml

- In the `navbar-collapse` div, update the `Page One` link to link to the `PageOne` action of the `Other` controller.

```html
<!-- ... -->
<div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
    <ul class="navbar-nav flex-grow-1">
        <li class="nav-item">
            <a> class="nav-link text-dark" href="@Url.Action("PageOne", "Other")">Page One</a>
        </li>
        <li class="nav-item">
            <a> class="nav-link text-dark" href="@Url.Action("Index", "Other")">Other</a>
        </li>
    </ul>
</div>
<!-- ... -->
```

- Save the file.
- Refresh the browser; the menu should now work on all pages.
- Click the `Page One` link.

## pageone.html

- Delete `wwwroot/pageone.html` as it is no longer needed.

## Person.cs

- Create the `Models` folder under the project.
- Right-click the `Models` folder.
- Select `Add / Class...` and name it `Person.cs`.

```cs
public class Person
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
}
```

- Save the file.

## OtherController.cs

- In the `PageOne` method, instantiate a new `Person` object.
- Use quick actions to add the `using IntroMVC.Models;` directive.
- Pass the `person` to the `View`.

```cs
public IActionResult PageOne()
{
    var person = new Person
    {
        Id = 1,
        FirstName = "Bob",
        LastName = "Loblaw",
        DateOfBirth = DateTime.Now.AddDays(-10000)
    };
    return View(person);
}
```

- Save the file.

## PageOne.cshtml

- Add the model to the view.
- Display the person.

```html
@model IntroMVC.Models.Person
@{
    Layout = "_Layout";
    ViewData["Title"] = "Page One";
}
<div class="text-center">
    <h1 class="display-4">This is page one</h1>
</div>
<h2>Person</h2>
<dl>
    <dt>Id</dt>
    <dd>@Model.Id</dd>
    <dt>First name</dt>
    <dd>@Model.FirstName</dd>
    <dt>Last name</dt>
    <dd>@Model.LastName</dd>
    <dt>Date of birth</dt>
    <dd>@Model.DateOfBirth</dd>
</dl>
```

- Save the file, refresh the browser.
