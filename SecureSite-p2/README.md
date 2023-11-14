# Walkthrough 16 - Identity Customization and Role-Based Authorization

## Setup

This will explore further **ASP.NET Core Identity** by customizing the User
**Identity** and using **Role-based authorization**.

- Start SQL Server.
- Open `SecureSite` from the end of the previous walkthrough.

## Create a User

- If the database already exists and you have created a user, skip this section.
- Otherwise, open the **Package Manager Console** (PMC) and issue the following
  command:

```console
Update-Database
```

- Run the site.
- Register a new user.

## ApplicationUser.cs

- Add a new class to the `Models` folder named `ApplicationUser`.
- Have the class _inherit_ from `IdentityUser`.
- Add the directive:

```cs
using Microsoft.AspNetCore.Identity;
```

- Add two properties for user name.

```cs
public class ApplicationUser : IdentityUser
{
    [PersonalData]
    public string FirstName { get; set; }

    [PersonalData]
    public string LastName { get; set; }
}
```

- Save and close the file.
- `IdentityUser` will now need to be replaced throughout the application with
  `ApplicationUser`.

## Login.cshtml.cs

- Open `Areas/Identity/Pages/Account/Login.cshtml.cs`.
- Replace `IdentityUser` with `ApplicationUser` and add the
  `using SecureSite.Models;` namespace.

```cs
namespace SecureSite.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;

        public LoginModel(SignInManager<ApplicationUser> signInManager, ILogger<LoginModel> logger, UserManager<ApplicationUser> userManager)
        {
        // ...
```

- Save the file.

## Register.cshtml.cs

- Open `Areas/Identity/Pages/Account/Register.cshtml.cs`.
- Replace `IdentityUser` with `ApplicationUser` and add the
  `using SecureSite.Models;` namespace.

```cs
namespace SecureSite.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
        // ...

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            // ...
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser {
                    UserName = Input.Email,
                    Email = Input.Email
                };
            // ...
```

- Save the file.

## _LoginPartial.cshtml

- Open `Views/Shared/_LoginPartial.cshtml`.
- Replace `IdentityUser` with `ApplicationUser`.

```html
@using Microsoft.AspNetCore.Identity
@inject SignInManager<ApplicationUser> SignInManager
@inject UserManager<ApplicationUser> UserManager

<ul class="navbar-nav">
<!-- ... -->
```

- Save the file.

## Startup.cs

- Open `Startup.cs`.
- Replace `IdentityUser` with `ApplicationUser` in `ConfigureServices` and add
  the `using SecureSite.Models;` namespace.

```cs
public void ConfigureServices(IServiceCollection services)
{
    // ...
    services.AddDefaultIdentity<ApplicationUser>(options => options
        .SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
    // ...
}
```

- Save the file.

## ApplicationDbContext.cs

- Open `Data/ApplicationDbContext.cs`.
- Update the class to use the `ApplicationUser`, add the
  `using SecureSite.Models;` directive.

```cs
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    // ...
```

- Build the project (`Ctrl+B`) to ensure no errors.

## Updating the Database

- From **PMC**, run the following commands:

```console
Add-Migration FirstAndLastName

Update-Database
```

- In SQL Server Object Explorer, view the `AspNetUsers` table to see the new
  columns.

## Register.cshtml.cs

- Open `Areas/Identity/Pages/Account/Register.cshtml.cs`.
- Add first name and last name to the `InputModel`.

```cs
// ...
public class InputModel
{
    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }

    [Required]
    [EmailAddress]
    [Display(Name = "Email")]
    public string Email { get; set; }

    // ...
}
// ...
```

- Update the `OnPostAsync` method with `FirstName` and `LastName`.

```cs
public async Task<IActionResult> OnPostAsync(string returnUrl = null)
{
    returnUrl ??= Url.Content("~/");

    ExternalLogins = (await _signInManager
        .GetExternalAuthenticationSchemesAsync())
        .ToList();

    if (ModelState.IsValid)
    {
        var user = new ApplicationUser
        {
            FirstName = Input.FirstName,
            LastName = Input.LastName
            UserName = Input.Email,
            Email = Input.Email,
        };

    // ...
```

- Save the file.

## Register.cshtml

- Open `Areas/Identity/Pages/Account/Register.cshtml`.
- Add first name and last name to the view.

