using BabyNames.Data;
using BabyNames.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace BabyNames.Controllers;

public class BabyNamesController : Controller
{
    private readonly BabyNameContext _context;

    public BabyNamesController(BabyNameContext context) => _context = context;

    // GET: BabyNames
    public async Task<IActionResult> Index()
    {
        var babyNameContext = _context.BabyName.Include(b => b.Religion);
        return View(await babyNameContext.AsNoTracking().ToListAsync());
    }

    // GET: BabyNames/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.BabyName == null)
        {
            return NotFound();
        }

        var babyName = await _context.BabyName
            .Include(b => b.Religion)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (babyName == null)
        {
            return NotFound();
        }

        return View(babyName);
    }

    // GET: BabyNames/Create
    public IActionResult Create()
    {
        ViewData["ReligionId"] = new SelectList(_context.Religion.AsNoTracking(), "Id", "Name");
        return View();
    }

    // POST: BabyNames/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Meaning,Numerology,Gender,ReligionId")] BabyName babyName)
    {
        if (ModelState.IsValid)
        {
            _context.Add(babyName);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["ReligionId"] = new SelectList(_context.Religion.AsNoTracking(), "Id", "Name", babyName.ReligionId);

        return View(babyName);
    }

    // GET: BabyNames/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.BabyName == null)
        {
            return NotFound();
        }

        var babyName = await _context.BabyName.FindAsync(id);
        if (babyName == null)
        {
            return NotFound();
        }
        ViewData["ReligionId"] = new SelectList(_context.Religion.AsNoTracking(), "Id", "Name", babyName.ReligionId);

        return View(babyName);
    }

    // POST: BabyNames/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Meaning,Numerology,Gender,ReligionId")] BabyName babyName)
    {
        if (id != babyName.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(babyName);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BabyNameExists(babyName.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }
        ViewData["ReligionId"] = new SelectList(_context.Religion.AsNoTracking(), "Id", "Name", babyName.ReligionId);
        return View(babyName);
    }

    // GET: BabyNames/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.BabyName == null)
        {
            return NotFound();
        }

        var babyName = await _context.BabyName
            .Include(b => b.Religion)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (babyName == null)
        {
            return NotFound();
        }

        return View(babyName);
    }

    // POST: BabyNames/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.BabyName == null)
        {
            return Problem("Entity set 'BabyNameContext.BabyName' is null.");
        }
        var babyName = await _context.BabyName.FindAsync(id);
        if (babyName != null)
        {
            _context.BabyName.Remove(babyName);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BabyNameExists(int id)
    {
        return (_context.BabyName?.AsNoTracking().Any(e => e.Id == id)).GetValueOrDefault();
    }
}
