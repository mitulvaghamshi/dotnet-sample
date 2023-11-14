# Walkthrough 23 - Blazor WebAssembly with EF Core

## Setup

This will explore **Blazor WebAssembly using EF Core**.

- Start Visual Studio.
- Click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `Blazor WebAssembly App` template, click `Next`.
- Set Project name to `MovieTrackerBlazor`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is not selected, click
  `Next`.
- Set version to `.NET 5.0`, and:
  - unselect `Configure for HTTPS`,
  - select `ASP.NET Core hosted`,
  - unselect `Progressive Web Application`
- Click `Create`.

## Delete following files

- From `MovieTrackerBlazor.Shared/` delete `WeatherForecast.cs`.
- From `MovieTrackerBlazor.Server/Controllers/` delete
  `WeatherForecastController.cs`.
- From `MovieTrackerBlazor.Client/`:
  - Delete `Shared/SurveyPrompt.razor`.
  - Delete `Pages/FetchData.razor`.
  - Delete `Pages/Counter.razor`.

## Genre.cs

- Create a `Genre` class in the `MovieTrackerBlazor.Shared` folder.
- Add the `using System.ComponentModel.DataAnnotations;` directive.

```cs
public class Genre
{
    [Key]
    public int Id { get; set; }

    [StringLength(25)]
    public string GenreDescription { get; set; }
}
```

- Save the file.

## Movie.cs

- Create a `Movie` class in the `MovieTrackerBlazor.Shared` folder.
- Add the `using System.ComponentModel.DataAnnotations;` directive.

```cs
public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required, StringLength(100)]
    public string Title { get; set; }

    [DataType(DataType.Date)]
    public DateTime? DateSeen { get; set; }

    [ForeignKey("Genre")]
    public int? GenreId { get; set; }

    public Genre Genre { get; set; }

    [Range(1, 10)]
    public int? Rating { get; set; }
}
```

- Save the file.

## GenresController.cs

- Right-click the `MovieTrackerBlazor.Server.Controllers` folder and select
  `Add / Controller...`, select the `API templates` and select the
  `API Controllers with actions, using Entity Framework`.
- Click **Add**.
- Set `Model` class to `Genre (MovieTrackerBlazor.Shared)`.
- For `Data context` class, click the `+` button.
- Accept the name
  `MovieTrackerBlazor.Server.Data.MovieTrackerBlazorServerContext`.
- Click **Add**.
- Accept the default `Controller` name, click **Add**.
- Update the routing to remove api.
- Delete these endpoints: `GetGenre` with `id`, `PutGenre`, `PostGenre` and
  `DeleteGenre`, also delete `GenreExists` method.

```cs
[Route("[controller]")]
[ApiController]
public class GenresController : ControllerBase
{
    private readonly MovieTrackerBlazorServerContext _context;

    public GenresController(MovieTrackerBlazorServerContext context)
    {
        _context = context;
    }

    // GET: Genres
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetGenre()
    {
        return await _context.Genre.ToListAsync();
    }
}
```

- Save the file.

## MoviesController.cs

- Right-click the `MovieTrackerBlazor.Server.Controllers` folder and select
  `Add / Controller...`, select the `API templates` and select the
  `API Controllers with actions, using Entity Framework`.
- Click **Add**.
- Set `Model` class to `Movie (MovieTrackerBlazor.Shared)`.
- Set `Data context` class to
  `MovieTrackerBlazor.Server.Data.MovieTrackerBlazorServerContext`.
- Accept the default `Controller` name, click **Add**.
- Update the routing to remove api.

```cs
[Route("[controller]")]
[ApiController]
public class MoviesController : ControllerBase
{
    private readonly MovieTrackerBlazorServerContext _context;
// ...
```

- Update `GetMovie` to include `Genre`.

```cs
[HttpGet]
public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
{
    return await _context.Movie.Include(m => m.Genre).ToListAsync();
}
```

- Save the file.

## appsettings.json

- Update the _connection string_ to use **SQL Server Express** and simplify the
  database name.

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
    "MovieTrackerBlazorServerContext": "Server=localhost\\sqlexpress;Database=movie_tracker_blazor;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

- Save the file.

## Database Migration

- In the **Package Manager Console**, ensure `MovieTrackerBlazor.Server` is the
  default project.
