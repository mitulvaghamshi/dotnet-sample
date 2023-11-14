# Walkthrough 9 - EF Core Database Scaffolding, Sorting and Searching

## Setup

This walkthrough will create an MVC application to help **manage a hospital**
and demonstrate the capability of the database **scaffolder** and introduce
**sorting** and **searching**.

- Start SQL Server.
- Start Visual Studio.
- Click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `ASP.NET Core Web App (Model-View-Controller)` template, click
  `Next`.
- Set Project name to `Hospital`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is not selected, click
  `Next`.
- Set version to `.NET 5.0`, unselect `Configure for HTTPS`, click `Create`.
- Download SQL script: [CHDB.sql.zip](../resources/CHDB.sql.zip) and drag it
  into the project in the Solution Explorer.

## CHDB.sql

- Extract and open `CHDB.sql`.
- In the upper-left, click the `Execute` button.
- You will be prompted to connect to a database, set `Server Name` to
  `localhost\sqlexpress`, click `Connect`.
- The script should complete and create the database.

## appsettings.json

- Add a connection string for the `CHDB` database.

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

- Open the **Package Manager Console** (PMC) and issue the following commands:

```console
PM> Install-Package Microsoft.EntityFrameworkCore.Tools

PM> Install-Package Microsoft.EntityFrameworkCore.SqlServer

PM> Scaffold-DbContext
	-Connection name=CHDB
	-Provider Microsoft.EntityFrameworkCore.SqlServer
	-Context CHDBContext
	-OutputDir Models
	-ContextDir Data
	-DataAnnotations
```

- A `Data` and a `Models` folder will be created.
- The `CHDBContext.cs` file will be open; briefly examine it.
- Open the `Models` folder and note that all `CHDB` tables have been modeled.
- Open the `Patients.cs` file and examine the class.

## Startup.cs

- Add the following directives:

```cs
using Hospital.Data;
using Microsoft.EntityFrameworkCore;
```

- The scaffolder doesn't inject the database into the `Startup` class. Do this
  manually.

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    services.AddDbContext<CHDBContext>(options => options
        .UseSqlServer(Configuration.GetConnectionString("CHDB")));
}
```

- Save the file.

## MedicationsController.cs

- Right-click the `Controller` folder and select `Add / Controller...`
- Choose `MVC Controller with views, using Entity Framework`, click **Add**.
- Set `Model` class to `Medications (Hospital.Models)`.
- Set `Data context` class to `CHDBContext (Hospital.Data)`.
- Accept default name of `MedicationsController`, click **Add**.
- Examine the code.

## _Layout.cshtml

- Open `Views / Shared / _Layout.cshtml` and add a link for the `Medications`
  controller.

```html
<!-- ... -->
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Medications" asp-action="Index">Medications</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
</li>
<!-- ... -->
```

- Save the file.
- From the **PMC**, issue the following command to run the site:

```console
PM> dotnet watch run --quite --project HospitalMVC.csproj
```

- Click the `Medications` link.

## Medication.cs

- Add some _annotations_ to enhance output.

```cs
[Table("medications")]
public partial class Medication
{
    public Medication()
    {
        UnitDoseOrders = new HashSet<UnitDoseOrder>();
    }

    [Key]
    [Column("medication_id")]
    public int MedicationId { get; set; }

    [Required]
    [StringLength(25)]
    [Display(Name = "Description")]
    [Column("medication_description")]
    public string MedicationDescription { get; set; }

    [Display(Name = "Cost")]
    [Column("medication_cost", TypeName = "decimal(9, 2)")]
    public decimal? MedicationCost { get; set; }

    [StringLength(20)]
    [Display(Name = "Package Size")]
    [Column("package_size")]
    public string PackageSize { get; set; }

    [StringLength(20)]
    [Column("strength")]
    public string Strength { get; set; }

    [StringLength(20)]
    [Column("sig")]
    public string Sig { get; set; }

    [Display(Name = "Units Used YTD")]
    [Column("units_used_ytd")]
    public int? UnitsUsedYtd { get; set; }

    [DataType(DataType.Date)]
    [Display(Name = "Last Prescribed")]
    [Column("last_prescribed_date", TypeName = "date")]
    public DateTime? LastPrescribedDate { get; set; }

    [InverseProperty(nameof(UnitDoseOrder.Medication))]
    public virtual ICollection<UnitDoseOrder> UnitDoseOrders { get; set; }
}
```

- Save the file, the browser should refresh automatically.

## Index.cshtml

- Trim a couple of columns from the view to make it narrower.

```html
<!-- ... -->
<th>
    @Html.DisplayNameFor(model => model.PackageSize)
</th>
<th>
    @Html.DisplayNameFor(model => model.Strength)
</th>
<th>
    @Html.DisplayNameFor(model => model.Sig)
</th>
<th>
    @Html.DisplayNameFor(model => model.UnitsUsedYtd)
