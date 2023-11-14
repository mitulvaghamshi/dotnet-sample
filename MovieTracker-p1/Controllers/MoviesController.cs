using Microsoft.AspNetCore.Mvc;
using MovieTracker.Models;

namespace MovieTracker.Controllers
{
	public class MoviesController : Controller
	{
		private static List<Movie> movies = new() { new Movie { Id = 1, Title = "Birds of Prey", DateSeen = DateTime.Now.AddDays(-50), Genre = "Action", Rating = 6, }, new Movie { Id = 2, Title = "Palm Springs", DateSeen = DateTime.Now.AddDays(-25), Rating = 7, }, new Movie { Id = 3, Title = "Hamilton", Genre = "Drama", } };

		// GET: MoviesController
		public ActionResult Index()
		{
			return View(movies);
		}

		// GET: MoviesController/Details/5
		public ActionResult Details(int id)
		{
			var movie = movies.Find(x => x.Id == id);

			return View(movie);
		}

		// GET: MoviesController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: MoviesController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(/*IFormCollection collection*/ Movie movie)
		{
			try
			{
				//movies.Add(new Movie
				//{
				//    Title = collection[nameof(Movie.Title)],
				//    DateSeen = DateTime.Parse(collection[nameof(Movie.DateSeen)]),
				//    Genre = collection[nameof(Movie.Genre)],
				//    Rating = int.Parse(collection[nameof(Movie.Rating)]),
				//});

				if (!ModelState.IsValid) { return View(movie); }

				movies.Add(movie);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: MoviesController/Edit/5
		public ActionResult Edit(int id)
		{
			var movie = movies.Find(movie => movie.Id == id);

			return View(movie);
		}

		// POST: MoviesController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, /*IFormCollection collection*/ Movie movie)
		{
			try
			{
				if (!ModelState.IsValid) { return View(movie); }

				var index = movies.FindIndex(movie => movie.Id == id);

				movies[index] = movie;

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: MoviesController/Delete/5
		public ActionResult Delete(int id)
		{
			var movie = movies.Find(movie => movie.Id == id);

			return View(movie);
		}

		// POST: MoviesController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, /*IFormCollection collection*/ Movie movie)
		{
			try
			{
				var index = movies.FindIndex(movie => movie.Id == id);

				movies.RemoveAt(index);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
