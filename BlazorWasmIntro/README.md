# Walkthrough 20 - Blazor WebAssembly Intro

## Setup

This will explore **Blazor WebAssembly**.

- Start Visual Studio.
- Click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `Blazor WebAssembly App` template, click `Next`.
- Set Project name to `BlazorWasmIntro`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is not selected, click
  `Next`.
- Set version to `.NET 5.0`, and:
  - unselect `Configure for HTTPS`,
  - unselect `ASP.NET Core hosted`,
  - unselect `Progressive Web Application`
- Click `Create`.
- Run the site and test each of the 3 pages.
- The functionality is identical to `BlazorServerIntro`.
- Close the browser.

## index.html

- Open `wwwroot/index.html`.
- In the `body` tag, notice the `div` tag for `app`.

## App.razor

- Open `App.razor`.
- This file is the same as in `BlazorServerIntro`.
- Notice the `RouteView` component and the `DefaultLayout="@typeof(MainLayout)"`
  attribute.

## MainLayout.razor

- Open `Shared/MainLayout.razor`.
- This file is the same as in `BlazorServerIntro`.
- Notice the `@Body` property.
- This is where other `Blazor` components will render.
- In the `sidebar` div, notice the `NavMenu` tag.

## NavMenu.razor

- Open `Shared/NavMenu.razor`.
- This file is the same as in `BlazorServerIntro`.
- Notice the `NavLink` components.

## Index.razor

- Open `Pages/Index.razor` file.
- This file is the same as in `BlazorServerIntro`.

## Counter.razor

- Open `Pages/Counter.razor` file.
- This file is the same as in `BlazorServerIntro`.

## FetchData.razor

- Open `Pages/FetchData.razor` file.
- This file is different than in `BlazorServerIntro`.
- Instead of _injecting_ a C# class, the `HttpClient` class is _injected_.
- Recall, we used the `HttpClient` class to call a remote web service.
- Scroll to the `@code` section.
- Notice that the `WeatherForecast` class is defined here.
- Also notice that the `GetFromJsonAsync` method of `HttpClient` is called to
  retrieve the **JSON** from a static file in the `wwwroot / sample-data`
  folder.

## Todo.razor

- Right-click the `Page` folder and select `Add / Razor Component...`, name it
  `Todo.razor`.
- Add a `@page` directive and change the heading from `h3` to `h1`.

```html
@page "/todo"

<h1>Todo</h1>

@code {

}
```

## NavMenu.razor

- Open `Shared/NavMenu.razor`.
- Add a menu item for `Todo` below `Fetch` data.
- Blazor uses the [Open Iconic](https://github.com/iconic/open-iconic) icon set.

```html
<!-- ... -->
        <li class="nav-item px-3">
            <NavLink class="nav-link" href="fetchdata">
                <span> class="oi oi-list-rich"
                  aria-hidden="true"></span>Fetch data
            </NavLink>
        </li>
        <li class="nav-item px-3">
            <NavLink class="nav-link" href="todo">
                <span> class="oi oi-list"
                  aria-hidden="true"></span> Todo
            </NavLink>
        </li>
    </ul>
</div>
<!-- ... -->
```

- Save the file.
- Run the site, click the `Todo` button in the `nav`.

## TodoItem.cs

- Create a `Models` folder.
- Add a class named `TodoItem` to the `Models` folder.
- Add two properties to the class.

```cs
public class TodoItem
{
    public string Title { get; set; }

    public bool IsDone { get; set; }
}
```

- Save the file.

## _Imports.razor

- Open `_Imports.razor`.
- Add the `Models` _namespace_.

```html
@using System.Net.Http
@using System.Net.Http.Json
@using Microsoft.AspNetCore.Components.Forms
@using Microsoft.AspNetCore.Components.Routing
@using Microsoft.AspNetCore.Components.Web
@using Microsoft.AspNetCore.Components.Web.Virtualization
@using Microsoft.AspNetCore.Components.WebAssembly.Http
@using Microsoft.JSInterop
@using BlazorWasmIntro
@using BlazorWasmIntro.Shared
@using BlazorWasmIntro.Models
```

- Save the file.

## Todo.razor

- Add a field for todo items.

```html
@page "/todo"

<h1>Todo</h1>

@code {
    private List<TodoItem> todos = new();
}
```

- Add an unordered list and a `foreach` loop to render each `todo` item as a
  list item.

```html
@page "/todo"

<h1>Todo</h1>

<ul>
    @foreach (var todo in todos)
    {
        <li>@todo.Title</li>
    }
</ul>

@code {
    private List<TodoItem> todos = new();
}
```

- Add a text input and a button to facilitate the creation of new items.

```html
@page "/todo"

<h1>Todo</h1>

<ul>
    @foreach (var todo in todos)
    {
        <li>@todo.Title</li>
    }
</ul>

<input placeholder="Something todo..." />
<button class="btn btn-primary">Add todo</button>

@code {
    private List<TodoItem> todos = new();
}
```

- Save the file.
- Refresh the browser.
- Add a method to handle the button clicks.

```html
<!-- ... -->
@code {
    private List<TodoItem> todos = new();

    private void AddTodo()
    {
      <!-- ... -->
    }
}
```

- Register the method with the button.

```html
<!-- ... -->
<button class="btn btn-primary"
  @onclick="AddTodo">Add todo</button>
<!-- ... -->
```

- Add a string for the new todo item.

```html
<!-- ... -->
@code {
    private List<TodoItem> todos = new();
    private string newTodo;

    private void AddTodo()
    {
      <!-- ... -->
    }
}
```

- Bind the variable to the text input.

```html
<!-- ... -->
<input placeholder="Something todo" @bind="newTodo" />
<!-- ... -->
```

- Implement `AddTodo`.

```cs
private void AddTodo()
{
    if (!string.IsNullOrWhiteSpace(newTodo))
    {
        todos.Add(new TodoItem { Title = newTodo });
        newTodo = "";
    }
}
```

- Save the file.
- Refresh the browser, add some todo items.
- Update the list item to have a checkbox and make each item editable.

```html
<!-- ... -->
<ul>
    @foreach (var todo in todos)
    {
        <li>
            @todo.Title
            <input type="checkbox" @bind="todo.IsDone" />
            <input @bind="todo.Title" />
        </li>
    }
</ul>
<!-- ... -->
```

- Save the file.
- Refresh the browser, add some todo items.
- Update the header to show a count of the number of items that aren't done.

```html
@page "/todo"

<h1>Todo (@todos.Count(todo => !todo.IsDone))</h1>
<!-- ... -->
```

- Save the file.
- Refresh the browser, add some todo items.
- From the File menu, close the solution.
