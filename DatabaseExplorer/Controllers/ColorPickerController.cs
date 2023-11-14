using DatabaseExplorer.Data;
using DatabaseExplorer.Models.ColorPicker;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DatabaseExplorer.Controllers;

public class ColorPickerController : Controller
{
    private readonly ColorContext _context;

    public ColorPickerController(ColorContext context) => _context = context;

    // GET: Color
    public async Task<IActionResult> Index()
    {
        var context = _context.Color.Include(c => c.Group);

        return View(await context.AsNoTracking().ToListAsync());
    }

    // GET: Color/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Color == null) return NotFound();

        var colors = await _context.Color
            .Include(c => c.Group)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (colors == null) return NotFound();

        return View(colors);
    }

    // GET: Color/Create
    public IActionResult Create()
    {
        ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name");

        return View();
    }

    // POST: Color/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Value,GroupId")] Color colors)
    {
        if (ModelState.IsValid)
        {
            _context.Add(colors);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", colors.GroupId);

        return View(colors);
    }

    // GET: Color/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Color == null) return NotFound();

        var colors = await _context.Color.FindAsync(id);

        if (colors == null) return NotFound();

        ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", colors.GroupId);

        return View(colors);
    }

    // POST: Color/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, string OldValue, [Bind("Id,Name,Value,GroupId")] Color colors)
    {
        if (id != colors.Id) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                if (colors.Value == OldValue)
                {
                    _context.Update(colors);
                    await _context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColorsExists(colors.Id)) return NotFound(); else throw;
            }
            return RedirectToAction(nameof(Index));
        }

        ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", colors.GroupId);

        return View(colors);
    }

    // GET: Color/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Color == null) return NotFound();

        var colors = await _context.Color
            .Include(c => c.Group)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (colors == null) return NotFound();

        return View(colors);
    }

    // POST: Color/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Color == null) return Problem("Entity set 'ColorContext.Color' is null.");

        var colors = await _context.Color.FindAsync(id);

        if (colors != null) _context.Color.Remove(colors);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool ColorsExists(int id) => (_context.Color?.AsNoTracking().Any(e => e.Id == id)).GetValueOrDefault();
}
