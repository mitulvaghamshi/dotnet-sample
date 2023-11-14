# Walkthrough 5 - Entity Framework Introduction

## Setup

This walkthrough will add database persistence to the MVC `MovieTracker` application.

- Start SQL Server.
- Open `MovieTracker` from the end of the previous walkthrough.

## MoviesController.cs

- Copy the three movies inside the static list.
- Open Notepad and paste them in.
- They will be needed later.

```cs
private static List<Movie> movies = new List<Movie>
{
    new Movie
    {
        Id = 1,
        Title = "Birds of Prey",
        DateSeen = DateTime.Now.AddDays(-50),
        Genre = "Action",
        Rating = 6
    },
    new Movie
    {
        Id = 2,
        Title = "Palm Springs",
        DateSeen = DateTime.Now.AddDays(-25),
        Rating = 7
    },
    new Movie
    {
        Id = 3,
        Title = "Hamilton",
        Genre = "Drama"
    }
};
```

## EF Scaffolding

- Delete `MoviesController.cs` and the `Views / Movies` folder.
- Right-click the project in **Solution Explorer**.
- Select `Manage NuGet Packages...`.
- notice that there is only one package installed.
- Right-click the `Controllers` folder and select `Add / Controller...`.
- Select the `MVC Controller with views, using Entity Framework`, and click `Add`.
- Set `Model` class to `Movie (MovieTracker.Models)`.
- Click the `plus` button of `DataContext` class.
- Accept the default name `MovieTracker.Data.MovieTrackerContext`, and click `Add`.
- Accept default `Controller` name `MoviesController`, and click `Add`.
- Intermittently, the scaffolder will fail and suggest installing `Microsoft.VisualStudio.Web.CodeGeneration.Design`.
  - This package should already be installed.
  - If this error occurs, select Rebuild Solution from the **Build** menu.
  - If that doesn't succeed, close the solution and re-open it.
  - If that still doesn't succeed, close Visual Studio and re-open.
  - If that still doesn't solve it, uninstall and reinstall the package.
- The following things will happen.
  - `Microsoft.EntityFrameworkCore.SqlServer` package will be installed.
  - `Microsoft.EntityFrameworkCore.Tools` package will be installed.
  - A folder named `Data` will be created with the file `MovieTrackerContext.cs`.
  - `MoviesController.cs` will be created.
  - `Views/Movies` folder will be created with the following views.
    - `Index.cshtml`
    - `Details.cshtml`
    - `Create.cshtml`
    - `Edit.cshtml`
    - `Delete.cshtml`
  - `appsettings.json` will be updated with a connection string for the database.
  - The `ConfigureServices` method of `Startup.cs` will updated to register the database.

## Packages

- Right-click the `MovieTracker` project and select `Manage NuGet Packages...`.
- Notice the two new packages that have been installed.

## Startup.cs

- Open `Startup.cs` and note the `services.AddDbContext` line in the `ConfigureServices` method.
- This is the dependency injection; also known as registering the database context.

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    services.AddDbContext<MovieTrackerContext>(options =>
        options.UseSqlServer(Configuration
            .GetConnectionString("MovieTrackerContext")));
}
```

## appsettings.json

- Update the connection string to use **SQL Server Express** and rename the database to `movie_tracker`.

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
    "MovieTrackerContext": "Server=.\\sqlexpress;Database=movie_tracker;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

- Save the file.
- Run the site. Click the `Movies` link.
- An error will occur because the database doesn't exist yet.

## MovieTrackerContext.cs

- Notice the `DbSet Movie` property.
- Update the constructor to ensure the database is created.

```cs
public MovieTrackerContext (DbContextOptions<MovieTrackerContext> options)
    : base(options)
{
    Database.EnsureCreated();
}
```

- Save the file.
- Run the site.
- Click the `Movies` link. Add a movie.
- Close the browser.
- Implement the `OnModelCreating` method to seed the database on first creation.
- Paste the three movies you copied earlier from Notepad.

```cs
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Movie>().HasData(
        new Movie
        {
            Id = 1,
            Title = "Birds of Prey",
            DateSeen = DateTime.Now.AddDays(-50),
            Genre = "Action",
            Rating = 6
        },
        new Movie
        {
            Id = 2,
            Title = "Palm Springs",
            DateSeen = DateTime.Now.AddDays(-25),
            Rating = 7
        },
        new Movie
        {
            Id = 3,
            Title = "Hamilton",
            Genre = "Drama"
        }
    );
}
```

- Run the site.
- Click the `Movies` link.
- The seeded movies will not be here; because the database already existed.
- From the `View` menu, select `SQL Server Object Explorer`.
- Expand the `SQL Server` node, `localhost\sqlexpress` may already be present.
- If it isn't, click the `Add SQL Server` button.
- Set `Server Name` to `localhost\sqlexpress`, click `Connect`.
- Expand `localhost\sqlexpress/Databases` and right-click `movie_tracker` and select `Delete`.
- On the delete confirmation dialog, select `Close` existing connections.
- Run the site. Click the `Movies` link.
- The database will be created again and seeded with these records this time.
- Close the browser.

## Create.cshtml

- Add an _autofocus_ attribute to the input tag for `Title`.

```html
<!-- ... -->
<div class="form-group">
    <label> asp-for="Title" class="control-label"></label>
    <input asp-for="Title" class="form-control" autofocus />
    <span> asp-validation-for="Title" class="text-danger"></span>
