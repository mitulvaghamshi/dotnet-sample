# Walkthrough 14 - Fully Implementing a REST API

## Setup

This walkthrough will further explore **Web API**.

- Start SQL Server.
- Start Visual Studio.
- Click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `ASP.NET Core Web API` template, click `Next`.
- Set Project name to `PhysicianAPI`.
- Set Location to a folder of your choosing.
- Set Solution name to `MedicalStaff`.
- Ensure Place solution and project in the same directory is unchecked, click
  `Next`.
- Set version to `.NET 5.0`, and:
  - Set `Authentication Type` to `None`
  - Unselect `Configure for HTTPS`
  - Select `Enable OpenAPI` support, if necessary
- Click `Create`.
- Delete `WeatherForecast.cs` and `Controllers / WeatherForecastController.cs`.

## appsettings.json

- Add a _connection string_ for the `CHDB` database.

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "CHDB": "Server=localhost\\sqlexpress;Database=CHDB;Trusted_Connection=True"
  }
}
```

- Save the file.

## Scaffold-DbContext

- Open the `Package Manager Console` (PMC) and issue the following commands:

```console
Install-Package Microsoft.EntityFrameworkCore.Tools

Install-Package Microsoft.EntityFrameworkCore.SqlServer

Scaffold-DbContext name=CHDB Microsoft.EntityFrameworkCore.SqlServer
    -OutputDir Models -Tables physicians
```

## Startup.cs

- Add the directives.

```cs
using PhysicianAPI.Models;
using Microsoft.EntityFrameworkCore;
```

- Register the database context.

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllers();

    services.AddDbContext<CHDBContext>(options =>
        options.UseSqlServer(Configuration.GetConnectionString("CHDB")));

    services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "PhysicianAPI", Version = "v1"
    }));
}
```

- Save the file.

## PhysiciansController.cs

- Right-click `Controllers` folder and select `Add / Controller...`
- Select `API Controller with actions, using Entity Framework`, click **Add**.
- Set `Model` class to `Physician (PhysicianAPI.Models)`.
- Set `Data context` class to `CHDBContext (PhysicianAPI.Models)`.
- Accept the default `Controller` name `PhysiciansController`, click **Add**.
- Update `PostPhysician` to compute the new physician's id.

```cs
public async Task<ActionResult<Physician>> PostPhysician(Physician physician)
{
    var maxId = _context.Physicians.Max(p => p.PhysicianId);

    physician.PhysicianId = maxId + 1;

    _context.Physicians.Add(physician);

    try
    {
        await _context.SaveChangesAsync();
    }
    catch (DbUpdateException)
    {
        if (PhysicianExists(physician.PhysicianId))
        {
            return Conflict();
        }
        else
        {
            throw;
        }
    }

    return CreatedAtAction("GetPhysician",
        new { id = physician.PhysicianId }, physician);
}
```

