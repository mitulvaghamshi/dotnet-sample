using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Models;

namespace Recipes.Controllers;

public class RecipesController : Controller
{
    private readonly RecipeContext _context;

    public RecipesController(RecipeContext context) => _context = context;

    // GET: Recipes
    public async Task<IActionResult> Index()
    {
        var recipeContext = _context.Recipe.AsNoTracking().Include(r => r.Category);

        return View(await recipeContext.ToListAsync());
    }

    // GET: Recipes/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Recipe == null) return NotFound();

        var recipe = await _context.Recipe
            .Include(r => r.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (recipe == null) return NotFound();

        return View(recipe);
    }

    // GET: Recipes/Create
    public IActionResult Create()
    {
        ViewData["Categories"] = new SelectList(_context.Category.AsNoTracking(), "Id", "Name");

        return View();
    }

    // POST: Recipes/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,CategoryId,Method,Ingredients,PreparationTime,CookingTime,ReadyIn,Image")] Recipe recipe)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Categories"] = new SelectList(_context.Category.AsNoTracking(), "Id", "Name");

            return View(recipe);
        }
        
        _context.Add(recipe);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Recipes/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Recipe == null) return NotFound();

        var recipe = await _context.Recipe.FindAsync(id);

        if (recipe == null) return NotFound();

        ViewData["Categories"] = new SelectList(_context.Category.AsNoTracking(), "Id", "Name");

        return View(recipe);
    }

    // POST: Recipes/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryId,Method,Ingredients,PreparationTime,CookingTime,ReadyIn,Image")] Recipe recipe)
    {
        if (id != recipe.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            ViewData["Id"] = new SelectList(_context.Category.AsNoTracking(), "Id", "Name", recipe.Id);

            return View(recipe);
        }

        try
        {
            _context.Update(recipe);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!RecipeExists(recipe.Id)) return NotFound(); else throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Recipes/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Recipe == null) return NotFound();

        var recipe = await _context.Recipe
            .Include(r => r.Category)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (recipe == null) return NotFound();

        return View(recipe);
    }

    // POST: Recipes/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Recipe == null) return Problem("Entity set 'Recipe' is null.");

        var recipe = await _context.Recipe.FindAsync(id);

        if (recipe != null) _context.Recipe.Remove(recipe);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool RecipeExists(int id) => (_context.Recipe?.AsNoTracking().Any(e => e.Id == id)).GetValueOrDefault();
}