- Issue following commands:

```console
Add-Migration init

Update-Database
```

- In SQL Server Object Explorer, expand `localhost\sqlexpress / Databases`.
- Right-click `movie_tracker_blazor` and select `New Query...`, paste the
  following `INSERT` statements into the query window.

```sql
INSERT INTO Genre VALUES('Action');
INSERT INTO Genre VALUES('Adventure');
INSERT INTO Genre VALUES('Animation');
INSERT INTO Genre VALUES('Biography');
INSERT INTO Genre VALUES('Comedy');
INSERT INTO Genre VALUES('Crime');
INSERT INTO Genre VALUES('Documentary');
INSERT INTO Genre VALUES('Drama');
INSERT INTO Genre VALUES('Family');
INSERT INTO Genre VALUES('Fantasy');
INSERT INTO Genre VALUES('Film Noir');
INSERT INTO Genre VALUES('History');
INSERT INTO Genre VALUES('Horror');
INSERT INTO Genre VALUES('Music');
INSERT INTO Genre VALUES('Musical');
INSERT INTO Genre VALUES('Mystery');
INSERT INTO Genre VALUES('Romance');
INSERT INTO Genre VALUES('Sci-Fi');
INSERT INTO Genre VALUES('Short Film');
INSERT INTO Genre VALUES('Sport');
INSERT INTO Genre VALUES('Superhero');
INSERT INTO Genre VALUES('Thriller');
INSERT INTO Genre VALUES('War');
INSERT INTO Genre VALUES('Western');
```

- Click the `Execute` button in the upper-left corner.
- Close the query window.

## index.html

- Open `wwwroot/index.html`.
- Update the `title` to `Movie Tracker`.

```html
<!DOCTYPE html>
<html>

<head>
    <base href="/" />
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=no" />
    <link href="MovieTrackerBlazor.Client.styles.css" rel="stylesheet" />
    <link href="css/bootstrap/bootstrap.min.css" rel="stylesheet" />
    <link href="css/app.css" rel="stylesheet" />
    <title>Movie Tracker</title>
</head>
<!-- ... -->
```

- Save the file.

## NavMenu.razor

- Open `MovieTrackerBlazor.Client/Shared/NavMenu.razor`.
- Update the application name to `Movie Tracker`.
- Change the `Home` link text to `Movies`, and delete the `Counter` and `Fetch`
  data menu items.

```html
<div class="top-row pl-4 navbar navbar-dark">
    <a class="navbar-brand" href="">Movie Tracker</a>
    <button class="navbar-toggler" @onclick="ToggleNavMenu">
        <span class="navbar-toggler-icon"></span>
    </button>
</div>

<div class="@NavMenuCssClass" @onclick="ToggleNavMenu">
    <ul class="nav flex-column">
        <li class="nav-item px-3">
            <NavLink class="nav-link" href="" Match="NavLinkMatch.All">
                <span class="oi oi-home" aria-hidden="true"></span> Movies
            </NavLink>
        </li>
    </ul>
</div>

@code {
    private bool collapseNavMenu = true;

    private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    private void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }
}
```

- Save the file.

## _Imports.razor

- Open `MovieTrackerBlazor.Client/_Imports.razor`.
- Add the `using MovieTrackerBlazor.Shared;` directive.

```html
@using System.Net.Http
@using System.Net.Http.Json
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Web.Virtualization
@using Microsoft.AspNetCore.Components.WebAssembly.Http
@using Microsoft.JSInterop
@using MovieTrackerBlazor.Client
@using MovieTrackerBlazor.Client.Shared
@using MovieTrackerBlazor.Shared
```

Save the file.

## Index.razor

- Open `MovieTrackerBlazor.Client / Pages / Index.razor`.
- Update the `heading` and delete the `SurveyPrompt`.

```html
@page "/"

<h1>Movie Tracker</h1>

Welcome to your new app.
```

- Inject the `HttpClient`.

```html
@page "/"

@inject HttpClient Http

<h1>Movie Tracker</h1>
```

- Add a code block.
- Declare an array of `movies`.
- Override `OnInitializedAsync` to retrieve the movies from the Web API.

