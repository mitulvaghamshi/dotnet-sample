# Walkthrough 21 - Blazor WebAssembly Server

## Setup

- Click `Create a new project`.
- Select the `Blazor WebAssembly App` template, click `Next`.
- Set Project name to `BlazorWasmWithBackend`.
- Set Location to a folder of your choosing.
- Ensure Place solution and project in the same directory is not selected, click
  `Next`.
- Set version to `.NET 5.0`, and:
  - unselect `Configure for HTTPS`,
  - select `ASP.NET Core hosted`,
  - unselect `Progressive Web Application`
- Click `Create`.
- Notice that 2 projects have been created
- Run the site and test each of the 3 pages.
- The functionality is identical to `BlazorServerIntro` and `BlazorWasmIntro`.
- Close the browser.

## BlazorWasmWithBackend.Shared

- Note the `WeatherForecast` class is here.

## WeatherForecastController.cs

- Open
  `BlazorWasmWithBackend.Server / Controllers / WeatherForecastController.cs`.
- Note that it is a **Web API Controller**.
- Modify the `Get` method to accept a parameter to determine the number of
  forecasts that will be returned.

```cs
[HttpGet]
public IEnumerable<WeatherForecast> Get(int num = 5)
{
    var rng = new Random();

    return Enumerable.Range(1, num)
      .Select(index => new WeatherForecast
    {
        Date = DateTime.Now.AddDays(index),
        TemperatureC = rng.Next(-20, 55),
        Summary = Summaries[rng.Next(Summaries.Length)]
    })
    .ToArray();
}
```

- Save the file.

## FetchData.razor

- Open `BlazorWasmWithBackend.Client / Pages / FetchData.razor`.
- Add a variable for number of forecasts to retrieve.

```html
...
@code {
    private WeatherForecast[] forecasts;

    private string numberOfForecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await Http
          .GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
    }
}
```

- Add a method to get the forecasts.

```html
<!-- ... -->
@code {
    private WeatherForecast[] forecasts;
    private string numberOfForecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await Http
          .GetFromJsonAsync<WeatherForecast[]>("WeatherForecast");
    }

    private async Task GetForecasts()
    {
        int.TryParse(numberOfForecasts, out int num);

        if (num <= 0)
        {
            num = 5;
        }

        numberOfForecasts = num.ToString();

        forecasts = await Http
          .GetFromJsonAsync<WeatherForecast[]>($"WeatherForecast/?num={num}");
    }
}
```

- Add a text input and a button to retrieve the forecasts.

```html
@page "/fetchdata"

@using BlazorWasmWithBackend.Shared

@inject HttpClient Http

<h1>Weather forecast</h1>

<p>This component demonstrates fetching data from the server.</p>
<p>
    Number of forecasts to retrieve
    <input @bind="numberOfForecasts" />
    <button class="btn btn-primary"
      @onclick="GetForecasts">Get Forecasts</button>
</p>

@if (forecasts == null)
<!-- ... -->
```

- Save the file.
- Run the site.
- Try retrieving a different number of forecasts, including invalid input.
- From the `File` menu, close the solution.
