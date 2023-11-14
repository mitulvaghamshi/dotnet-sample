# Walkthrough 12 - Introduction to Web API

## Setup

This will explore **Web API**.

- Start SQL Server.
- Start Visual Studio.
- Click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `ASP.NET Core Web API` template, click `Next`.
- Set Project name to `MedicationAPI`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is unchecked, click
  `Next`.
- Set version to `.NET 5.0`, and:
  - Set `Authentication Type` to `None`
  - Unselect `Configure for HTTPS`
  - Select `Enable OpenAPI` support if necessary
- Click `Create`.

## WeatherForecastController.cs

- Open `Controllers/WeatherForecastController.cs`
- Notice the `Get` method in the controller.
- Press Ctrl+F5 to run the site, the **Swagger Documentation** page will appear,
  more on **Swagger** later.
- Change the URL to http://localhost:12345/weatherforecast; notice that **JSON**
  is returned instead of a web page.
- Delete `WeatherForecastController.cs` and `WeatherForecast.cs`.

## appsettings.json

- Add a **connection string** for the `CHDB` database.

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

- Open the **Package Manager Console** (PMC) and issue the following commands.

```console
Install-Package Microsoft.EntityFrameworkCore.Tools

Install-Package Microsoft.EntityFrameworkCore.SqlServer

## We can skip the argument names, if parameter order is known.
Scaffold-DbContext name=chdb Microsoft.EntityFrameworkCore.SqlServer
  -OutputDir Models -Tables medications
```

## Startup.cs

- Add the directives:

```cs
using MedicationAPI.Models;
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
      { Title = "MedicationAPI", Version = "v1" }));
}
```

- Notice that no default routing has been specified in the `Configure` method;
  the endpoints will be specified in the controller.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // ...
    app.UseEndpoints(endpoints => endpoints.MapControllers());
}
```

- Save the file.

## MedicationsController.cs

- Right-click `Controllers` folder and select `Add / Controller...`
- Select the `API` templates, select
  `API Controller with actions, using Entity Framework`, click **Add**.
- Set `Model` class to `Medication (Medication.Models)`.
- Set `Data context` class to `CHDBContext (Medication.Models)`.
- Accept default name of `MedicationsController`, click **Add**.
- Run the site, navigate to http://localhost:12345/api/medications to see all
  medications.
- Navigate to http://localhost:12345/api/medications/10 to see an individual
  medication.
- Update the routing to not use the word api.

```cs
[Route("[controller]")]
[ApiController]
public class MedicationsController : ControllerBase
{
    private readonly CHDBContext _context;
    // ...
```

- Save the file.
- Refresh the browser, the data won't be found.
- Navigate to http://localhost:12345/medications/11 to see a medication.
- Change the routing to `meds`.

```cs
[Route("meds")]
[ApiController]
public class MedicationsController : ControllerBase
{
    private readonly CHDBContext _context;
    // ...
```

- Save the file.
- Refresh the browser, the data won't be found.
- Navigate to http://localhost:12345/meds/12 to see a medication.
- Change the routing back.

```cs
[Route("[controller]")]
[ApiController]
public class MedicationsController : ControllerBase
{
    private readonly CHDBContext _context;
    // ...
```

- Save the file.
- Refresh the browser, the data won't be found.
- Navigate to http://localhost:12345/medications/13 to see a medication.
- Change the class name to `Temp`, also change the constructor name.

```cs
[Route("[controller]")]
[ApiController]
public class Temp : ControllerBase
{
    private readonly CHDBContext _context;

    public Temp(CHDBContext context)
    {
        _context = context;
    }
    // ...
```

- Save the file, navigate to http://localhost:12345/temp/14 to see a medication.
- Change the name back to `MedicationsController`.

```cs
[Route("[controller]")]
[ApiController]
public class MedicationsController : ControllerBase
{
    private readonly CHDBContext _context;

