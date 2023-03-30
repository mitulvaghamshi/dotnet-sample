using assignment2.Data;
using assignment2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace assignment2.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            _context = context;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Categories.AsNoTracking().ToListAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = id.ToString(),
                    Description = $"A category with id {id}, does not exists."
                });
            }

            var category = await _context.Categories.FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = id.ToString(),
                    Description = $"Unable to find a category with id: {id}."
                });
            }

            ViewBag.Title = category.CategoryName;
            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CategoryId,CategoryName,Description")] Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = id.ToString(),
                    Description = $"A category with id {id}, does not exists."
                });
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = id.ToString(),
                    Description = $"Unable to find a category with id: {id}."
                });
            }

            ViewBag.Title = $"Edit {category.CategoryName}";
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CategoryId,CategoryName,Description")] Category category)
        {
            if (id != category.CategoryId)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = id.ToString(),
                    Description = $"A category with id {id}, does not exists."
                });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(category.CategoryId))
                    {
                        return View("Error", new ErrorViewModel
                        {
                            RequestId = id.ToString(),
                            Description = $"Unable to find a category with id: {id}."
                        });
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = id.ToString(),
                    Description = $"A category with id {id}, does not exists."
                });
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.CategoryId == id);
            if (category == null)
            {
                return View("Error", new ErrorViewModel
                {
                    RequestId = id.ToString(),
                    Description = $"Unable to find a category with id: {id}."
                });
            }

            ViewBag.Title = $"Delete {category.CategoryName}";
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.CategoryId == id);
        }
    }
}
