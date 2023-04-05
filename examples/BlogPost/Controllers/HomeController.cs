using BlogPost.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace BlogPost.Controllers;

public class HomeController : Controller
{
    private readonly static string endpoint = "https://jsonplaceholder.typicode.com";
 
    private readonly static MediaTypeWithQualityHeaderValue jsonHeader = new("application/json");

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger) => _logger = logger;

    public async Task<IActionResult> Index()
    {
        try
        {
            var posts = new List<Post>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(endpoint);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(jsonHeader);

                var response = await client.GetAsync("posts?_start=0&_end=10");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                posts = JsonSerializer.Deserialize<List<Post>>(json);
            }
            return View(posts);
        }
        catch (Exception e)
        {
            return View("Error", new ErrorViewModel { RequestId = e.Message });
        }
    }

    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() =>
        View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
