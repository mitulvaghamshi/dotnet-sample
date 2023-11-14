# Walkthrough 15 - Authentication and Authorization

## Setup

This will explore ASP.NET Core Identity.

- Start SQL Server.
- Start Visual Studio.
- Click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `ASP.NET Core Web App (Model-View-Controller)` template, click
  `Next`.
- Set Project name to `SecureSite`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is not selected, click
  `Next`.
- Set version to `.NET 5.0`, set `Authentication Type` to `Individual Accounts`,
  leave `Configure for HTTPS` _selected_, click `Create`.

## Data

- Expand the `Data` folder and open `ApplicationDbContext.cs`.
- Note that `ApplicationDbContext` _inherits_ `IdentityDbContext`, which itself
  _inherits_ from `DbContext`.
- Expand the `Migrations` folder and examine the two generated files.

## appsettings.json

- Update the _connection string_ for a new **SQL Server Express** database named
  `SecureSite-Identity`.

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost\\sqlexpress;Database=SecureSite-Identity;Trusted_Connection=True;MultipleActiveResultSets=true"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```

- Save the file.

## Update-Database

- Open the **Package Manager Console** (PMC) and issue the following command:

```console
Update-Database
```

- Open SQL Server Object Explorer and examine the new database.

## Create a User

- Run the site.
- Click the `Privacy` link to verify you can access it.
- Notice the `Register` and `Login` links on the right.
- Click `Login`, note the `Use another service to log in` message.
- Click `Register` as a new user.
- Click the `Register` button, note the _error messages_.
- Enter an _invalid email address_ and press the `Tab` key.
- Enter a _valid email address_.
- Set _password_ and _confirm password_ to `abc`.
- Set _passwords_ to `abcdef`, click the `Register` button.
- Note the _error messages_.
- Set `passwords` to `Abcd5!` and `Register`.
- In SQL Server Object Explorer, view the data of the `AspNetUsers` table.
- Notice that `EmailConfirmed` is `False`.
- Back in the browser, on the `Register` confirmation page that appears, click
  the `Click here to confirm your account` link.
- In SQL Server Object Explorer, click the `Refresh` button to see that
  `EmailConfirmed` is now `True`.
- Leave this window open.
- Login with the newly created account.
- Click the `Hello email address` link.
- Explore the various account management options.
- Click the `Logout` link.
- Register and confirm another user with the same _password_ `Abcd5!`.
- Refresh the `AspNetUsers` data and notice that even though the _passwords_
  were the same, due to **salting**, the `PasswordHash` values are different.

## _Layout.cshtml

- Open `Views/Shared/_Layout.cshtml`
- Notice in the `navbar` _div_, there is a partial view named `_LoginPartial`

```html
<!-- ... -->
<div class="navbar-collapse collapse d-sm-inline-flex justify-content-between">
    <ul class="navbar-nav flex-grow-1">
        <li class="nav-item">
            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
        </li>
        <li class="nav-item">
            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Privacy">Privacy</a>
        </li>
    </ul>
    <partial name="_LoginPartial" />
</div>
<!-- ... -->
```

## _LoginPartial.cshtml

- Open `Views/Shared/_LoginPartial.cshtml`
- Notice that it has a section that displays `Hello` and `Logout` if the user is
  signed in; and a section with `Register` and `Login`.

## HomeController.cs

- In the `Controllers` folder, open `HomeController`.
- Add the directive:

```cs
using Microsoft.AspNetCore.Authorization;
```

- Decorate the `Privacy` method with an `[Authorize]` attribute.

```cs
[Authorize]
public IActionResult Privacy()
{
    return View();
}
```

- Save the file.
- Run the site, click the `Privacy` link.
- You will be required to login, do so.
- Logout.
- Move the `[Authorize]` attribute to the **class**.

```cs
[Authorize]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}
```

- Save the file.
- Run the site, you will be required to login to access any endpoint of this
  `Controller`, do so.
- Logout.
- Decorate the `Index` method with the `[AllowAnonymous]` attribute.

```cs
[AllowAnonymous]
public IActionResult Index()
{
    return View();
}
```

- Save the file.
- Run the site, access the home page.
- Click the `Privacy` link, login.
- Logout.

## Startup.cs