```html
@page "/"

@inject HttpClient Http

<h1>Movie Tracker</h1>

@code {
    private Movie[] movies;

    protected override async Task OnInitializedAsync()
    {
        movies = await Http.GetFromJsonAsync<Movie[]>("movies");
    }
}
```

- Add some markup to display one of two loading messages.

```html
@page "/"

@inject HttpClient Http

<h1>Movie Tracker</h1>

@if (movies == null)
{
    <p><em>Loading...</em></p>
}
else
{
    if (movies.Length == 0)
    {
        <p><em>No movies found.</em></p>
    }
    else
    {
        <p><em>@movies.Length movie(s) found.</em></p>
    }
}

@code {
    private Movie[] movies;

    protected override async Task OnInitializedAsync()
    {
        movies = await Http.GetFromJsonAsync<Movie[]>("movies");
    }
}
```

- Run the site.

## MovieForm.razor

- Add a Razor component to the `Pages` folder named `MovieForm.razor`.
- Delete the heading.

```html
@code {

}
```

- Declare 3 parameters for the component.

```html
@code {
    [Parameter]
    public Movie movie { get; set; }

    [Parameter]
    public string ButtonText { get; set; }

    [Parameter]
    public EventCallback OnValidSubmit { get; set; }
}
```

- Start the `EditModel` specifying the `Model` and `OnValidSubmit` properties.

```html
<EditForm Model="movie" OnValidSubmit="OnValidSubmit">

</EditForm>

@code {
<!-- ... -->
```

- Add a label and text input for `Title`.

```html
<!-- ... -->
<EditForm Model="movie" OnValidSubmit="OnValidSubmit">
    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Title</label>
        <InputText id="Title" @bind-Value="movie.Title" />
    </div>
</EditForm>
<!-- ... -->
```

- Add a label and date control for `DateSeen`.

```html
<!-- ... -->
<EditForm Model="movie" OnValidSubmit="OnValidSubmit">
    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Title</label>
        <InputText id="Title" @bind-Value="movie.Title" />
    </div>
    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Date Seen</label>
        <InputDate id="DateSeen" @bind-Value="movie.DateSeen" />
    </div>
</EditForm>
<!-- ... -->
```

- Inject the `HttpClient`.

```html
@inject HttpClient Http

<EditForm Model="movie" OnValidSubmit="OnValidSubmit">
<!-- ... -->
```

- Declare a list of genres.
- Override `OnInitializedAsync` to retrieve the genres from the Web API.

```html
@code {
    [Parameter]
    public Movie movie { get; set; }

    [Parameter]
    public string ButtonText { get; set; }

    [Parameter]
    public EventCallback OnValidSubmit { get; set; }

    private List<Genre> genres = new();

    protected override async Task OnInitializedAsync()
    {
        genres = await Http.GetFromJsonAsync<List<Genre>>("genres");
    }
}
```

- Add a label and select control for `Genre`.
- Loop through the genres collection to create the drop-down list options.

```html
<!-- ... -->
<EditForm Model="movie" OnValidSubmit="OnValidSubmit">
    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Title</label>
        <InputText id="Title" @bind-Value="movie.Title" />
    </div>

    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Date Seen</label>
        <InputDate id="DateSeen" @bind-Value="movie.DateSeen" />
    </div>

    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Genre</label>
        <InputSelect id="GenreId" @bind-Value="movie.GenreId">
            @foreach (var genre in genres)
            {
                <option value="@genre.Id">@genre.GenreDescription</option>
            }
        </InputSelect>
    </div>
</EditForm>
<!-- ... -->
```

- Add a label and number input for `Rating`.

```html
<!-- ... -->
<EditForm Model="movie" OnValidSubmit="OnValidSubmit">
    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Title</label>
        <InputText id="Title" @bind-Value="movie.Title" />
    </div>

    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Date Seen</label>
        <InputDate id="DateSeen" @bind-Value="movie.DateSeen" />
    </div>

    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Genre</label>
        <InputSelect id="GenreId" @bind-Value="movie.GenreId">
            @foreach (var genre in genres)
            {
                <option value="@genre.Id">@genre.GenreDescription</option>
            }
        </InputSelect>
    </div>

    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Rating</label>
        <InputNumber id="Rating" @bind-Value="movie.Rating" />
    </div>
</EditForm>
<!-- ... -->
```

