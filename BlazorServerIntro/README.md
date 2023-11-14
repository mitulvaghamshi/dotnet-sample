# Walkthrough 19 - Blazor Server Introduction

## Setup

This will explore Blazor Server.

- Start Visual Studio.
- Click `Create a new project`.
- Set language to `C#` and project type to `Web`.
- Select the `Blazor Server App` template, click `Next`.
- Set Project name to `BlazorServerIntro`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is not selected, click
  `Next`.
- Set version to `.NET 5.0`, unselect `Configure for HTTPS`, click `Create`.
- Run the site and test each of the 3 pages.
- Close the browser.

## Startup.cs

- Open `Startup.cs`.
- Notice the differences in the `ConfigureServics` and `Configure` methods.
- In `Configure`, notice the `endpoints.MapFallbackToPage("/_Host")` statement.

## _Host.cshtml

- Open `Pages/_Host.cshtml`.
- In the `body` tag, notice the `<component type="typeof(App)" ...` tag.

## App.razor

- Open `App.razor`.
- Notice the `RouteView` component and the `DefaultLayout="@typeof(MainLayout)"`
  attribute.

## MainLayout.razor

- Open `Shared/MainLayout.razor`.
- Notice the `@Body` property.
- This is where other `Blazor` components will render.
- In the `sidebar` div, notice the `NavMenu` tag.

## NavMenu.razor

- Open `Shared/NavMenu.razor`.
- Notice the `NavLink` components.

## Index.razor

- Delete the existing `Pages / Index.razor` file.
- Right-click the `Page` folder and select `Add / Razor Component...`, name it
  `Index.razor`.
- Delete the existing code.
- Add a page directive that establishes the routing.

```html
@page "/"
```

- Add an h1 tag and a greeting.

```html
@page "/"

<h1>Hello, world!</h1>

Welcome to your new app.
```

- Add the `SurveyPrompt` component with the title property.

```html
@page "/"

<h1>Hello, world!</h1>

Welcome to your new app.

<SurveyPrompt Title="How is Blazor working for you?" />
```

- Run the site, it should function as before.

## SurveyPrompt.razor

- Open `Shared / SurveyPrompt.razor`.
- Notice how the `Title` property is received and used.

## Counter.razor

- Delete the existing `Pages / Counter.razor` file.
- Right-click the `Page` folder and select `Add / Razor Component...`, name it
  `Counter.razor`.
- Delete the existing code.

```html
<h3>Counter</h3>

@code {

}
```

- Add a page directive that establishes the routing and a header.

```html
@page "/counter"

<h1>Counter</h1>
```

- In the browser, click the `Counter` link.
- Add a code block.

```html
@page "/counter"

<h1>Counter</h1>

@code {

}
```

- Declare a variable to hold the count and a method to interact with it.

```html
@page "/counter"

<h1>Counter</h1>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

- Display the variable and add a button to call the method.

```html
@page "/counter"

<h1>Counter</h1>

<p>Current count: @currentCount</p>

<button class="btn btn-primary"
    @onclick="IncrementCount">Click me</button>

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

- Save the file, refresh the browser if necessary.
- The `counter` functions as before.
- To illustrate how components can be nested, add the `Index` component.

```html
@page "/counter"

<h1>Counter</h1>

<p>Current count: @currentCount</p>

<button class="btn btn-primary"
    @onclick="IncrementCount">Click me</button>

<Index />

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

- Save the file, refresh the browser if necessary.
- Notice the `Index` component.
- Remove the `Index` component.

```html
@page "/counter"

<h1>Counter</h1>

<p>Current count: @currentCount</p>

<button class="btn btn-primary"
    @onclick="IncrementCount">Click me</button>

<Index />

@code {
    private int currentCount = 0;

    private void IncrementCount()
    {
        currentCount++;
    }
}
```

- Save the file, refresh the browser if necessary.

## WeatherForecast.cs

- Open `Data/WeatherForecast.cs`.
- This is the class that the `WeatherForecastService` returns.

## WeatherForecastService.cs

- Open `Data/WeatherForecastService.cs`
- Notice that it is just a **Plain Old C# Object** (POCO) and not a web service,
  because with **Blazor Server**, all of the code runs **server-side**.

## FetchData.razor

- Delete the existing `Pages / FetchData.razor` file.
- Right-click the `Page` folder and select `Add / Razor Component...`, name it
  `FetchData.razor`.
- Delete the existing code.

```html
<h3>FetchData</h3>

@code {

}
```

- Add a `@page` directive that establishes the routing, a header, some text, and
  a code block.

```html
@page "/fetchdata"

<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from a service.</p>

@code {

}
```

- Add an `@using` directive to the `Data` namespace.
- Declare an array of `WeatherForecasts`.

```html
@page "/fetchdata"

@using BlazorServerIntro.Data

<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from a service.</p>

@code {
    private WeatherForecast[] forecasts;
}
```

- Inject the `WeatherForecastService`.
- Override the `OnInitializedAsync` method to get the `forecasts`.

```html
@page "/fetchdata"

@using BlazorServerIntro.Data

@inject WeatherForecastService ForecastService

<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from a service.</p>

@code {
    private WeatherForecast[] forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await ForecastService
            .GetForecastAsync(DateTime.Now);
    }
}
```

- Display a loading message or the count of forecasts.

```html
@page "/fetchdata"

@using BlazorServerIntro.Data

@inject WeatherForecastService ForecastService

<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from a service.</p>

@if (forecasts == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <p>Retrieved @forecasts.Count() forecast(s).</p>
}

@code {
    private WeatherForecast[] forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await ForecastService
            .GetForecastAsync(DateTime.Now);
    }
}
```

- Save the file, in the browser, click the `Fetch` data link.
- Replace the `count` output with a table of `forecasts`.

```html
<!-- ... -->
@if (forecasts == null)
{
    <p><em>Loading...</em></p>
}
else
{
    <p>Retrieved @forecasts.Count() forecast(s).</p>

    <table class="table">
        <thead>
            <tr>
                <th>Date</th>
                <th>Temp. (C)</th>
                <th>Temp. (F)</th>
                <th>Summary</th>
            </tr>
        </thead>
        <tbody>
            @foreach (var forecast in forecasts)
            {
                <tr>
                    <td>@forecast.Date.ToShortDateString()</td>
                    <td>@forecast.TemperatureC</td>
                    <td>@forecast.TemperatureF</td>
                    <td>@forecast.Summary</td>
                </tr>
            }
        </tbody>
    </table>
}
<!-- ... -->
```

- Save the file, refresh the browser if necessary.
- Fetch data functions as before.
