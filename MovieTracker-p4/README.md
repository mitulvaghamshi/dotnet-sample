# Walkthrough 7 - Testing

## Setup

This walkthrough will add testing to the MVC `MovieTracker` application.

- Open `MovieTracker` from the end of the previous walkthrough.
- Right-click the solution in the Solution Explorer, select
  `Add / New Project...`
- Set language to `C#` and project type to `Test`.
- Select the `xUnit Test Project` template, click `Next`.
- Set Project name to `MovieTrackerTest`, leave Location as is, click `Next`.
- Set version to **.NET 5.0**, click `Create`.

## MovieTrackerTest

- In solution explorer, under the new project, expand `Dependencies`.
- Right-click the new project and select `Add / Project Reference...`.
- Select `MovieTracker`, click `OK`.
- Note that the `MovieTracker` project gets added as a dependency.
- In the `PMC`, change the `Default project` drop-down list (near the top of the
  PMC) to `MovieTrackerTest`.
- Issue the command:

```console
Install-Package Microsoft.EntityFrameworkCore.InMemory
```

## UnitTest1.cs

- Add the directives as:

```cs
using Microsoft.EntityFrameworkCore;
using MovieTracker.Models;
using MovieTracker.Data;
```

- Add a method that will initialize and return an **in-memory** database.

```cs
private MovieTrackerContext CreateContext(string databaseName)
{
    var options = new DbContextOptionsBuilder<MovieTrackerContext>()
        .UseInMemoryDatabase(databaseName)
        .Options;

    var context = new MovieTrackerContext(options);

    context.Movie.AddRange(
        new Movie
        { 
            Id = 1,
            Title = "Car Chases and Explosions",
            DateSeen = new DateTime(2021, 7, 1).Date,
            Genre = "Action",
            Rating = 6
        },
        new Movie
        {
            Id = 2,
            Title = "Silly Misunderstandings",
            DateSeen = new DateTime(2021, 8, 15).Date,
            Genre = "Comedy",
            Rating = 7
        },
        new Movie
        {
            Id = 3,
            Title = "Serious Discussions",
            DateSeen = new DateTime(2021, 9, 30).Date,
            Genre = "Drama",
            Rating = 8
        }
    );

    context.SaveChanges();

    return context;
}
```

- Rename `Test1` to `Index_NoInput_ReturnsMovies`.

```cs
[Fact]
public void Index_NoInput_ReturnsMovies()
{
}
```

- Add 3 comments to outline the structure of the method.

```cs
[Fact]
public void Index_NoInput_ReturnsMovies()
{
    // Arrange

    // Act

    // Assert
}
```

- Create a `context`, instantiate a new instance of`MoviesController`, add the
  `using MovieTracker.Controllers;` directive.
- Note that dependency injection is allowing us to pass the in memory database
  to the controller.

```cs
[Fact]
public void Index_NoInput_ReturnsMovies()
{
    // Arrange
    var context = CreateContext("Index");
    var moviesController = new MoviesController(context);

    // Act

    // Assert
}
```

- Add a breakpoint to the line of code where the context is instantiated (F9).
- From the `Test` menu, select `Test Explorer`.
- In the Text Explorer, expand the test until you reach
  `Index_NoInput_ReturnsMovies`.
- Right-click it and select `Debug`.
- The code will stop at the breakpoint, press F11 to step into `CreateContext`.
- Press F11 to keep stepping until the `context.Movie.AddRange` statement is
  about to execute.
- Hover over context and expand it, then `Movie / Results View / [0]`.
- Note that it is the first `Movie` created in `OnModelCreating` method of the
  context class.
- Press F11 until the return statement.
- The app will crash before the return because of a duplicate primary key.
- Press Shift+F5 to stop the application, if necessary.

## MovieTrackerContext.cs

- Comment out the call to the `EnsureCreated` method in the constructor.

```cs
public MovieTrackerContext (DbContextOptions<MovieTrackerContext> options) : base(options)
{
    // Database.EnsureCreated();
}
```

- Save the file.

## UnitTest1.cs

- Debug again, this time the test should pass.
- Remove the breakpoint.
- Call the `Index` method.

```cs
[Fact]
public void Index_NoInput_ReturnsMovies()
{
    // Arrange
    var context = CreateContext("Index");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = moviesController.Index();

    // Assert
}
```

- Right-click the `Index` method and select `Go To Definition` (F12).
- Note that the `Index` method is asynchronous and returns a
  `Task<IActionResult>`.
- Update the call to the `Index` method to make it an asynchronous call.

```cs
[Fact]
public void Index_NoInput_ReturnsMovies()
{
    // Arrange
    var context = CreateContext("Index");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Index();

    // Assert
}
```

