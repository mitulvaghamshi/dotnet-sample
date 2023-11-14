using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieTracker.Data;
using MovieTracker.Models;

namespace MovieTracker.Controllers
{
	public class MoviesController : Controller
	{
		private readonly MovieTrackerContext _context;

		public MoviesController(MovieTrackerContext context) => _context = context;

		// GET: DropDatabase
		public async Task<IActionResult> DropDatabase()
		{
			if (!await _context.Database.EnsureCreatedAsync())
				await _context.Database.EnsureDeletedAsync();
			return RedirectToAction(nameof(Index));
		}

		// GET: Movie
		public async Task<IActionResult> Index()
		{
			return _context.Movie != null ?
				View(await _context.Movie.Include(m => m.Genre).ToListAsync()) :
				Problem("Entity set 'MovieTrackerContext.Movie' is null.");
		}

		// GET: Movie/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Movie == null)
				return View("Error", new ErrorViewModel { Description = "Invalid Movie ID" });

			var movie = await _context.Movie
				.Include(m => m.Genre)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (movie == null)
				return View("Error", new ErrorViewModel
				{
					RequestId = id.ToString(),
					Description = "Unable to find a Movie with requested id."
				});
			return View(movie);
		}

		// GET: Movie/Create
		public IActionResult Create()
		{
			ViewData[nameof(Movie.GenreId)] = GenreList();
			return View();
		}

		// POST: Movie/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Title,DateSeen,GenreId,Rating,ReleaseYear")] Movie movie)
		{
			if (ModelState.IsValid)
			{
				_context.Add(movie);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(movie);
		}

		// GET: Movie/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.Movie == null)
				return View("Error", new ErrorViewModel { Description = "Invalid Movie ID" });

			var movie = await _context.Movie.FindAsync(id);
			if (movie == null)
				return View("Error", new ErrorViewModel
				{
					RequestId = id.ToString(),
					Description = "Unable to find a Movie with requested id."
				});

			ViewData[nameof(Movie.GenreId)] = GenreList();
			return View(movie);
		}

		// POST: Movie/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, [Bind("Id,Title,DateSeen,GenreId,Rating,ReleaseYear")] Movie movie)
		{
			if (id != movie.Id)
				return View("Error", new ErrorViewModel { Description = "Invalid Movie ID" });

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
						return View("Error", new ErrorViewModel
						{
							RequestId = id.ToString(),
							Description = "Unable to find a Movie with requested id"
						});
					else throw;
				}
				return RedirectToAction(nameof(Index));
			}
			return View(movie);
		}

		// GET: Movie/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Movie == null)
				return View("Error", new ErrorViewModel { Description = "Invalid Movie ID" });

			var movie = await _context.Movie
				.Include(m => m.Genre)
				.FirstOrDefaultAsync(m => m.Id == id);

			if (movie == null)
				return View("Error", new ErrorViewModel
				{
					RequestId = id.ToString(),
					Description = "Unable to find a Movie with requested id."
				});

			ViewData[nameof(Movie.GenreId)] = GenreList();
			return View(movie);
		}

		// POST: Movie/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Movie == null)
				return Problem("Entity set 'MovieTrackerContext.Movie' is null.");

			var movie = await _context.Movie.FindAsync(id);
			if (movie != null) _context.Movie.Remove(movie);

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool MovieExists(int id) => (_context.Movie?.Any(e => e.Id == id)).GetValueOrDefault();

		private SelectList GenreList() => new(_context.Genres, nameof(Genre.Id), nameof(Genre.GenreDescription));
	}
}