```html
<!-- ... -->
<form asp-route-returnUrl="@Model.ReturnUrl" method="post">
    <h4>Create a new account.</h4>
    <hr />

    <div asp-validation-summary="All" class="text-danger"></div>

    <div class="form-group">
        <label asp-for="Input.FirstName"></label>
        <input asp-for="Input.FirstName" class="form-control" />
        <span asp-validation-for="Input.FirstName" class="text-danger"></span>
    </div>

    <div class="form-group">
        <label asp-for="Input.LastName"></label>
        <input asp-for="Input.LastName" class="form-control" />
        <span asp-validation-for="Input.LastName" class="text-danger"></span>
    </div>

    <!-- ... -->

    <button type="submit" class="btn btn-primary">Register</button>
</form>

<!-- ... -->
```

- Save the file.
- Run the site, register a new user.

## _LoginPartial.cshtml

- Open `Views/Shared/_LoginPartial.cshtml`.
- Change the login greeting to show the user's first name instead of their user
  name.

```html
@using Microsoft.AspNetCore.Identity
@inject SignInManager<ApplicationUser> SignInManager
@inject UserManager<ApplicationUser> UserManager

<ul class="navbar-nav">

@if (SignInManager.IsSignedIn(User))
{
    var user = await UserManager.GetUserAsync(User);
    var firstName = user.FirstName;

    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="Identity"
            asp-page="/Account/Manage/Index" title="Manage">
            Hello @firstName</a>
    </li>
```

- Save the file.
- Run the site, login with the new user if necessary.
- Notice the greeting.

## Scaffold Identity

- Right-click the project and select `Add / New Scaffolded Item...`
- In the list of `Installed items`, click `Identity`, select `Identity`, click
  **Add**.
- Select `Account / Manage / Index`.
- Set the `Data context` class to `ApplicationDbContext (SecureSite.Data)`.
- Click **Add**.
- Close the read me file.

## Index.cshtml.cs

- Open `Areas/Identity/Pages/Account/Manage/Index.cshtml`, select
  `Index.cshtml.cs`.
- Add first name and last name to the `InputModel`.

```cs
public class InputModel
{
    [Phone]
    [Display(Name = "Phone number")]
    public string PhoneNumber { get; set; }

    [Required]
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Required]
    [Display(Name = "Last Name")]
    public string LastName { get; set; }
}
```

- Update the `LoadAsync` method to retrieve first name and last name and put
  them in the input model.

```cs
private async Task LoadAsync(ApplicationUser user)
{
    var firstName = user.FirstName;
    var lastName = user.LastName;
    var userName = await _userManager.GetUserNameAsync(user);
    var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

    Input = new InputModel
    {
        FirstName = firstName,
        LastName = lastName
        UserName = userName;
        PhoneNumber = phoneNumber,
    };
}
```

- Update the `OnPostAsync` method to update first name and last name.

```cs
public async Task<IActionResult> OnPostAsync()
{
    var user = await _userManager.GetUserAsync(User);

    if (user == null)
    {
        return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
    }

    if (!ModelState.IsValid)
    {
        await LoadAsync(user);

        return Page();
    }

    var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

    if (Input.PhoneNumber != phoneNumber)
    {
        var setPhoneResult = await _userManager
            .SetPhoneNumberAsync(user, Input.PhoneNumber);

        if (!setPhoneResult.Succeeded)
        {
            StatusMessage = "Unexpected error when trying to set phone number.";

            return RedirectToPage();
        }
    }

    // Update custom properties
    user.FirstName = Input.FirstName;
    user.LastName = Input.LastName;

    await _userManager.UpdateAsync(user);
    await _signInManager.RefreshSignInAsync(user);

    StatusMessage = "Your profile has been updated";

    return RedirectToPage();
}
```

- Save the file.

## Index.cshtml

- Open `Areas/Identity/Pages/Account/Manage/Index.cshtml`.
- Add first name and last name to the view.