</th>
<!-- ... -->
<td>
    @Html.DisplayFor(modelItem => item.PackageSize)
</td>
<td>
    @Html.DisplayFor(modelItem => item.Strength)
</td>
<td>
    @Html.DisplayFor(modelItem => item.Sig)
</td>
<td>
    @Html.DisplayFor(modelItem => item.UnitsUsedYtd)
</td>
<!-- ... -->
```

- Save the file.
- `Cost` and `UnitsUsedYTD` are _numeric_ and should be _right-justified_.
- Add a **CSS** class for this.

```html
<!-- ... -->
<th class="text-end">
    @Html.DisplayNameFor(model => model.MedicationCost)
</th>
<!-- ... -->
<th class="text-end">
    @Html.DisplayNameFor(model => model.UnitsUsedYtd)
</th>
<!-- ... -->
<td class="text-end">
    @Html.DisplayFor(modelItem => item.MedicationCost)
</td>
<!-- ... -->
<td class="text-end">
    @Html.DisplayFor(modelItem => item.UnitsUsedYtd)
</td>
<!-- ... -->
```

- Save the file.

## Create.cshtml

- Click the `Create New` link.
- Notice that the primary key is available to be specified by the user.
- This isn't ideal.
- It happened because the scaffolder recognized that the `medications` table
  doesn't have an _auto-increment_ for `MedicationId`.
- Delete `MedicationId` and give _autofocus_ to `MedicationDescription`.

```html
<!-- ... -->
<form asp-action="Create">
    <div asp-validation-summary="ModelOnly" class="text-danger"></div>
    <div class="form-group">
        <label asp-for="MedicationId" class="control-label"></label>
        <input asp-for="MedicationId" class="form-control" />
        <span asp-validation-for="MedicationId" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="MedicationDescription" class="control-label"></label>
        <input asp-for="MedicationDescription" class="form-control" autofocus />
        <span asp-validation-for="MedicationDescription" class="text-danger"></span>
    </div>
<!-- ... -->
```

- Save the file.

## MedicationsController.cs

- Update the `POST` `Create` method to compute the new `MedicationId`.

```cs
[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Create([Bind("MedicationId,MedicationDescription,MedicationCost,PackageSize,Strength,Sig,UnitsUsedYtd,LastPrescribedDate")] Medication medication)
{
    if (ModelState.IsValid)
    {
        var maxId = _context.Medications.Max(m => m.MedicationId);

        medication.MedicationId = maxId + 1;
        _context.Add(medication);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    return View(medication);
}
```

- Save the file.
- Add a new medication.

## ErrorViewModel.cs

- Attempt to delete an existing medication, it should fail with a referential
  integrity exception.
- Add a `Description` property to the `ErrorModel`.

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

- Add some code for the new `Description` property and remove the
  `Development Mode` text.

```html
@model ErrorViewModel

@{ ViewData["Title"] = "Error"; }

<h1 class="text-danger">Error.</h1>
<h2 class="text-danger">An error occurred while processing your request.</h2>

@if (Model.ShowRequestId)
{
    <p><strong>Request ID:</strong> @Model.RequestId</p>
}

<p><strong>Description:</strong> @Model.Description</p>
```

- Save the file.

## MedicationsController.cs

- Add a `try/catch` to `DeleteConfirmed`.

```cs
[ValidateAntiForgeryToken]
[HttpPost, ActionName("Delete")]
public async Task<IActionResult> DeleteConfirmed(int id)
{
    try
    {
        var medication = await _context.Medications.FindAsync(id);

        _context.Medications.Remove(medication);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    catch
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = id.ToString(),
            Description = $"Unable to delete medication id {id}. It is referenced by other data in the system."
        });
    }
}
```

- Save the file.
- Attempt another delete, the improved error message will be displayed.
- In the **PMC**, click the stop button to end the web server.

## Sorting

- We are going to add functionality to allow the user to sort the data by either
  the `MedicationDescription` or `MedicationCost` column.
- Update the `Index` method to accept a parameter for sort order.
- Add two `ViewBag` entries that will track the state of the sort parameters.

```cs
public async Task<IActionResult> Index(string sortOrder)
{
    ViewBag.DescriptionSortParm = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
    ViewBag.CostSortParm = sortOrder == "cost" ? "cost_desc" : "cost";

    return View(await _context.Medications.ToListAsync());
}
```

- Use **LINQ** to query the database.

```cs
public async Task<IActionResult> Index(string sortOrder)
{
    ViewBag.DescriptionSortParm = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
    ViewBag.CostSortParm = sortOrder == "cost" ? "cost_desc" : "cost";

    var medications = from m in _context.Medications select m;

    return View(await _context.Medications.ToListAsync());
}
```

- Sort the result set according to the current sort parameter.

```cs
public async Task<IActionResult> Index(string sortOrder)
{
    ViewBag.DescriptionSortParm = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
    ViewBag.CostSortParm = sortOrder == "cost" ? "cost_desc" : "cost";

    var medications = from m in _context.Medications select m;

    switch (sortOrder)
    {
        case "description_desc":
            medications = medications.OrderByDescending(m => m.MedicationDescription);
            break;
        case "cost":
            medications = medications.OrderBy(m => m.MedicationCost);
            break;
        case "cost_desc":
            medications = medications.OrderByDescending(m => m.MedicationCost);
            break;
        default:
            medications = medications.OrderBy(m => m.MedicationDescription);
            break;
    }

    return View(await _context.Medications.ToListAsync());
}
```

- Return the `medications` result set, which is sorted, with _change tracking
  disabled_.

```cs
public async Task<IActionResult> Index(string sortOrder)
{
    // ...

    return View(await _context.Medicationsmedications.AsNoTracking().ToListAsync());
}
```

- Save the file.

## Index.cshtml

- Open `Views / Medications / Index.cshtml` and add column heading hyperlinks to
  `MedicationDescription` and `MedicationCost`.

```html
<!-- ... -->
<th>
    <a asp-action="Index"
       asp-route-sortOrder="@ViewBag.DescriptionSortParm">
        @Html.DisplayNameFor(model => model.MedicationDescription)
    </a>
