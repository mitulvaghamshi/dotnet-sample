# Walkthrough 11 - MVC Filtering

## Setup

This walkthrough will add filtering by a dropdown list to the `Hospital` site.

- Start SQL Server.
- Open `Hospital` from the end of the previous walkthrough.

## CurrentAdmissionsController.cs

- Right-click the `Controllers` folder and select `Add / Controller...`
- Choose `MVC Controller with views, using Entity Framework`, click **Add**.
- Set `Model` class to `Admissions (Hospital.Models)`.
- Set `Data context` class to `CHDBContext (Hospital.Models)`.
- Set the controller name to `CurrentAdmissionsController`, click **Add**.

## _Layout.cshtml

- Open `Views/Shared/_Layout.cshtml` and add a link for the `CurrentAdmissions`
  controller.

```html
<!-- ... -->
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
</li>
<li class="nav-item">
    <a class="nav-link text-dark" asp-area="" asp-controller="CurrentAdmissions" asp-action="Index">Current Admissions</a>
</li>
<!-- ... -->
```

- Save the file.
- Run the site.
- Click the `Current Admissions` link.
- Over 5,000 records are returned; this isn't great for performance.

## CurrentAdmissionsController.cs

- Update the `Index` method to use **LINQ** to retrieve current admissions only
  (`DischargeDate` of `null`) and to sort by `Patient`s `LastName`, then
  `FirstName`.

```cs
public async Task<IActionResult> Index()
{
    var admissions = from a in _context.Admissions
        .Include(a => a.Patient)
        .Where(a => a.DischargeDate == null)
        .OrderBy(a => a.Patient.LastName)
        .ThenBy(a => a.Patient.FirstName) select a;

    return View(await admissions.AsNoTracking().ToListAsync());
}
```

- Save the file, refresh the site.

## Index.cshtml

- Open `Views/CurrentAdmissions/Index.cshtml`.
- Update the title and heading.

```html
@model IEnumerable<Hospital.Models.Admissions>

@{ ViewData["Title"] = "Current Admissions"; }

<h1>@ViewBag.Title</h1>

<p>
    <a asp-action="Create">Create New</a>

<!-- ... -->
```

- Update the table headings.

```html
<!-- ... -->
<table class="table">
    <thead>
        <tr>
            <th>
                Patient
            </th>
            <th>
                @Html.DisplayNameFor(model => model.PrimaryDiagnosis)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.SecondaryDiagnoses)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Room)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Bed)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.AttendingPhysician)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.NursingUnit)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.Patient)
            </th>
            <th></th>
        </tr>
    </thead>
    <!-- ... -->
```

- Update the table row.

```html
<!-- ... -->
@foreach (var item in Model) {
    <tr>
        <td>
            @Html.DisplayFor(modelItem => item.Patient.LastName),
            @Html.DisplayFor(modelItem => item.Patient.FirstName)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.PrimaryDiagnosis)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.SecondaryDiagnoses)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Room)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Bed)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.AttendingPhysician.PhysicianId)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.NursingUnit.NursingUnitId)
        </td>
        <td>
            @Html.DisplayFor(modelItem => item.Patient.PatientId)
        </td>
        <td>
            @Html.ActionLink("Edit", "Edit", new { /* id=item.PrimaryKey */ }) |
            @Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }) |
            @Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })
        </td>
    </tr>
}
<!-- ... -->
```

- Save the file, refresh the site.

## CurrentAdmissionsController.cs

- Update the `Index` method to accept a parameter for `NursingUnitId`, populate
  a select list of nursing units and to apply a `Where` clause to the admissions
  data source if there is a value.

```cs
public async Task<IActionResult> Index(string nursingUnitId)
{
    ViewBag.NursingUnitId = new SelectList(
        _context.NursingUnits.ToList(), "NursingUnitId", "NursingUnitId");

    var admissions = from a in _context.Admissions
        .Include(a => a.Patient)
        .Where(a => a.DischargeDate == null)
        .OrderBy(a => a.Patient.LastName)
        .ThenBy(a => a.Patient.FirstName) select a;

    if (!string.IsNullOrEmpty(nursingUnitId))
    {
        admissions = admissions.Where(a => a.NursingUnitId == nursingUnitId);
    }

    return View(await admissions.AsNoTracking().ToListAsync());
}
```

- Save the file.

## Index.cshtml

- Add a dropdown list to display the available nursing units and a link to
  reset.
- The dropdown list will cause the form to submit when its value changes.