- This requires updating this method to be asynchronous, add the
  `using System.Threading.Tasks;` directive.

```cs
[Fact]
public async Task Index_NoInput_ReturnsMovies()
{
    // Arrange
    var context = CreateContext("Index");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Index();

    // Assert
}
```

- Add a breakpoint to the act line of code (F9).
- Debug the test.
- The code will stop at the breakpoint, press F10 to step over the call to the
  `Index` method.
- Hover over `actionResult` to see that the data type is actually a
  `ViewResult`.
- The `Autos` and `Locals` windows should be at the bottom of the screen, if
  they are not, either can be accessed from the `Debug / Windows` menu.
- In `Autos` or `Locals`, expand `actionResult` and note that the `Model`
  property is a `List` of `Movie` objects.
- Press F5 to allow the test to finish.
- Remove the breakpoint.
- Add an assertion to check the return type, add the
  `using Microsoft.AspNetCore.Mvc;` directive.

```cs
[Fact]
public async Task Index_NoInput_ReturnsMovies()
{
    // Arrange
    var context = CreateContext("Index");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Index();

    // Assert
    Assert.IsType<ViewResult>(actionResult);
}
```

- From the `Test Explorer`, click `Run All` to run all tests.
- It should pass.
- Convert the more general `actionResult` to its specific `ViewResult` type and
  check that its model is a list of movies.
- Add the `using System.Collections.Generic;` directive.

```cs
[Fact]
public async Task Index_NoInput_ReturnsMovies()
{
    // Arrange
    var context = CreateContext("Index");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Index();
    // Assert
    Assert.IsType<ViewResult>(actionResult);

    var viewResult = actionResult as ViewResult;
    Assert.IsType<List<Movie>>(viewResult.Model);
}
```

- Convert the view result `model` to a list of `movies`, check the movie
  `count`, and incorrectly the `Id` of the 1st movie.

```cs
[Fact]
public async Task Index_NoInput_ReturnsMovies()
{
    // Arrange
    var context = CreateContext("Index");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Index();

    // Assert
    Assert.IsType<ViewResult>(actionResult);

    var viewResult = actionResult as ViewResult;
    Assert.IsType<List<Movie>>(viewResult.Model);

    var movies = viewResult.Model as List<Movie>;
    
    // Check the number of movies and first movie id
    Assert.Equal(3, movies.Count);
    Assert.Equal(101, movies[0].Id); // This should fail...
}
```

- Run the test, note the test results appear in the bottom area of the Test
  Explorer.
- Fix the assertion.

```cs
[Fact]
public async Task Index_NoInput_ReturnsMovies()
{
    // Arrange
    var context = CreateContext("Index");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Index();
    // Assert
    Assert.IsType<ViewResult>(actionResult);

    var viewResult = actionResult as ViewResult;
    Assert.IsType<List<Movie>>(viewResult.Model);

    var movies = viewResult.Model as List<Movie>;
    // Check the number of movies and first movie Id
    Assert.Equal(3, movies.Count);
    Assert.Equal(1, movies[0].Id); // This will pass
}
```

- Run the test again, it should pass.
- Test each attribute of the model, use different models.

```cs
[Fact]
public async Task Index_NoInput_ReturnsMovies()
{
    // Arrange
    var context = CreateContext("Index");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Index();
    // Assert
    Assert.IsType<ViewResult>(actionResult);

    var viewResult = actionResult as ViewResult;
    Assert.IsType<List<Movie>>(viewResult.Model);

    var movies = viewResult.Model as List<Movie>;
    // Check the number of movies and a portion of every record and all fields
    Assert.Equal(3, movies.Count);
    Assert.Equal(1, movies[0].Id);
    Assert.Equal("Silly Misunderstandings", movies[1].Title);
    Assert.Equal(new DateTime(2021, 9, 30).Date, movies[2].DateSeen);
    Assert.Equal("Action", movies[0].Genre);
    Assert.Equal(7, movies[1].Rating);
}
```

- Run the test again, it should pass.
- Copy the `Index_NoInput_ReturnsMovies` method and rename it
  `Details_MovieId_ReturnsMovie`.

```cs
[Fact]
public async Task Details_MovieId_ReturnsMovie()
{
    // ...
}
```

- Update the database named passed to `CreateContext`; this is being done so
  that each test gets its own new copy of the **in-memory** database.
- Also, update the act code to call the `Details` method.

```cs
[Fact]
public async Task Details_MovieId_ReturnsMovie()
{
    // Arrange
    var context = CreateContext("Details");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Details(1);

    // Assert
    // ...
}
```

- Update the assertion code to handle a single movie and check all of its
  attributes.