- Add validator, validation summary and a submit button.

```html
<!-- ... -->
<EditForm Model="movie" OnValidSubmit="OnValidSubmit">
    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Title</label>
        <InputText id="Title" @bind-Value="movie.Title" />
    </div>

    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Date Seen</label>
        <InputDate id="DateSeen" @bind-Value="movie.DateSeen" />
    </div>

    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Genre</label>
        <InputSelect id="GenreId" @bind-Value="movie.GenreId">
            @foreach (var genre in genres)
            {
                <option value="@genre.Id">@genre.GenreDescription</option>
            }
        </InputSelect>
    </div>

    <div class="form-group row">
        <label class="col-sm-2 col-form-label">Rating</label>
        <InputNumber id="Rating" @bind-Value="movie.Rating" />
    </div>

    <DataAnnotationsValidator />
    <ValidationSummary />

    <button type="submit" class="btn btn-primary">@ButtonText</button>
</EditForm>
<!-- ... -->
```

- Save the file.

## AddMovie.razor

- Add a Razor component to the `Pages` folder named `AddMovie.razor`.
- Add a `@page` directive to specify the routing, inject the `HttpClient` and
  the `NavigationManager`.
- Add a space between `Add` and `Movie` in the heading.

```html
@page "/addmovie"

@inject HttpClient Http

@inject NavigationManager NavManager

<h3>Add Movie</h3>

@code {

}
```

- Instantiate a movie.
- Add a method that will call the Web API to add a movie.
- Navigate to the home page.

```html
@code {
    Movie movie = new();

    async Task AddMovieAsync()
    {
        await Http.PostAsJsonAsync("movies", movie);
        NavManager.NavigateTo("/");
    }
}
```

- Incorporate the `MovieForm` component and set its properties.

```html
@page "/addmovie"

@inject HttpClient Http

@inject NavigationManager NavManager

<h3>Add Movie</h3>

<MovieForm ButtonText="Add" movie="movie" OnValidSubmit="AddMovieAsync" />

@code {
    Movie movie = new();

    async Task AddMovieAsync()
    {
        await Http.PostAsJsonAsync("movies", movie);
        NavManager.NavigateTo("/");
    }
}
```

- Save the file.

## Index.razor

- Add a button that links to the `AddMovie` component and update the no movies
  found text.

```html
<!-- ... -->
<h1>Movie Tracker</h1>

@if (movies == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <a class="btn btn-primary" href="/addmovie" title="Add">
        <span class="oi oi-plus" />
    </a>

    <p></p>

    if (movies.Length == 0)
    {
        <p><em>No movies found. Click the add button to add a movie.</em></p>
    }
    else
    {
        <p><em>@movies.Length movie(s) found.</em></p>
    }
}
<!-- ... -->
```

- Save the file.
- Run the site.
- Add a movie.
- Remove the placeholder message and add a table to render the movies.

```html
<!-- ... -->
<h1>Movie Tracker</h1>

@if (movies == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <a class="btn btn-primary" href="/addmovie" title="Add">
        <span class="oi oi-plus" />
    </a>

    <p></p>

    if (movies.Length == 0)
    {
        <p><em>No movies found. Click the add button to add a movie.</em></p>
    }
    else
    {
        <table class="table table-striped">
            <thead>
                <tr>
                    <th>Title</th>
                    <th>Date Seen</th>
                    <th>Genre</th>
                    <th class="text-right">Rating</th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                @foreach (var movie in movies)
                {
                    <tr>
                        <td>@movie.Title</td>
                        <td>@(string.Format("{0:yyyy-MM-dd}", movie.DateSeen))</td>
                        <td>@movie.Genre.GenreDescription</td>
                        <td class="text-right">@movie.Rating</td>
                        <td></td>
                    </tr>
                }
            </tbody>
        </table>
    }
}
<!-- ... -->
```

## EditMovie.razor

- Add a Razor component to the `Pages` folder named `EditMovie.razor`.
- Add a `@page` directive to specify the routing, inject the `HttpClient` and
  the `NavigationManager`.
- Add a space between `Add` and `Movie` in the heading.