```html
<!-- ... -->

    <a asp-action="Create">Create New</a>
</p>

<form asp-action="Index" method="get">
    <p>
        Nursing Unit Id:
        <select asp-for="@Model.FirstOrDefault().NursingUnitId"
                asp-items="@ViewBag.NursingUnitId"
                onchange="submit()"></select>
        | <a asp-action="Index">All Current Admissions</a>
    </p>
</form>

<table class="table">
<!-- ... -->
```

- Save the file, refresh the site.
- As an alternative, remove the reset link and add an "empty" item to the
  dropdown list, which will cause reset.

```html
<!-- ... -->
<form asp-action="Index" method="get">
    <p>
        Nursing Unit Id:
        <select asp-for="@Model.FirstOrDefault().NursingUnitId"
                asp-items="@ViewBag.NursingUnitId"
                onchange="submit()">
            <option value="">All Current Admissions</option>
        </select>
    </p>
</form>
<!-- ... -->
```

- Save the file, refresh the site.

## CurrentAdmissionsController.cs

- Add a data source that retrieves the nursing unit manager name and display
  that in the dropdown list.

```cs
public async Task<IActionResult> Index(string nursingUnitId)
{
    var nursingUnits = from n in _context.NursingUnits orderby n.ManagerLastName
        select new { Name = n.ManagerFirstName + " " + n.ManagerLastName, n.NursingUnitId };

    ViewBag.NursingUnitId = new SelectList(nursingUnits, "NursingUnitId", "Name");

    // ...

    return View(await admissions.AsNoTracking().ToListAsync());
}
```

- Save the file.

## Index.cshtml

- Update the label text.

```html
<!-- ... -->
<form asp-action="Index" method="get">
    <p>
        Nursing Unit Manager:
        <select asp-for="@Model.FirstOrDefault().NursingUnitId"
                asp-items="@ViewBag.NursingUnitId"
                onchange="submit()">
            <option value="">All Current Admissions</option>
        </select>
    </p>
</form>
<!-- ... -->
```

- Save the file, refresh the site.

## CurrentAdmissionsController.cs

- Update the page title programmatically instead of hard-coding it in the view.

```cs
public async Task<IActionResult> Index(string nursingUnitId)
{
    // ...

    ViewBag.Title = "Current Admissions";

    if (!string.IsNullOrEmpty(nursingUnitId))
    {
        ViewBag.Title = $"Current Admissions - {nursingUnitId}";
        admissions = admissions.Where(a => a.NursingUnitId == nursingUnitId);
    }

    return View(await admissions.AsNoTracking().ToListAsync());
}
```

- Save the file.

## Index.cshtml

- Delete the hard-coded title.

```cs
@model IEnumerable<Hospital.Models.Admission>

<h1>@ViewBag.Title</h1>

// ...
```

- Save the file, refresh the site.
- Notice that the dropdown doesn't quite behave correctly when the page is first
  loaded.
- This is due to using the `FirstOrDefault` method which is making
  `Barbara Parsons, 2SOUTH` the "current" nursing unit.
- Remove the select tag helper and instead use the `Html.DropDownList` helper.

```html
<!-- ... -->
<form asp-action="Index" method="get">
    <p>
        Nursing Unit Manager:
        @Html.DropDownList("nursingUnitId",
            (IEnumerable<SelectListItem>)ViewBag.NursingUnitId,
            "All Current Admissions", new { onchange = "submit()" })
    </p>
</form>
<!-- ... -->
```

- Save the file, refresh the site.

## Index.cshtml

- Examine the `Edit`, `Details` and `Delete` link and note the scaffolder left
  them commented.
- Uncomment them and set them to `PatientId`.

```html
<!-- ... -->
    <td>
        @Html.ActionLink("Edit", "Edit", new { id = item.PatientId }) |
        @Html.ActionLink("Details", "Details", new { id = item.PatientId }) |
        @Html.ActionLink("Delete", "Delete", new { id = item.PatientId })
    </td>
<!-- ... -->
```

- Save the file, refresh the site.
- Set `Nursing Unit Manager` to `All Current Admissions`.
- Try the `Details` link of the first patient `(Aching, Tiffany)`, it appears to
  work.
- Try the `Details` link of the second patient `(Atwater, Mallory)`; notice the
  populated `DischargeDate`, that is not a "current" patient and is incorrect.
- Click the `Back to List` link and try the `Edit` link, it will fail.
- Examine the error.
- Notice that it mentions that the `Admissions` entity needs a composite key.
- Update the links to use the correct composite key.

