# Walkthrough 13 - Using a RESTful Web Service

## Setup

This walkthrough will create an MVC application that will consume a **RESTful**
web service.

- Start Visual Studio.
- Click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `ASP.NET Core Web App (Model-View-Controller)` template, click
  `Next`.
- Set Project name to `Blog`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is not selected, click
  `Next`.
- Set version to `.NET 5.0`, unselect `Configure for HTTPS`, click `Create`.

## Post.cs

- Navigate to https://jsonplaceholder.typicode.com.
- We will use this site to generate our test data.
- Click the `/posts` link and inspect the data.
- Change the URL to https://jsonplaceholder.typicode.com/posts/1 to look at a
  single post.
- Select the **JSON** and click `Ctrl+C` to copy it.
- Right-click the `Models` folder and add a `Class` named `Post.cs`.
- Delete the empty `Post` class definition.

```cs
namespace Blog.Models
{
    // public class Post
    // {
    // }
}
```

- Place the cursor before the closing brace of the namespace.
- From the `Edit` menu, select `Paste Special / Paste JSON As Classes`.

```cs
namespace Blog.Models
{
    public class Rootobject
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }
}
```

- Change the name of the class to `Post`.

```cs
public class Post
{
    // ...
}
```

- Save the file.

## HomeController.cs

- Update the `Index` method to be _asynchronous_.

```cs
public async Task<IActionResult> Index()
{
    return View();
}
```

- Add a `try/catch`.

```cs
public async Task<IActionResult> Index()
{
    try
    {
        return View();
    }
    catch (Exception)
    {
        throw;
    }
}
```

- Add the directives:

```cs
using System.Net.Http;
using System.Net.Http.Headers;
```

- Instantiate a list of posts and set up a call to the web server.

```cs
public async Task<IActionResult> Index()
{
    try
    {
        var posts = new List<Post>();

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    // ...
}
```

- Call the web server and get the **JSON**.

```cs
public async Task<IActionResult> Index()
{
    // ...
            var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
            }
    // ...
}
```

- Add a breakpoint to the assignment to the response variable (the web service
  call).
- Press F5 to build and run in debug mode.
- Press F10 to step until after the `json` variable is assigned.
- Hover over the `json` variable and click the magnifying glass to see a text
  representation of the **JSON**.
- Close the dialog.
- Hover again, but click the drop-down to the right of the magnifying glass and
  select **JSON Visualizer**.
- Close the dialog.
- Stop the debugger and remove the breakpoint.
- Add the `using System.Text.Json;` directive, parse the **JSON** into the list
  of `posts` and return them in the view.

```cs
public async Task<IActionResult> Index()
{
    // ,,,
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                posts = JsonSerializer.Deserialize<List<Post>>(json);
            }
    // ...
}
```

- Handle the exception.

```cs
public async Task<IActionResult> Index()
{
    try
    {
        // ...
        return View(posts);
    }
    catch (Exception ex)
    {
        return View("Error", new ErrorViewModel { RequestId = ex.Message });
    }
}
```

- Right-click in the `Index` method and select `Add View...`, select
  `Razor View`, click **Add**.
- Set View name to `Index`, `Template` to `List`, `Model` class to
  `Post (Blog.Models)`, click **Add**, click **Yes** to replace the existing
  view.
- Press Ctrl+F5 to run, all 100 posts are displayed.
- Test the error handling.
- Change the URL to a site that doesn't exist.

```cs
// ...
var response = await client.GetAsync("https://jsonplaceholder.typicode.org/posts");
// ...
```

- Save the file, refresh the browser to see the error.
- Restore the URL.

```cs
// ...
var response = await client.GetAsync("https://jsonplaceholder.typicode.com/posts");
// ...
```

- Save the file, refresh the browser to see the data again.
- To return only the first 10 records, update the return statement.

```cs
// ...
return View(posts.Take(10));
// ...
```

- Save the file. Refresh the page to see only 10 posts.
- To skip the first 25 posts, update the return statement.

```cs
// ...
return View(posts.Skip(25).Take(10));
// ...
```

- Save the file.
- Refresh the page to see a different set of 10 posts.
- Navigate to the documentation of the web service at
  https://github.com/typicode/json-server#slice
- To see that we don't have to get all 100 posts, but that we can retrieve a
  subset.
- Update the request to only return a subset of posts.
- Update the return statement to return all posts in the list.

```cs
// ...
var response = await client
    .GetAsync("https://jsonplaceholder.typicode.com/posts?_start=35&_end=41");
// ...
```

- Save the file.
- Refresh the page to see the specified posts.
- Update the request to sort the posts by title.

```cs
// ...
var response = await client
    .GetAsync("https://jsonplaceholder.typicode.com/posts?_start=35&_end=41&_sort=title");
// ...
```

- Save the file.
- Refresh the page to see the posts sorted by title.