    public MedicationsController(CHDBContext context)
    {
        _context = context;
    }
    // ...
```

- Save the file, navigate to http://localhost:12345/medications/15 to see a
  medication.
- Change the `GetMedication` method name to `Temp`.

```cs
[HttpGet("{id}")]
public async Task<ActionResult<Medication>> Temp(int id)
{
// ...
```

- Save the file.
- Refresh the browser, the data remains, because the routing hasn't changed.
- Change the method name back to `GetMedication`.

```cs
[HttpGet("{id}")]
public async Task<ActionResult<Medication>> GetMedication(int id)
{
// ...
```

- Save the file.
- Refresh the browser, the data remains.
- Close the browser.

## launchSettings.json

- In Solution Explorer, expand `Properties` and select `launchSettings.json`.
- Update the `launchUrl` to `medications`.

```json
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:4105",
      "sslPort": 0
    }
  },
  "$schema": "http://json.schemastore.org/launchsettings.json",
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "launchUrl": "medications",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "MedicationAPI": {
      "commandName": "Project",
      "launchBrowser": true,
      "launchUrl": "medications",
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "dotnetRunMessages": "true",
      "applicationUrl": "http://localhost:5000"
    }
  }
}
```

- Save the file.
- Run the site, the `medications` endpoint is served.

## Swagger

- Navigate to http://localhost:12345/swagger.
- Under the `MedicationAPI` heading, click the `/swagger/v1/swagger.json` link
  to see the generated documentation.
- In the `Schemas` section, expand `Medication` to see the **JSON**
  representation of the `Medication` class.

## MedicationsController

- Highlight all of the `DeleteMedication` method.
- Press `Ctrl+K, Ctrl+C` to comment it.

```cs
//[HttpDelete("{id}")]
//public async Task<IActionResult> DeleteMedication(int id)
//{
//    var medication = await _context.Medications.FindAsync(id);
//    if (medication == null)
//    {
//        return NotFound();
//    }

//    _context.Medications.Remove(medication);
//    await _context.SaveChangesAsync();

//    return NoContent();
//}
```

- Save the file.
- Refresh the browser, notice that the `Delete` verb is no longer documented.
- If necessary, highlight all of the `DeleteMedication` method.
- Press `Ctrl+K, Ctrl+U` to uncomment it.

```cs
[HttpDelete("{id}")]
public async Task<IActionResult> DeleteMedication(int id)
{
    var medication = await _context.Medications.FindAsync(id);

    if (medication == null)
    {
        return NotFound();
    }

    _context.Medications.Remove(medication);

    await _context.SaveChangesAsync();

    return NoContent();
}
```

- Save the file.

## Swagger

- Refresh the browser, notice that the `Delete` verb is documented again.
- Click the `GET` button with `/Medications`.
- Click the `Try it out` button, click the `Execute` button.
- The status code `200` will be returned along with all medications in **JSON**.
- Click the `GET` button again to collapse the section.
- Click the `GET` button with `/MedicationAPI/{id}`.
- Click the `Try it out` button, click the `Execute` button.
- Notice that the `id` parameter is _required_.
- Set it to `1` and click `Execute`.
- Only the 1st medication is returned.
- Change the `id` parameter to `200`, click `Execute`.
- This medication doesn't exist, so `404` is returned.
- Change the `id` parameter back to `1`, click `Execute`.
- Select the `Response` body and copy it (`Ctrl+C`).
- Collapse the section.
- Click the `PUT` button.
- Click `Try it out`.
- Set `id` to `1`.
- Select the template `Request body` and delete (clear) it.
- Paste in the copied body (`Ctrl+V`) and change the `description` to `Advil`.

```json
{
  "medicationId": 1,
  "medicationDescription": "Advil",
  "medicationCost": 1.23,
  "packageSize": "50 Tablets",
  "strength": "10 MG",
  "sig": "PRN",
  "unitsUsedYtd": 1231,
  "lastPrescribedDate": "2019-05-22T00:00:00"
}
```

- Click `Execute`, `204` should be returned indicating success.
- Collapse the section.
- Click `GET` `/Medications/{id}`.
- Click `Execute`, the updated medication will be returned.
- Collapse the section.
- Click `POST`, click `Try it out`.
- Replace the `Request body` with the previously copied medication.
- Change the `medication id` to `170` and the `description` to `Ibuprofen`.

```json
{
  "medicationId": 170,
  "medicationDescription": "Ibuprofen",
  "medicationCost": 1.23,
  "packageSize": "50 Tablets",
  "strength": "10 MG",
  "sig": "PRN",
  "unitsUsedYtd": 1231,
  "lastPrescribedDate": "2019-05-22T00:00:00"
}
```

- Click `Execute`, `201` should be returned indicating success.
- Collapse the section.
- Click `GET` `/Medications`.
- Click `Clear`, then `Execute`.
- Scroll to the bottom of the `Response` body to see the newly added medication.
- Collapse the section.
- Click `DELETE`, click `Try it out`, set `id` to `170`, click `Execute`.
- `200` should be returned indicating success.
- Click `GET` `/Medications`.
- Click `Clear`, then `Execute`.
- Scroll to the bottom of the `Response` body to see the newly added medication
  is now gone.
- Collapse the section.

## MedicationAPITest

- Add a test project to the solution.
- Right-click the `Solution` and select `Add / New Project...`
- Set language to `C#` and project type to `Test`.
- Select `xUnit Test Project`, click `Next`.
- Set Name to `MedicationAPITest`, leave Location as is, click `Next`.
- Set version to `.NET 5.0`, click `Create`.
- Right-click the new project and select `Add / Project Reference...`
- Select `MedicationAPI`, click `OK`.
- In the **PMC**, change the `Default` project drop-down list (near the top of
  the **PMC**) to `MedicationAPITest`.
- Issue the command:

```console
Install-Package Microsoft.EntityFrameworkCore.InMemory
```

## UnitTest1.cs

- Add the directives:

```cs
using MedicationAPI.Models;
using Microsoft.EntityFrameworkCore;
```

- Initialize the in-memory database.

```cs
public class UnitTest1
{
    private CHDBContext context;

    public UnitTest1()
    {
        var options = new DbContextOptionsBuilder<CHDBContext>()
          .UseInMemoryDatabase(databaseName: "Medication_Tests")
          .Options;

        context = new CHDBContext(options);

        context.Medications.AddRange(
            new Medication
            {
                MedicationId = 1,
                MedicationDescription = "Pain Reliever",
                MedicationCost = 1.0M
            },
            new Medication
            {
                MedicationId = 2,
                MedicationDescription = "Ibuprofen",
                MedicationCost = 1.25M
            },
            new Medication
            {
                MedicationId = 3,
                MedicationDescription = "Advil",
                MedicationCost = 1.5M
            }
        );

        context.SaveChanges();
    }

    [Fact]
    public void Test1()
    {
    }
}
```

- Add the directives.

```cs
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using MedicationAPI.Controllers;
```

- Update the test method.

```cs
[Fact]
public async Task Get_NoInput_ReturnsMedications()
{
    // Arrange
    var medicationsController = new MedicationsController(context);

    // Act
    var actionResult = await medicationsController.GetMedications();

    // Assert
    Assert.IsType<ActionResult<IEnumerable<Medication>>>(actionResult);

    var genericMedications = actionResult.Value;
    var medications = genericMedications as List<Medication>;

    Assert.Equal(3, medications.Count);
    Assert.Equal(1, medications[0].MedicationId);
    Assert.Equal("Ibuprofen", medications[1].MedicationDescription);
    Assert.Equal(1.5M, medications[2].MedicationCost);
}
```

- Build the project, run the test.