```html
<!-- ... -->
    <td>
        @Html.ActionLink("Edit", "Edit", new {
            id = item.PatientId,
            admissionDate = item.AdmissionDate
        }) |
        @Html.ActionLink("Details", "Details", new {
            id = item.PatientId,
            admissionDate = item.AdmissionDate
        }) |
        @Html.ActionLink("Delete", "Delete", new {
            id = item.PatientId,
            admissionDate = item.AdmissionDate
        })
    </td>
}
<!-- ... -->
```

- Save the file.

## CurrentAdmissionsController.cs

- Update the `Details` method to use the `admissionDate`.

```cs
public async Task<IActionResult> Details(int? id, DateTime? admissionDate)
{
    if (id == null || admissionDate == null)
    {
        return NotFound();
    }

    var admission = await _context.Admissions
        .Include(a => a.AttendingPhysician)
        .Include(a => a.NursingUnit)
        .Include(a => a.Patient)
        .FirstOrDefaultAsync(m => m.PatientId == id && m.AdmissionDate == admissionDate);

    if (admission == null)
    {
        return NotFound();
    }

    return View(admission);
}
```

- Save the file.

## Details.cshtml

- Update the `Edit` link to use the correct composite key.

```html
<div>
    @Html.ActionLink("Edit", "Edit", new {
        id = Model.PatientId,
        admissionDate = Model.AdmissionDate
    }) |
    <a asp-action="Index">Back to List</a>
</div>
```

- Save the file, refresh the site.
- Try the `Details` link of `Atwater, Mallory`, it will work correctly now.

## CurrentAdmissionsController.cs

- Update the **GET** `Edit` method to use the `admissionDate`.

```cs
public async Task<IActionResult> Edit(int? id, DateTime? admissionDate)
{
    if (id == null || admissionDate == null)
    {
        return NotFound();
    }

    var admission = await _context.Admissions.FirstOrDefaultAsync(a =>
        a.PatientId == id && a.AdmissionDate == admissionDate);

    // ...

    return View(admission);
}
```

- Update `AdmissionExists` and the **POST** `Edit` to use `admissionDate`.

```cs
private bool AdmissionExists(int id, DateTime admissionDate)
{
    return _context.Admissions.Any(e =>
        e.PatientId == id && e.AdmissionDate == admissionDate);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("PatientId,AdmissionDate,DischargeDate,PrimaryDiagnosis,SecondaryDiagnoses,AttendingPhysicianId,NursingUnitId,Room,Bed")] Admission admission)
{
    // ...

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(admission);

            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!AdmissionExists(admission.PatientId, admission.AdmissionDate))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return RedirectToAction(nameof(Index));
    }

    // ...

    return View(admission);
}
```

- Save the file, refresh the site.
- Return to the list of all current patients.
- Note the first patient `(Aching, Tiffany)`.
- Edit that patient's admission record and set the `DischargeDate` to today.
- When returned to the list of all current patients, note that the patient is no
  longer present.
- Update the `Delete` and `DeleteConfirmed` methods to use the `admissionDate`.

```cs
public async Task<IActionResult> Delete(int? id, DateTime? admissionDate)
{
    if (id == null || admissionDate == null)
    {
        return NotFound();
    }

    var admission = await _context.Admissions
        .Include(a => a.AttendingPhysician)
        .Include(a => a.NursingUnit)
        .Include(a => a.Patient)
        .FirstOrDefaultAsync(m => m.PatientId == id && m.AdmissionDate == admissionDate);

    // ...

    return View(admission);
}

[ValidateAntiForgeryToken]
[HttpPost, ActionName("Delete")]
public async Task<IActionResult> DeleteConfirmed(int id, DateTime admissionDate)
{
    var admission = await _context.Admissions.FirstOrDefaultAsync(a =>
        a.PatientId == id && a.AdmissionDate == admissionDate);

   // ...
}
```

- Save the file, refresh the site.
- Delete an admission record.

## Details.cshtml

- Improve the `Details` view by enhancing the heading and removing unnecessary
  fields.

```html
<!-- ... -->

<h1>Current Admission - @Model.Patient.FirstName @Model.Patient.LastName</h1>

<div>
    <h4>Admission</h4>
    <hr />

    <dl class="row">
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.DischargeDate)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.DischargeDate)
        </dd>
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.PrimaryDiagnosis)
        </dt>
        <!-- ... -->
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.NursingUnit.NursingUnitId)
        </dd>
        <dt class = "col-sm-2">
            @Html.DisplayNameFor(model => model.Patient)
        </dt>
        <dd class = "col-sm-10">
            @Html.DisplayFor(model => model.Patient.PatientId)
        </dd>
    </dl>
</div>
<!-- ... -->
```

- Save the file, view the details of a current admission.
