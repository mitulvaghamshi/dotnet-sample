using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecureSite.Models;
using System.Diagnostics;

namespace SecureSite.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;


    public HomeController(ILogger<HomeController> logger) => _logger = logger;

    public IActionResult Index()
    {
        return View();
    }

    // [Authorize(Roles = "Admin, Manager")] // Either Admin or Manager can access
    [Authorize(Roles = "Admin")]   // Require user to be both, 
    [Authorize(Roles = "Manager")] // Admin and Manager to access
    public IActionResult Privacy() => View();

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
}