```cs
[Fact]
public async Task Details_MovieId_ReturnsMovie()
{
    // Arrange
    var context = CreateContext("Details");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Details(1);

    // Assert
    Assert.IsType<ViewResult>(actionResult);

    var viewResult = actionResult as ViewResult;
    Assert.IsType<List<Movie>>(viewResult.Model);

    var movie = viewResult.Model as Movie;

    // Test all properties of a movie.
    Assert.Equal(1, movie.Id);
    Assert.Equal("Car Chases and Explosions", movie.Title);
    Assert.Equal(new DateTime(2021, 7, 1).Date, movie.DateSeen);
    Assert.Equal("Action", movie.Genre);
    Assert.Equal(6, movie.Rating);
}
```

- In the Test Explorer, run each test individually by right-clicking them and
  selecting Run, they pass.
- Run `UnitTest1` which will run both tests, they pass.
- Copy the `Details_MovieId_ReturnsMovie` method and rename it
  `Create_Movie_RedirectsToIndex`.

```cs
[Fact]
public async Task Create_Movie_RedirectsToIndex()
{
    // ...
}
```

- Update the database name and the act code to call the `Create` method and
  delete most of the assert code.

```cs
[Fact]
public async Task Create_Movie_RedirectsToIndex()
{
    // Arrange
    var context = CreateContext("Create");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Create(new Movie
    {
        Title = "Testing for Fun and Profit",
        DateSeen = DateTime.Now.Date,
        Genre = "Drama",
        Rating = 9
    });

    // Assert
    Assert.IsType<ViewResult>(actionResult);
}
```

- Add a breakpoint to the act line of code (F9).
- In the `Test Explorer`, right-click the new test and debug it.
- Step into the code with F11.
- Notice that the new movie gets added to the database.
- Continue stepping to the assert, it will error.
- The return type isn't a `ViewResult`, but a `RedirectToActionResult`.
- Press F5 to allow the code to complete.
- Remove the breakpoint.
- Update the assert and test the `ActionName` property of the
  `RedirectToActionResult`.

```cs
[Fact]
public async Task Create_Movie_RedirectsToIndex()
{
    // Arrange
    var context = CreateContext("Create");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Create(new Movie
    {
        Title = "Testing for Fun and Profit",
        DateSeen = DateTime.Now.Date,
        Genre = "Drama",
        Rating = 9
    });

    // Assert
    Assert.IsType<RedirectToActionResult>(actionResult);

    var redirectToActionResult = actionResult as RedirectToActionResult;
    Assert.Equal("Index", redirectToActionResult.ActionName);
}
```

- Run the new test, it should pass.
- Call the `Index` method to get a count of movies, to verify the create
  succeeded.

```cs
[Fact]
public async Task Create_Movie_RedirectsToIndex()
{
    // Arrange
    var context = CreateContext("Create");
    var moviesController = new MoviesController(context);

    // Act
    var actionResult = await moviesController.Create(new Movie
    {
        Title = "Testing for Fun and Profit",
        DateSeen = DateTime.Now.Date,
        Genre = "Drama",
        Rating = 9
    });

    // Assert
    Assert.IsType<RedirectToActionResult>(actionResult);

    var redirectToActionResult = actionResult as RedirectToActionResult;
    Assert.Equal("Index", redirectToActionResult.ActionName);

    actionResult = await moviesController.Index();
    var viewResult = actionResult as ViewResult;
    var movies = viewResult.Model as List<Movie>;
    
    // Verify count
    Assert.Equal(4, movies.Count);
}
```

- Run all tests. They should pass.

## MoviesController.cs

- Open `MoviesController.cs`.
- Scroll to the `Details` method.
- From the Test menu, select `Live Unit Testing / Start` (this is only available
  in **Visual Studio Enterprise** editions).
- Notice the green checkmarks and blue dashes that appear.
- In the `Details` method, change the return to not return a movie to the view.

```cs
public async Task<IActionResult> Details(int? id)
{
    if (id == null)
    {
        return View("Error", new ErrorViewModel
        {
            Description = "Movie id must be specified."
        });
    }

    var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

    if (movie == null)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = id.ToString(),
            Description = $"Unable to find movie with id={id}."
        });
    }

    return View(/* movie */);
}
```

- Note that some of the tests now fail.
- Change the return back.

```cs
public async Task<IActionResult> Details(int? id)
{
    if (id == null)
    {
        return View("Error", new ErrorViewModel
        {
            Description = "Movie id must be specified."
        });
    }

    var movie = await _context.Movie.FirstOrDefaultAsync(m => m.Id == id);

    if (movie == null)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = id.ToString(),
            Description = $"Unable to find movie with id={id}."
        });
    }

    return View(movie);
}
```

- All the tests pass again.
- From the `Test` menu, select `Live Unit Testing / Stop`.