```html
<!-- ... -->
<div class="form-group">
    <label asp-for="Username"></label>
    <input asp-for="Username" class="form-control" disabled />
</div>

<div class="form-group">
    <label asp-for="Input.FirstName"></label>
    <input asp-for="Input.FirstName" class="form-control" autofocus />
    <span asp-validation-for="Input.FirstName" class="text-danger"></span>
</div>

<div class="form-group">
    <label asp-for="Input.LastName"></label>
    <input asp-for="Input.LastName" class="form-control" />
    <span asp-validation-for="Input.LastName" class="text-danger"></span>
</div>

<div class="form-group">
    <label asp-for="Input.PhoneNumber"></label>
    <input asp-for="Input.PhoneNumber" class="form-control" />
    <span asp-validation-for="Input.PhoneNumber" class="text-danger"></span>
</div>
<!-- ... -->
```

- Save the file.

## _ViewImports.cshtml

- Build the project, it will fail due to a namespace issue in
  `Areas / Identity / Pages / Account / Manage / _ViewImports.cshtml`, open the
  file and fix the namespace issue.

```html
@using SecureSite.Areas.Identity.Pages.Account.Manage
@using SecureSite.Models
```

- Build the project.
- Run the site, access the profile of a user and note the first name and last
  name properties are available.
- Click the `Hello Firstname` link to access account management.
- Click the `Personal data` button, then the `Download` button to download
  _personal data_.
- Open the **JSON** file to see what _personal data_ is stored for the current
  user.

## Startup.cs

- To enable **Role based authorization**, modify **Identity** in the
  `ConfigureServices` method.

```cs
public void ConfigureServices(IServiceCollection services)
{
    // ...
    services.AddDefaultIdentity<ApplicationUser>(options => options
        .SignIn.RequireConfirmedAccount = true)
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();
    // ...

    services.AddControllersWithViews();
}
```

- Save the file.
- Run the site.

## Roles

- Register 3 users with the following email addresses `admin@mail.com`,
  `manager@mail.com` and `clerk@mail.com`.
- In SQL Server Object Explorer, right-click the `localhost\sqlexpress` database
  and select `New Query...`
- Copy the following SQL and run it to create 3 roles and to assign users to
  them.

```sql
USE [SecureSite-Identity];

INSERT INTO AspNetRoles (Id, [Name]) VALUES(NEWID(), 'Admin');
INSERT INTO AspNetRoles (Id, [Name]) VALUES(NEWID(), 'Manager');
INSERT INTO AspNetRoles (Id, [Name]) VALUES(NEWID(), 'Clerk');

INSERT INTO AspNetUserRoles VALUES(
    (SELECT Id FROM AspNetUsers WHERE UserName = 'admin@mail.com'),
    (SELECT Id FROM AspNetRoles WHERE [Name] = 'Admin')
);

INSERT INTO AspNetUserRoles VALUES(
    (SELECT Id FROM AspNetUsers WHERE UserName = 'admin@mail.com'),
    (SELECT Id FROM AspNetRoles WHERE [Name] = 'Manager')
);

INSERT INTO AspNetUserRoles VALUES(
    (SELECT Id FROM AspNetUsers WHERE UserName = 'manager@mail.com'),
    (SELECT Id FROM AspNetRoles WHERE [Name] = 'Manager')
);

INSERT INTO AspNetUserRoles VALUES(
    (SELECT Id FROM AspNetUsers WHERE UserName = 'clerk@mail.com'),
    (SELECT Id FROM AspNetRoles WHERE [Name] = 'Clerk')
);
```

- Run the following SQL to see the roles.

```sql
SELECT u.Email, r.[Name] [Role]
FROM AspNetUserRoles ur
JOIN AspNetRoles r ON ur.RoleId = r.Id
JOIN AspNetUsers u ON ur.UserId = u.Id
```

- Run the site and access the `Privacy` page as `admin@mail.com`,
  `manager@mail.com` and as `clerk@mail.com`.

## HomeController.cs

- Decorate the `Privacy` method with the `Admin` role.

```cs
[Authorize(Roles = "Admin")]
public IActionResult Privacy()
{
    return View();
}
```

- Save the file.
- Only members of `Admin` will be able to access this page now.
- Run the site and attempt to access as `admin@mail.com`, `manager@mail.com` and
  as `clerk@mail.com`.
- Change the attribute so that members of `Admin` or `Manager` have access.

```cs
[Authorize(Roles = "Admin, Manager")]
public IActionResult Privacy()
{
    return View();
}
```