- Run the site.
- Click the first **GET** button (the one that doesn't have an id parameter).
- Click the `Try it out` button.
- Click the `Execute` button to see all physicians.
- Click the **GET** button to collapse it.
- Click the second **GET** button (the one that does have an id paremeter).
- Click the `Try it out` button.
- Set the `id` parmeter to `1`, click `Execute`.
- Make note of the `Request` URL your API is using, it will be needed later;
  specifically the port.
- Select the **JSON** in the `Response` body and click `Ctrl+C` to copy it.

## Doctors Project - Setup

- Right-click the Solution in the Solution Explorer and select
  `Add / New Project...`
- Set language to `C#` and project type to `Web`.
- Select the `ASP.NET Core Web App (Model-View-Controller)` template, click
  `Next`.
- Set Project name to `Doctors`, leave Location as is, click `Next`.
- Set version to `.NET 5.0`, set `Authentication Type` to `None`, unselect
  `Configure for HTTPS`, click `Create`.

## Doc.cs

- Create a new model file named `Doc` in the `Models` folder.
- Delete the empty `Doc` class definition.

```cs
namespace Doctors.Models
{
    // public class Doc
    // {
    // }
}
```

- Place the cursor between the curly braces.
- From the `Edit` menu, select `Paste Special / Paste JSON As Classes`.
- Rename the class from `RootObject` to `Doc`.

```cs
public class Doc
{
    public int physicianId { get; set; }
    public string firstName { get; set; }
    public string lastName { get; set; }
    public string specialty { get; set; }
    public string phone { get; set; }
    public int ohipRegistration { get; set; }
}
```

- Annotate the class, adding the `using System.ComponentModel.DataAnnotations;`
  directive.

```cs
public class Doc
{
    [Key]
    public int physicianId { get; set; }

    [Display(Name = "First Name")]
    public string firstName { get; set; }

    [Display(Name = "Last Name")]
    public string lastName { get; set; }

    [Display(Name = "Specialty")]
    public string specialty { get; set; }

    [Display(Name = "Phone")]
    public string phone { get; set; }

    [Display(Name = "OHIP Registration")]
    public int ohipRegistration { get; set; }
}
```

- Save the file.

## ErrorViewModel.cs

- In the `Models` folder, open `ErrorViewModel` class.
- Add a `Description` property to it.

```cs
public class ErrorViewModel
{
    public string RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    public string Description { get; set; }
}
```

- Save the file.

## Error.cshtml

- Open `Views/Shared/Error.csthml`.
- Add the `Description` property and remove the `Development Mode` text.

```html
@model ErrorViewModel

@{ ViewData["Title"] = "Error"; }

<h1> class="text-danger">Error.</h1>

<h2> class="text-danger">
    An error occurred while processing your request.
</h2>

@if (Model.ShowRequestId)
{
    <p>
        <strong>Request ID:</strong> <code>@Model.RequestId</code>
    </p>
}

<p>
    <strong>Description:</strong> <code>@Model.Description</code>
</p>
```

- Save the file.

## DocsController.cs

- **Scaffold** a new `MVC Controller with read/write actions`, name it
  `DocsController`.
- Setup a string with the URL for the API with a trailing slash using the port
  noted earlier.
- Also, setup a string for `application/json` as it will be needed multiple
  times.

```cs
public class DocsController : Controller
{
    private string baseUrl = "http://localhost:12345/api/";
    private string appJson = "application/json";

    // GET: Docs
    public ActionResult Index()
    // ...
```

## Index

- Change the method signature of `Index` to be _asynchronous_.

```cs
public async Task<ActionResult> Index()
{
    // ...
}
```

- Add the directives.

```cs
using Doctors.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
```

- Instantiate a list of `Doc` and set up a call to the web server.
- Call the web server and get the **JSON**.
- Parse the **JSON** into the list of `Doc` and return them in the view.

```cs
public async Task<ActionResult> Index()
{
    try
    {
        var docs = new List<Doc>();

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue(appJson));

            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync("physicians");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content
                    .ReadAsStringAsync();

                docs = JsonSerializer
                    .Deserialize<List<Doc>>(json);
            }
        }

        return View(docs);
    }
    catch (Exception ex)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = HttpContext.TraceIdentifier,
            Description = ex.Message
        });
    }
}
```

- Right-click in the `Index` method and select `Add View...` select
  `Razor View`, click **Add**.
- Set `View name` to `Index`, `Template` to `List`, `Model` class to
  `Doc (Doctors.Models)`, click **Add**.

## Index.cshtml

- Update the title and heading.
- Remove the physician id from the table and update the `Edit`, `Details` and
  `Delete` links.

```html
@model IEnumerable<Doctors.Models.Doc>

@{ ViewData["Title"] = "Doctors"; }

<h1>@ViewBag.Title</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>

<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.firstName)
        <!-- ... -->
        @foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.firstName)
        <!-- ... -->

<td>
    @Html.ActionLink("Edit", "Edit", new { id = item.physicianId }) |
    @Html.ActionLink("Details", "Details", new { id = item.physicianId }) |
    @Html.ActionLink("Delete", "Delete", new { id = item.physicianId })
</td>
<!-- ... -->
```

- Save the file.

## _Layout.cshtml

- Open `Views/Shared/_Layout.cshtml` and modify the `Home` link to the `Docs`
  controller.

```html
<!-- ... -->
<li class="nav-item">
    <a class="nav-link text-dark" asp-area=""
        asp-controller="Docs"
        asp-action="Index">Docs</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area=""
        asp-controller="Home"
        asp-action="Privacy">Privacy</a>
</li>
<!-- ... -->
```

- Save the file.

## Running Multiple Projects

- In the Solution Explorer, right-click the Solution and select `Properties`.
- Under `Common Properties`, select `Startup Project`.
- Select `Multiple startup projects`.
- Change the `Action` dropdown to `Start` for both projects.
- Select the `ProjectAPI` project and click the `Up` button, so that it starts
  first.
- Click **OK**.
- Run the site, select the browser tab with the MVC page (not Swagger), if
  necessary.
- Click the `Docs` link.
- The physicians should appear.

## DocsController.cs

### GetDocAsync

- Since the `Details`, `Edit` and `Delete` **GETs** will all be the same, create
  a new method named `GetDocAsync` to retrieve a doctor from the API.
- Instantiate a `Doc` and set up a call to the web server.
- Call the web server and get the **JSON**.
- Parse the **JSON** into the `Doc` and return it.

```cs
private async Task<Doc> GetDocAsync(int id)
{
    var doc = new Doc();

    try
    {
        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();

            client.DefaultRequestHeaders.Accept
                .Add(new MediaTypeWithQualityHeaderValue(appJson));

            client.BaseAddress = new Uri(baseUrl);

            var response = await client.GetAsync($"physicians/{id}");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content
                    .ReadAsStringAsync();

                doc = JsonSerializer.Deserialize<Doc>(json);
            }
        }
    }
    catch
    {
    }

    return doc;
}
```

### Details

- Change the method signature of `Details` to be _asynchronous_, get the doctor
  and return it in the view.

```cs
public async Task<ActionResult> Details(int id)
{
    var doc = await GetDocAsync(id);

    if (doc.physicianId == 0)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = id.ToString(),
            Description = $"Unable to find doctor with id={id}"
        });
    }

    return View(doc);
}
```

- Right-click in the `Details` method and select `Add View...` select
  `Razor View`, click **Add**.
- Set `View name` to `Details`, `Template` to `Details`, `Model` class to
  `Doc (Doctors.Models)`, click **Add**.

## Details.cshtml

- Remove the physician id and update the `Edit` link.

```html
<!-- ... -->
<dl class="row">
    <!-- <dt> class = "col-sm-2">
        @Html.DisplayNameFor(model => model.physicianId)
    </dt>
    <dd> class = "col-sm-10">
        @Html.DisplayFor(model => model.physicianId)
    </dd> -->
    <dt> class = "col-sm-2">
        @Html.DisplayNameFor(model => model.firstName)
    </dt>
<!-- ... -->

<div>
    @Html.ActionLink("Edit", "Edit", new { id = Model.physicianId }) |
    <a asp-action="Index">Back to List</a>
</div>
```

- Save the file.
- Run the site and access the details of a physician.

## DocsController.cs

### Create

- Update the **POST** `Create` method signature to be _asynchronous_ and accept
  a `Doc` object.

```cs
public async Task<ActionResult> Create(Doc doc)
{
    // ...
}
```

- If the model state is valid, serialize the `Doc` object into **JSON**.
- Add the `using System.Text;` directive and set up a call to the web server.
- Call the web server.

```cs
public async Task<ActionResult> Create(Doc doc)
{
    try
    {
        if (ModelState.IsValid)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(doc),
                Encoding.UTF8, appJson);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue(appJson));

                client.BaseAddress = new Uri(baseUrl);

                var response = await client
                    .PostAsync("physicians", content);
            }
        }

        return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = HttpContext.TraceIdentifier,
            Description = ex.Message
        });
    }
}
```

- Right-click in the `Create` method and select `Add View...` select
  `Razor View`, click **Add**.
- Set `View name` to `Create`, `Template` to `Create`, `Model` class to
  `Doc (Doctors.Models)`, click **Add**.

## Create.cshtml

- Remove the physician id and set _autofocus_ to first name.

```html
<!-- ... -->
<form asp-action="Create">
    <div asp-validation-summary="ModelOnly" class="text-danger"></div>
    <!-- <div class="form-group">
        <label asp-for="physicianId" class="control-label"></label>
        <input asp-for="physicianId" class="form-control" />
        <span asp-validation-for="physicianId" class="text-danger"></span>
    </div> -->
    <div class="form-group">
        <label asp-for="firstName" class="control-label"></label>
        <input asp-for="firstName" class="form-control" autofocus />
        <span asp-validation-for="firstName" class="text-danger"></span>
    </div>
    <!-- ... -->
```

- Run the site and create a new physician.

## DocsController.cs

### Edit

- Change the **GET** `Edit` method signature to be _asynchronous_, get the
  doctor and return it in the view.

```cs
public async Task<ActionResult> Edit(int id)
{
    var doc = await GetDocAsync(id);

    if (doc.physicianId == 0)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = id.ToString(),
            Description = $"Unable to find doctor with id={id}"
        });
    }

    return View(doc);
}
```

- Update the **POST** `Edit` method signature to be _asynchronous_ and accept a
  `Doc` object.

```cs
public async Task<ActionResult> Edit(int id, Doc doc)
{
    // ...
}
```

- If the model state is valid, serialize the `Doc` object into **JSON** and set
  up a call to the web server.
- Call the web server.

```cs
public async Task<ActionResult> Edit(int id, Doc doc)
{
    try
    {
        if (ModelState.IsValid)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(doc),
                Encoding.UTF8, appJson);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept
                    .Add(new MediaTypeWithQualityHeaderValue(appJson));

                client.BaseAddress = new Uri(baseUrl);

                var response = await client
                    .PutAsync($"physicians/{id}", content);

                if (!response.IsSuccessStatusCode)
                {
                    return View("Error", new ErrorViewModel
                    {
                        RequestId = HttpContext.TraceIdentifier,
                        Description = response.StatusCode.ToString()
                    });
                }
            }
        }

        return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = HttpContext.TraceIdentifier,
            Description = ex.Message
        });
    }
}
```

- Right-click in the `Create` method and select `Add View...` select
  `Razor View`, click **Add**.
- Set `View name` to `Edit`, `Template` to `Edit`, `Model` class to
  `Doc (Doctors.Models)`, click **Add**.

## Edit.cshtml

- Hide the physician id and set _autofocus_ to first name.

```html
<!-- ... -->
<form asp-action="Edit">
    <div> asp-validation-summary="ModelOnly" class="text-danger"></div>
    <!-- <div class="form-group">
        <label> asp-for="physicianId" class="control-label"></label>
        <input asp-for="physicianId" class="form-control" type="hidden" />
        <span> asp-validation-for="physicianId" class="text-danger"></span>
    </div> -->
    <div class="form-group">
        <label> asp-for="firstName" class="control-label"></label>
        <input asp-for="firstName" class="form-control" autofocus />
        <span> asp-validation-for="firstName" class="text-danger"></span>
    </div>
    <!-- ... -->
```

- Run the site and edit the recently created new physician.

## DocsController.cs

### Delete

- Change the method signature of **GET** `Delete` to be _asynchronous_, get the
  doctor and return it in the view.

```cs
public async Task<ActionResult> Delete(int id)
{
    var doc = await GetDocAsync(id);

    if (doc.physicianId == 0)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = id.ToString(),
            Description = $"Unable to find doctor with id={id}"
        });
    }

    return View(doc);
}
```

- Update the **POST** `Delete` method signature to be _asynchronous_.

```cs
public async Task<ActionResult> Delete(int id, IFormCollection collection)
{
    // ...
}
```

- Set up a call to the web server.
- Call the web server.

```cs
public async Task<ActionResult> Delete(int id, IFormCollection collection)
{
    try
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri(baseUrl);

            var response = await client
                .DeleteAsync($"physicians/{id}");

            if (!response.IsSuccessStatusCode)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier,
                    Description = response.StatusCode.ToString()
                });
            }
        }

        return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = HttpContext.TraceIdentifier,
            Description = ex.Message
        });
    }
}
```

- Right-click in the `Delete` method and select `Add View...` select
  `Razor View`, click **Add**.
- Set `View name` to `Delete`, `Template` to `Delete`, `Model` class to
  `Doc (Doctors.Models)`, click **Add**.

## Delete.cshtml

- Remove the physician id.

```html
<!-- ... -->
<dl class="row">
    <!-- <dt class = "col-sm-2">
        @Html.DisplayNameFor(model => model.physicianId)
    </dt>
    <dd class = "col-sm-10">
        @Html.DisplayFor(model => model.physicianId)
    </dd> -->
    <dt class = "col-sm-2">
        @Html.DisplayNameFor(model => model.firstName)
    </dt>
    <!-- ... -->
```

- Run the site and delete the recently created new physician.
