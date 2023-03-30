using IntroMVC.Models;
using Microsoft.AspNetCore.Mvc;

namespace IntroMVC.Controllers
{
	public class HomeController : Controller
	{
		public IActionResult Index()
		{
			Person person = new()
			{
				Id = 1,
				Name = "Mitul Vaghamshi",
				About = "Software Developer",
				Phone = "123 456 7890",
				Address = "Middle of Nowhere",
				City = "Toon Toon",
				Region = "Moon View",
				PostalCode = "BLAH HALB",
				Country = "Funnalicy",
			};

			return View(person);
		}
	}
}
