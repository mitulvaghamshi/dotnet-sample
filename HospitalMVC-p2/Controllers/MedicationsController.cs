using HospitalMVC.Data;
using HospitalMVC.Models;
using HospitalMVC.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalMVC.Controllers;

public class MedicationsController : Controller
{
    private readonly CHDBContext _context;

    public MedicationsController(CHDBContext context) => _context = context;

    // GET: Medications
    public async Task<IActionResult> Index(QueryParam reqParam = default!)
    {
        if (_context.Medications == null) return Problem("Entity set 'CHDBContext.Medications' is null.");

        var medications = string.IsNullOrEmpty(reqParam.SearchTerm) ? from m in _context.Medications select m :
            from m in _context.Medications where m.MedicationDescription.Contains(reqParam.SearchTerm) select m;

        medications = reqParam.SortOrder switch
        {
            SortOrder.DescDescending => medications.OrderByDescending(m => m.MedicationDescription),
            SortOrder.Description => medications.OrderBy(m => m.MedicationDescription),
            SortOrder.CostDescending => medications.OrderByDescending(m => m.MedicationCost),
            SortOrder.Cost => medications.OrderBy(m => m.MedicationCost),
            SortOrder.None => medications,
            _ => throw new NotImplementedException(),
        };

        ViewBag.Response = reqParam;
        return View(await PaginatedList<Medication>.CreateAsync(medications.AsNoTracking(), reqParam.PageIndex, reqParam.PageSize));
    }

    // GET: Medications/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Medications == null) return NotFound();
        var medication = await _context.Medications.FirstOrDefaultAsync(m => m.MedicationId == id);
        if (medication == null) return NotFound();
        return View(medication);
    }

    // GET: Medications/Create
    public IActionResult Create() => View();

    // POST: Medications/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MedicationId,MedicationDescription,MedicationCost,PackageSize,Strength,Sig,UnitsUsedYtd,LastPrescribedDate")] Medication medication)
    {
        if (!ModelState.IsValid) return View(medication);
        var nextId = _context.Medications.Max(m => m.MedicationId);
        medication.MedicationId = nextId + 1;
        _context.Add(medication);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    // GET: Medications/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Medications == null) return NotFound();
        var medication = await _context.Medications.FindAsync(id);
        if (medication == null) return NotFound();
        return View(medication);
    }

    // POST: Medications/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("MedicationId,MedicationDescription,MedicationCost,PackageSize,Strength,Sig,UnitsUsedYtd,LastPrescribedDate")] Medication medication)
    {
        if (id != medication.MedicationId) return NotFound();
        if (!ModelState.IsValid) return View(medication);
        try
        {
            _context.Update(medication);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (MedicationExists(medication.MedicationId)) throw;
            return NotFound();
        }
        return RedirectToAction(nameof(Index));
    }

    // GET: Medications/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Medications == null) return NotFound();
        var medication = await _context.Medications.FirstOrDefaultAsync(m => m.MedicationId == id);
        if (medication == null) return NotFound();
        return View(medication);
    }

    // POST: Medications/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Medications == null) return Problem("Entity set 'CHDBContext.Medications' is null.");
        var medication = await _context.Medications.FindAsync(id);
        try
        {
            if (medication != null) _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        catch (Exception)
        {
            return View("Error", new ErrorViewModel
            {
                RequestId = id.ToString(),
                Description = "Unable to delete this medication, It may be required by other data in the system."
            });
        }
    }

    private bool MedicationExists(int id) => (_context.Medications?.Any(e => e.MedicationId == id)).GetValueOrDefault();
}
