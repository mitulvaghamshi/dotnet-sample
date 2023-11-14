# Walkthrough 6 - Error Handling

## Setup

This walkthrough will add error handling to the MVC `MovieTracker` application.

- Open `MovieTracker` from the end of the previous walkthrough.

## Error Handling

- At the moment, the app doesn't handle errors gracefully.
- Run the site, navigate to a non-existent URL such as
  http://localhost:12345/abc.
- The error returned isn't great.

## Startup.cs

- Add middleware for status code pages to the `Configure` method.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
    }

    app.UseStatusCodePages();

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

- Save the file, refresh the browser.
- This is a little better, but can be improved.
- Add some more middleware that will re-direct the user to the home page.

```cs
public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    if (env.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler("/Home/Error");
    }

    app.UseStatusCodePages();

    app.Use(async (context, next) =>
    {
        await next();
        if (context.Response.StatusCode == 404)
        {
            context.Request.Path = "/";
            await next();
        }
    });

    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");
    });
}
```

- Save the file, refresh the browser.
- This is better still, but doesn't inform the user that anything is wrong and
  could be confusing.
- Adjust the new middleware to redirect to the error page.

```cs
// ...
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/Error";
        await next();
    }
});
// ...
```

- Save the file, refresh the browser.
- This is better still, but perhaps is too harsh for this common scenario.
- Adjust the new middleware to redirect to a `CustomNotFound` page.

```cs
// ...
app.Use(async (context, next) =>
{
    await next();
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/CustomNotFound";
        await next();
    }
});
// ...
```

- Save the file.

## HomeController.cs

- Add a new method to return a not found page.

```cs
public IActionResult CustomNotFound()
{
    return View();
}
```

- Save the file.
- Right-click in the method and add a `Razor View` named `CustomNotFound` using
  the `Empty` template.

## CustomNotFound.cshtml

- Change the title to `Not Found` and the HTML for the page including a link to
  the home page.

```html
@{ ViewData["Title"] = "Custom Not Found"; }

<h1>Custom Not Found</h1>

<div class="text-center">
    <h1 class="display-1">404</h1>
    <p>The page you are looking for was not found.</p>
    <p><a href="/">Back to Home</a></p>
</div>
```

- Save the file, refresh the browser.
- Notice that the `_Layout` page wasn't specified in `CustomNotFound.cshtml`,
  yet it was still found.

## _ViewStart.cshtml

- Open `Views/_ViewStart.cshtml` to see the layout being specified.

## _ViewImports.cshtml

- Open `_ViewImports.cshtml` to see the using directives common to all views.

## ErrorViewModel.cs

- Click on the `Movies` link, then select the details of the first movie.
- Change the URL to a non-existent movie
  http://localhost:12345/Movies/Details/4, note the error.
- In the `Models` folder, open `ErrorViewModel.cs`.
- Add a new property for description.

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

- In the `Views/Shared` folder, open `Error.cshtml`.
- Add some code for the new `Description` property and remove the
  `Development Mode` text.

```html
@model ErrorViewModel
@{
    ViewData["Title"] = "Error";
}

<h1 class="text-danger">Error.</h1>
<h2 class="text-danger">An error occurred while processing your request.</h2>

@if (Model.ShowRequestId)
{
    <p><strong>Request ID:</strong> <code>@Model.RequestId</code></p>
}

<p><strong>Description:</strong> @Model.Description</p>
```

- Save the file.

## MoviesController.cs

- Update the error handling code in the `Details` method.

```cs
public async Task<IActionResult> Details(int? id)
{
    if (id == null)
    {
        return View("Error", new ErrorViewModel { Description = "Movie id invalid." });
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
```

- Save the file, refresh the browser.
- Update the error handling in the `Edit` methods.

```cs
public async Task<IActionResult> Edit(int? id)
{
    if (id == null)
    {
        return View("Error", new ErrorViewModel { Description = "Movie id invalid." });
    }

    var movie = await _context.Movie.FindAsync(id);

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

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DateSeen,Genre,Rating")] Movie movie)
{
    if (id != movie.Id)
    {
        return View("Error", new ErrorViewModel
        {
            RequestId = id.ToString(),
            Description = $"Movie ids don't match, id={id} not equal to Movie.id={movie.Id}."
        });
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(movie);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!MovieExists(movie.Id))
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = movie.Id.ToString(),
                    Description = $"Unable fo find movie with id={movie.Id}."
                });
            }
            else
            {
                throw;
            }
        }
        return RedirectToAction(nameof(Index));
    }
    return View(movie);
}
```

- Update the error handling in the Get `Delete` method.

```cs
public async Task<IActionResult> Delete(int? id)
{
    if (id == null)
    {
        return View("Error", new ErrorViewModel { Description = "Movie id invalid." });
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

- Save the file.
- Edit a movie and change the URL to a non-existent movie and a non-integer
  movie id. The errors are handled.
- Delete a movie and change the URL to a non-existent movie and a non-integer
  movie id. The errors are handled.

## DateSeenValidation.cs

- Add a movie with a date seen in the future. This doesn't make sense but
  succeeds.
- The Range attribute doesn't work with dates and also requires hard-coding the
  values.
- In the `Models` folder, add a new class named `DateSeenValidation.cs`.
- Make the class inherit the `ValidationAttribute` class.
- Add the `using System.ComponentModel.DataAnnotations;` directive.

```cs
public class DateSeenValidation : ValidationAttribute
{
}
```

- Implement the `IsValid` method.

```cs
public class DateSeenValidation : ValidationAttribute
{
    public override bool IsValid(object value)
    {
        var dateSeen = Convert.ToDateTime(value);
        
        return dateSeen <= DateTime.Now;
    }
}
```

- Save the file.

## Movie.cs

- Add the newly created annotation to the `DateSeen` property with an
  appropriate error message.

```cs
public class Movie
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [DataType(DataType.Date), Display(Name = "Date Seen")]
    [DateSeenValidation(ErrorMessage = "Date can't be in future.")]
    public DateTime? DateSeen { get; set; }

    public string Genre { get; set; }

    [Range(1, 10)]
    public int? Rating { get; set; }
}
```

- Save the file.
- Edit the movie just added and attempt to save with the date seen in the
  future.
- It won't be possible.
- Edit the date seen to the past, save the change.
- Edit the movie again and remove the date, the validation will also pass.
