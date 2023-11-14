using DatabaseExplorer.Data;
using DatabaseExplorer.Models.CarRentals;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DatabaseExplorer.Controllers;

public class CarRentalsController : Controller
{
    private readonly CarContext _context;

    public CarRentalsController(CarContext context) => _context = context;

    // GET: Cars
    public async Task<IActionResult> Index() => _context.Car != null ?
        View(await _context.Car.AsNoTracking().ToListAsync()) :
        Problem("Entity set 'CarContext.Car' is null.");

    // GET: Cars/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Car == null) return NotFound();

        var car = await _context.Car.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

        if (car == null) return NotFound();

        return View(car);
    }

    // GET: Cars/Create
    public IActionResult Create() => View();

    // POST: Cars/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Make,Model,Color,Year,PurchaseDate,Kilometers")] Car car)
    {
        if (!ModelState.IsValid) return View(car);

        _context.Add(car);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    // GET: Cars/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Car == null) return NotFound();

        var car = await _context.Car.FindAsync(id);

        if (car == null) return NotFound();

        return View(car);
    }

    // POST: Cars/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Make,Model,Color,Year,PurchaseDate,Kilometers")] Car car)
    {
        if (id != car.Id) return NotFound();

        if (!ModelState.IsValid) return View(car);

        try
        {
            _context.Update(car);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CarExists(car.Id)) return NotFound(); else throw;
        }

        return RedirectToAction(nameof(Index));
    }

    // GET: Cars/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Car == null) return NotFound();

        var car = await _context.Car.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

        if (car == null) return NotFound();

        return View(car);
    }

    // POST: Cars/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Car == null) return Problem("Entity set 'CarContext.Car' is null.");

        var car = await _context.Car.FindAsync(id);

        if (car != null) _context.Car.Remove(car);

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    private bool CarExists(int id) => (_context.Car?.AsNoTracking().Any(e => e.Id == id)).GetValueOrDefault();
}
