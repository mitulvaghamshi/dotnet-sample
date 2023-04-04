using HospitalMVC.Data;
using HospitalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HospitalMVC.Controllers;

public class MedicationsController : Controller
{
	private readonly CHDBContext _context;

	public MedicationsController(CHDBContext context) => _context = context;

	public enum SortOrder { DescriptionDescending, CostDescending, Cost, None };

	// GET: Medications
	public async Task<IActionResult> Index(string term, SortOrder order = SortOrder.None)
	{
		if (_context.Medications == null)
		{
			return Problem("Entity set 'CHDBContext.Medications' is null.");
		}

		ViewBag.SearchTerm = term;
		ViewBag.DescriptionSortParam = order == SortOrder.None ? SortOrder.DescriptionDescending : SortOrder.None;
		ViewBag.CostSortParam = order == SortOrder.Cost ? SortOrder.CostDescending : SortOrder.Cost;

		var medications = from m in _context.Medications select m;		
        if (!string.IsNullOrEmpty(term))
        {
			medications = medications.Where(m => m.MedicationDescription.Contains(term));
        }
        medications = order switch
		{
			SortOrder.DescriptionDescending => medications.OrderByDescending(m => m.MedicationDescription),
			SortOrder.CostDescending => medications.OrderByDescending(m => m.MedicationCost),
			SortOrder.Cost => medications.OrderBy(m => m.MedicationCost),
			SortOrder.None => medications.OrderBy(m => m.MedicationDescription),
			_ => medications,
		};

		return View(await medications.AsNoTracking().ToListAsync());
	}

	// GET: Medications/Details/5
	public async Task<IActionResult> Details(int? id)
	{
		if (id == null || _context.Medications == null)
		{
			return NotFound();
		}

		var medication = await _context.Medications
			.FirstOrDefaultAsync(m => m.MedicationId == id);
		if (medication == null)
		{
			return NotFound();
		}

		return View(medication);
	}

	// GET: Medications/Create
	public IActionResult Create()
	{
		return View();
	}

	// POST: Medications/Create
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Create([Bind("MedicationId,MedicationDescription,MedicationCost,PackageSize,Strength,Sig,UnitsUsedYtd,LastPrescribedDate")] Medication medication)
	{
		if (ModelState.IsValid)
		{
			var nextId = _context.Medications.Max(m => m.MedicationId);
			medication.MedicationId = nextId + 1;

			_context.Add(medication);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
		return View(medication);
	}

	// GET: Medications/Edit/5
	public async Task<IActionResult> Edit(int? id)
	{
		if (id == null || _context.Medications == null)
		{
			return NotFound();
		}

		var medication = await _context.Medications.FindAsync(id);
		if (medication == null)
		{
			return NotFound();
		}
		return View(medication);
	}

	// POST: Medications/Edit/5
	// To protect from overposting attacks, enable the specific properties you want to bind to.
	// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Edit(int id, [Bind("MedicationId,MedicationDescription,MedicationCost,PackageSize,Strength,Sig,UnitsUsedYtd,LastPrescribedDate")] Medication medication)
	{
		if (id != medication.MedicationId)
		{
			return NotFound();
		}

		if (ModelState.IsValid)
		{
			try
			{
				_context.Update(medication);
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!MedicationExists(medication.MedicationId))
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
		return View(medication);
	}

	// GET: Medications/Delete/5
	public async Task<IActionResult> Delete(int? id)
	{
		if (id == null || _context.Medications == null)
		{
			return NotFound();
		}

		var medication = await _context.Medications
			.FirstOrDefaultAsync(m => m.MedicationId == id);
		if (medication == null)
		{
			return NotFound();
		}

		return View(medication);
	}

	// POST: Medications/Delete/5
	[HttpPost, ActionName("Delete")]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> DeleteConfirmed(int id)
	{
		if (_context.Medications == null)
		{
			return Problem("Entity set 'CHDBContext.Medications' is null.");
		}

		var medication = await _context.Medications.FindAsync(id);
		try
		{
			if (medication != null)
			{
				_context.Medications.Remove(medication);
			}
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

	private bool MedicationExists(int id) =>
		(_context.Medications?.Any(e => e.MedicationId == id)).GetValueOrDefault();
}