</th>
<th>
    <a asp-action="Index"
       asp-route-sortOrder="@ViewBag.CostSortParm">
        @Html.DisplayNameFor(model => model.MedicationCost)
    </a>
</th>
<!-- ... -->
```

- Update the `Title` and heading.

```html
@model IEnumerable<Hospital.Models.Medications>

@{
    ViewData["Title"] = "Medications";
}

<h1>@ViewData["Title"]</h1>
<p>
    <a asp-action="Create">Create New</a>
</p>
```

- Save the file.

## MedicationsController.cs

- Add a breakpoint (F9) to the beginning of the `Index` method.
- Run in debug mode (F5).
- Notice the new hyperlinks, hover over them and notice their URLs, try each of
  them several times and note what is happening with `sortOrder`.
- Once done, stop debugging (Shift+F5) and remove the breakpoint.

### Searching

- We are going to add functionality to allow the user to search by the
  `MedicationDescription` column.
- Update the `Index` method to accept an additional parameter for search.
- Add a `ViewBag` entry that will track the search parameter.

```cs
public async Task<IActionResult> Index(string sortOrder, string searchString)
{
    ViewBag.DescriptionSortParm = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
    ViewBag.CostSortParm = sortOrder == "cost" ? "cost_desc" : "cost";
    ViewBag.SearchString = searchString;

    var medications = from m in _context.Medications

    // ...
```

- Use **LINQ** to search the result set.

```cs
public async Task<IActionResult> Index(string sortOrder, string searchString)
{
    ViewBag.DescriptionSortParm = string.IsNullOrEmpty(sortOrder) ? "description_desc" : "";
    ViewBag.CostSortParm = sortOrder == "cost" ? "cost_desc" : "cost";
    ViewBag.SearchString = searchString;

    var medications = from m in _context.Medications select m;

    if (!string.IsNullOrEmpty(searchString))
    {
        medications = medications
            .Where(m => m.MedicationDescription.Contains(searchString));
    }

    switch (sortOrder)

    // ...
```

- Save the file.

## Index.cshtml

- Add a text box for the search string, a button to perform the search and a
  link to clear the search.

```html
@model IEnumerable<Hospital.Models.Medications>

@{
    ViewData["Title"] = "Medications";
}

<h1>@ViewData["Title"]</h1>

<p>
    <a asp-action="Create">Create New</a>
</p>

<form asp-action="Index" method="get">
    <p>
        Search by Description: <input type="text" name="searchString" value="@ViewBag.SearchString" />
        <input type="submit" value="Search" class="btn btn-primary" /> |
        <a asp-action="Index">All Medications</a>
    </p>
</form>

<table class="table">
```

- Save the file.
- Run the site and test the search.
- Do a search, then do a sort.
- Notice that the search is lost.
- Update the routing for the search links with the search string.

```html
<!-- ... -->
<th>
    <a asp-action="Index"
        asp-route-sortOrder="@ViewBag.DescriptionSortParm"
        asp-route-searchString="@ViewBag.SearchString">
        @Html.DisplayNameFor(model => model.MedicationDescription)
    </a>
</th>
<th>
    <a asp-action="Index"
        asp-route-sortOrder="@ViewBag.CostSortParm"
        asp-route-searchString="@ViewBag.SearchString">
        @Html.DisplayNameFor(model => model.MedicationCost)
    </a>
</th>
<!-- ... -->
```

- Save the file.
- Run the site and test the search.
- Do a search, then do a sort.
- Notice that the search isn't lost.
