using Microsoft.AspNetCore.Mvc;

namespace IntroMVC.Controllers
{
	public class AboutController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}

		[Route("info/{id:int?}")]
		public IActionResult Post(int? id)
		{
			ViewBag.id = id;

			return View();
		}
	}
}