```html
@page "/editmovie/{id:int}"

@inject HttpClient Http

@inject NavigationManager NavManager

<h3>Edit Movie</h3>

@code {

}
```

- Add a parameter for the movie id and a movie property.

```html
@code {
    [Parameter]
    public int id { get; set; }

    Movie movie = new();
}
```

- Implement `OnParametersSetAsync` to retrieve the specified movie.

```html
@code {
    [Parameter]
    public int id { get; set; }

    Movie movie = new();

    protected async override Task OnParametersSetAsync()
    {
        movie = await Http.GetFromJsonAsync<Movie>($"movies/{id}");
    }
}
```

- Provide a method that will be called to update the movie.

```html
@code {
    [Parameter]
    public int id { get; set; }

    Movie movie = new();

    protected async override Task OnParametersSetAsync()
    {
        movie = await Http.GetFromJsonAsync<Movie>($"movies/{id}");
    }

    async Task EditMovieAsync()
    {
        await Http.PutAsJsonAsync($"movies/{movie.Id}", movie);
        NavManager.NavigateTo("/");
    }
}
```

- Incorporate the `MovieForm` component and set its properties.

```html
@page "/editmovie/{id:int}"

@inject HttpClient Http

@inject NavigationManager NavManager

<h3>Edit Movie</h3>

<MovieForm ButtonText="Update" movie="movie" OnValidSubmit="EditMovieAsync" />

@code {
    [Parameter]
    public int id { get; set; }

    Movie movie = new();

    protected async override Task OnParametersSetAsync()
    {
        movie = await Http.GetFromJsonAsync<Movie>($"movies/{id}");
    }

    async Task EditMovieAsync()
    {
        await Http.PutAsJsonAsync($"movies/{movie.Id}", movie);

        NavManager.NavigateTo("/");
    }
}
```

- Save the file.

## Index.razor

- Add a link in the table row that looks like a button to enable editing a
  movie.

```html
<!-- ... -->
<tbody>
    @foreach (var movie in movies)
    {
        <tr>
            <td>@movie.Title</td>
            <td>@(string.Format("{0:yyyy-MM-dd}", movie.DateSeen))</td>
            <td>@movie.Genre.GenreDescription</td>
            <td class="text-right">@movie.Rating</td>
            <td>
                <a class="btn btn-primary" href="/editmovie/@movie.Id" title="Edit"><span class="oi oi-pencil" /></a>
            </td>
        </tr>
    }
</tbody>
<!-- ... -->
```

- Save the file.
- Refresh the browser.
- Edit the movie.
- Inject the `JavaScript` runtime.

```html
@page "/"

@inject HttpClient Http

@inject IJSRuntime JS

<h1>Movie Tracker</h1>
<!-- ... -->
```

- Add a method to delete a movie, which will re-initialize the component.

```html
@code {
    private Movie[] movies;

    protected override async Task OnInitializedAsync()
    {
        movies = await Http.GetFromJsonAsync<Movie[]>("movies");
    }

    async Task DeleteAsync(int id)
    {
        var movie = movies.First(m => m.Id == id);

        if (await JS.InvokeAsync<bool>("confirm", $"Are you sure you want to delete {movie.Title}?"))
        {
            await Http.DeleteAsync($"movies/{id}");
            await OnInitializedAsync();
        }
    }
}
```

- Add a button in the table row to call the delete method.

```html
<!-- ... -->
<tbody>
    @foreach (var movie in movies)
    {
        <tr>
            <td>@movie.Title</td>
            <td>@(string.Format("{0:yyyy-MM-dd}", movie.DateSeen))</td>
            <td>@movie.Genre.GenreDescription</td>
            <td class="text-right">@movie.Rating</td>
            <td>
                <a class="btn btn-primary" href="/editmovie/@movie.Id"
                    title="Edit"><span class="oi oi-pencil" />
                </a>
                <button class="btn btn-danger" title="Delete"
                    @onclick="DeleteAsync(movie.Id)">
                    <span class="oi oi-trash" /></button>
            </td>
        </tr>
    }
</tbody>
<!-- ... -->
```

- Notice that there is an error with the call to `DeleteAsync`.
- The call must provide a closure.

```html
@onclick="(() => DeleteAsync(movie.Id))"
```

- Save the file.
- Refresh the browser.
- Delete the movie.
