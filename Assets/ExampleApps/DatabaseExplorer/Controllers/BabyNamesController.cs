using DatabaseExplorer.Data;
using DatabaseExplorer.Models.BabyNames;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DatabaseExplorer.Controllers;

public class BabyNamesController : Controller
{
    private readonly BabyContext _context;

    public BabyNamesController(BabyContext context) => _context = context;

    // GET: Babies
    public async Task<IActionResult> Index()
    {
        var babyContext = _context.Babies.Include(b => b.Nakshatra).Include(b => b.Religion).Include(b => b.Zodiac);

        return View(await babyContext.AsNoTracking().ToListAsync());
    }

    // GET: Babies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Babies == null) return NotFound();

        var baby = await _context.Babies
            .Include(b => b.Nakshatra)
            .Include(b => b.Religion)
            .Include(b => b.Zodiac)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (baby == null) return NotFound();

        return View(baby);
    }

    // GET: Babies/Create
    public IActionResult Create()
    {
        BuildSelectLists();

        return View();
    }

    // POST: Babies/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Meaning,Numerology,Gender,Syllables,NakshatraId,ReligionId,ZodiacId")] Baby baby)
    {
        if (!ModelState.IsValid)
        {
            BuildSelectLists();

            return View(baby);
        }

        _context.Add(baby);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Babies/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Babies == null) return NotFound();

        var baby = await _context.Babies.FindAsync(id);

        if (baby == null) return NotFound();

        BuildSelectLists();

        return View(baby);
    }

    // POST: Babies/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Meaning,Numerology,Gender,Syllables,NakshatraId,ReligionId,ZodiacId")] Baby baby)
    {
        if (id != baby.Id) return NotFound();

        if (!ModelState.IsValid)
        {
            BuildSelectLists();

            return View(baby);
        }

        try
        {
            _context.Update(baby);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!BabyExists(baby.Id)) return NotFound(); else throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Babies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Babies == null) return NotFound();

        var baby = await _context.Babies
            .Include(b => b.Nakshatra)
            .Include(b => b.Religion)
            .Include(b => b.Zodiac)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == id);

        if (baby == null) return NotFound();

        return View(baby);
    }

    // POST: Babies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Babies == null) return Problem("Entity set 'BabyContext.Babies' is null.");

        var baby = await _context.Babies.FindAsync(id);

        if (baby != null) _context.Babies.Remove(baby);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    private void BuildSelectLists()
    {
        ViewData["NakshatraId"] = new SelectList(_context.Nakshatras.AsNoTracking(), "Id", "Name");
        ViewData["ReligionId"] = new SelectList(_context.Religions.AsNoTracking(), "Id", "Name");
        ViewData["ZodiacId"] = new SelectList(_context.Zodiacs.AsNoTracking(), "Id", "Name");
    }

    private bool BabyExists(int id) => (_context.Babies?.AsNoTracking().Any(e => e.Id == id)).GetValueOrDefault();
}