- Edit the `ConfigureServices` method to begin configuring `Identity`.

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(Configuration
            .GetConnectionString("DefaultConnection")));

    services.AddDatabaseDeveloperPageExceptionFilter();

    services.AddDefaultIdentity<IdentityUser>(options =>
        options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();

    services.Configure<IdentityOptions>(options =>
    {
        // Configure Identity...
    });

    services.AddControllersWithViews();
}
```

- Put the cursor on `IdentityOptions` and press `F1`.
- Check the `PasswordOptions`, `SignInOptions` and `UserOptions`.
- Configure a couple of options.

```cs
// ...
services.Configure<IdentityOptions>(options =>
{
    // Configure Identity
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
});
// ...
```

- Save the file.
- Run the site, begin registering a new user with the _password_ `abc`, tab to
  the _Confirm password_ textbox.
- The initial _error message_ about **password length** will be incorrect.
- Provide the _password_ `Abcd5!` and attempt to `Register`.
- This error message is correct.

## Scaffold Identity

- In Solution Explorer, expand `Areas / Identity / Pages`.
- This is where `Identity` customization will happen.
- Right-click the project and select `Add / New Scaffolded Item...`
- In the list of `Installed items`, click `Identity`, select `Identity`, click
  **Add**.
- Select `Account\Login` and `Account\Register`.
- Set the `Data context` class to `ApplicationDbContext (SecureSite.Data)`.
- Click **Add**.
- Close the read me file.
- In Solution Explorer, expand `Areas / Identity / Pages / Account`, select
  `Login.cshtml`.

## Login.cshtml

- Delete the `local account` heading and give the first input _autofocus_.

```html
@page

@model LoginModel

@{ ViewData["Title"] = "Log in"; }

<h1>@ViewData["Title"]</h1>

<div class="row">
    <div class="col-md-4">
        <section>
            <form id="account" method="post">
                <hr />
                <div asp-validation-summary="All" class="text-danger"></div>
                <div class="form-group">
                    <label asp-for="Input.Email"></label>
                    <input asp-for="Input.Email" class="form-control" autofocus />
                    <span asp-validation-for="Input.Email" class="text-danger"></span>
                    <!-- ... -->
<!-- ... -->
```

- Update the `Use another service to log.` in markup.

```html
    <!-- ... -->
    </div>
    @if ((Model.ExternalLogins?.Count ?? 0) > 0)
    {
        <div class="col-md-6 col-md-offset-2">
            <section>
                <h3>Use another service to log in.</h3>
                <hr />
                <form id="external-account" asp-page="./ExternalLogin" asp-route-returnUrl="@Model.ReturnUrl" method="post" class="form-horizontal">
                    <div>
                        <p>
                            @foreach (var provider in Model.ExternalLogins!)
                            {
                                <button type="submit" class="btn btn-primary" name="provider" value="@provider.Name" title="Log in using your @provider.DisplayName account">@provider.DisplayName</button>
                            }
                        </p>
                    </div>
                </form>
            </section>
        </div>
    }
</div>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

- Save the file.

## Register.cshtml

- Give the first input _autofocus_.

```html
@page

@model RegisterModel

@{ ViewData["Title"] = "Register"; }

<h1>@ViewData["Title"]</h1>

<div class="row">
    <div class="col-md-4">
        <form asp-route-returnUrl="@Model.ReturnUrl" method="post">
            <h4>Create a new account.</h4>
            <hr />

            <div asp-validation-summary="All" class="text-danger"></div>

            <div class="form-group">
                <label asp-for="Input.Email"></label>
                <input asp-for="Input.Email" class="form-control" autofocus />
                <span asp-validation-for="Input.Email" class="text-danger"></span>
                <!-- ... -->
```

- Update the `Use another service to register` in markup.

```html
    <!-- ... -->
    </div>
    @if ((Model.ExternalLogins?.Count ?? 0) > 0)
    {
        <div class="col-md-6 col-md-offset-2">
            <section>
                <h3>Use another service to register.</h3>
                <hr />
                <form id="external-account" asp-page="./ExternalLogin" asp-route-returnUrl="@Model.ReturnUrl" method="post" class="form-horizontal">
                    <div>
                        <p>
                            @foreach (var provider in Model.ExternalLogins!)
                            {
                                <button type="submit" class="btn btn-primary" name="provider" value="@provider.Name" title="Log in using your @provider.DisplayName account">@provider.DisplayName</button>
                            }
                        </p>
                    </div>
                </form>
            </section>
        </div>
    }
</div>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

- Save the file.

## Register.cshtml.cs

- Expand `Register.cshtml` and select `Register.cshtml.cs`.
- Find the `InputModel` class in the file.
- Update the `StringLength` attribute of the `Password` property.

```cs
public class InputModel
{
    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 8)]
    [DataType(DataType.Password)]
    [Display(Name = "Password")]
    public string Password { get; set; }

    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
```

- Save the file.
- Run the site, begin registering a new user with the _password_ `abc`.
- The initial _error message_ about _password length_ will be correct now.