</div>
<!-- ... -->
```

- Save the file.

## Edit.cshtml

- Notice that `Id` is a hidden field.
- Add an _autofocus_ attribute to the input tag for `Title`.

```html
<!-- ... -->
<input type="hidden" asp-for="Id" />
<div class="form-group">
    <label> asp-for="Title" class="control-label"></label>
    <input asp-for="Title" class="form-control" autofocus />
    <span> asp-validation-for="Title" class="text-danger"></span>
</div>
<!-- ... -->
```

- Save the file.

## MoviesController.cs

- Add a breakpoint **F9** to every `public` method.
- Press **F5** to run in debug mode.
- Click the `Movies` link.

### Constructor

- This is where the database context dependency is requested.
- Hover over context, expand `context / Movie / Results View / [0]` to see the first movie record.
- Press **F5** to continue.

### Index

- Note that the `Index` method just returns the list of movies.
- Press F5 to continue.
- Click the `Details` link of the first movie.

### Details

- Note that the constructor is called again; as it will be each time.
- Remove the constructor breakpoint **F9**.
- Press **F5** to continue.
- Hover over `id`, note that it is `1`.
- Press **F10** to step through the method inspecting it, until reaching a return.
- Press **F5** to continue.
- Change the URL to `http://localhost:<port>/Movies/Details/10`.
- Step through the code and continue when reaching a return.
- Change the URL to `http://localhost:<port>/Movies/Details/1a`.
- Step through the code and continue when reaching a return.
- Change the URL to `http://localhost:<port>/Movies/Details/1`.
- Step through the code and continue when reaching a return.
- Click the `Edit` link.

### Edit

- Step through the code and continue when reaching a return.
- Change the movie title, click **Save**.
- Step through the code to the `ModelState` check.
- Hover over `ModelState`, expand `ModelState / Values / Results View / [2]` to see new title.
- Step through the code and continue when reaching a return.
- Remove the Index breakpoint, continue.
- Click the `Create New` link.

### Create

- Step through the code and continue when reaching a return.
- Enter only a new movie title, click `Create`.
- Notice that the `ModelState` is valid even though the movie only has a title.
- Step through the code and continue when reaching a return.
- Click the `Delete` link of the new movie.

### Delete

- Step through the code and continue when reaching a return.
- Click `Delete`
- Step through the code and continue when reaching a return.
- Close the browser.
- End the application by pressing Shift+F5 if necessary.
- From the `Debug` menu, select `Windows / Breakpoints`.
- Remove all breakpoints.

## SQLite

- Open the `Tools` menu, select `NuGet Package Manager / Package Manager Console`.
- Issue the command:

```shell
Install-Package Microsoft.EntityFrameWorkCore.Sqlite
```

## Startup.cs

- In the `ConfigureServices` method;
- Comment out the existing **SQL Server** database dependency injection;
- And add an **SQLite** injection with a different connection string.

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    // services.AddDbContext<MovieTrackerContext>(options => options
    //     .UseSqlServer(Configuration
    //         .GetConnectionString("MovieTrackerContext")));

    services.AddDbContext<MovieTrackerContext>(options => options
        .UseSqlite(Configuration
            .GetConnectionString("MovieTrackerContextLite")));
}
```

- Save the file.

## appsettings.json

- Add a connection string for **SQLite**.

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
    "MovieTrackerContextLite": "filename=movie_tracker.db",
    "MovieTrackerContext": "Server=.\\sqlexpress;Database=movie_tracker;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

- Save the file.
- Run the site and add a movie.
- Notice in Solution Explorer, there is now a `movie_tracker.db` file.
- If you select it, you will notice that it is a binary file.
- Scroll through it to see recognizable data, but you shouldn't edit it directly.

## SQLite/SQL Server Compact Toolbox

- From the `Extensions` menu, select `Manage Extensions`.
- On the left, select `Online`.
- Search for `sqlite`.
- Select `SQLite/SQL Server Compact Toolbox`, click the `Download` button.
- Once the download completes, exit Visual Studio and restart it.
- Open the project.
- From the `Tools` menu, select `SQLite/SQL Server Compact Toolbox`.
- Click the `Add SQLite and SQL Compact connections from current Solution` button.
- Expand `MovieTracker.db / Tables`.
- Right-click `Movies` and select `Edit Top 200 Rows` to see the data.

## Startup.cs

- Change the project back to **SQL Server** by commenting out the **SQLite** injection in favour of **SQL Server** in `ConfigureServices`.

```cs
public void ConfigureServices(IServiceCollection services)
{
    services.AddControllersWithViews();

    services.AddDbContext<MovieTrackerContext>(options => options
        .UseSqlServer(Configuration
            .GetConnectionString("MovieTrackerContext")));

    // services.AddDbContext<MovieTrackerContext>(options => options
    //     .UseSqlite(Configuration
    //         .GetConnectionString("MovieTrackerContextLite")));
}
```

- Save the file.