- Run the site and attempt to access as `admin@mail.com`, `manager@mail.com` and
  as `clerk@mail.com`.
- Change the attribute so that a user has to be a member of both `Admin` and
  `Manager` to have access.

```cs
[Authorize(Roles = "Admin")]
[Authorize(Roles = "Manager")]
public IActionResult Privacy()
{
    return View();
}
```

- Run the site and attempt to access as `admin@mail.com`, `manager@mail.com` and
  as `clerk@mail.com`.

## UsersController.cs

- Add a new empty `MVC Controller` named `UsersController`.
- Require users to be **authenticated** to access the new controller.
- Add the `using Microsoft.AspNetCore.Authorization;` directive.

```cs
[Authorize]
public class UsersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
```

- Add the directives:

```cs
using Microsoft.AspNetCore.Identity;
using SecureSite.Models;
```

- Declare a property for the user manager and set it in the constructor.

```cs
[Authorize]
public class UsersController : Controller
{
    private UserManager<ApplicationUser> userManager;

    public UsersController(UserManager<ApplicationUser> userManager)
    {
        this.userManager = userManager;
    }

    public IActionResult Index()
    {
        return View();
    }
}
```

- Only allow users with the `Admin` role access to the `Index` method.
- Get the users from the user manager and return the view with them.

```cs
[Authorize(Roles = "Admin")]
public IActionResult Index()
{
    var users = userManager.Users;

    return View(users);
}
```

- Attempt to scaffold the view.
- Right-click in the `Index` method and select `Add View...`, select
  `Razor View` and click **Add**.
- Set `View name` to `Index` and `Template` to `List`.
- Click the drop-down list for `Model` class and note that `ApplicationUser`
  isn't there.
- The View **Scaffolder** doesn't support `Identity`.
- Click `Cancel` twice.

## User.cs

- Add a new class to the `Models` folder named `User`.

```cs
public class User
{
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}
```

- Save the file.

## UsersController.cs

- Scaffold the view.
- Right-click in the `Index` method and select `Add View...`, select
  `Razor View` and click **Add**.
- Set `View name` to `Index`, `Template` to `List` and `Model` class to
  `User (SecureSite.Models)`, click **Add**.

## _LoginPartial.cshtml

- Open `Views/Shared/_LoginPartial.cshtml`.
- Add a link to the new `UsersController` in the signed in section of the view.

```html
@using Microsoft.AspNetCore.Identity
@inject SignInManager<ApplicationUser> SignInManager
@inject UserManager<ApplicationUser> UserManager

<ul class="navbar-nav">
    @if (SignInManager.IsSignedIn(User))
    {
        <!-- ... -->
        <li class="nav-item">
            <a class="nav-link text-dark" asp-controller="Users"
                asp-action="Index">Users</a>
        </li>
        <!-- ... -->
```

- Save the file.
- Run the site and login as a user with the `Admin` role.
- Attempt to access the `UsersController`.
- The _error message_ will indicate that an `ApplicationUser` model was
  expected, but that a `User` model was received.

## Index.cshtml

- Open `Views/Users/Index.cshtml`.
- Change the model to `ApplicationUser`.
- Update the `Title` and remove the `Create` and `Edit` links.

```html
@model IEnumerable<SecureSite.Models.ApplicationUser>

@{ ViewData["Title"] = "Users"; }

<h1>@ViewBag.Title</h1>

<table class="table">
    <thead>
        <tr>
            <th>
                @Html.DisplayNameFor(model => model.UserName)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.FirstName)
            </th>
            <th>
                @Html.DisplayNameFor(model => model.LastName)
            </th>
            <th></th>
        </tr>
    </thead>
    <tbody>
        @foreach (var item in Model) {
        <tr>
            <td>
                @Html.DisplayFor(modelItem => item.UserName)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.FirstName)
            </td>
            <td>
                @Html.DisplayFor(modelItem => item.LastName)
            </td>
            <td>
                @Html.ActionLink("Details", "Details", new { /* id=item.PrimaryKey */ }) |
                @Html.ActionLink("Delete", "Delete", new { /* id=item.PrimaryKey */ })
            </td>
        </tr>
        }
    </tbody>
</table>
```

- Save the file and access the `Users`.
- Logout and login as `manager@mail.com` and attempt to access the `Users`.
