using HospitalMVC.Data;
using HospitalMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HospitalMVC.Controllers
{
    public class CurrentAdmissionsController : Controller
    {
        private readonly CHDBContext _context;

        public CurrentAdmissionsController(CHDBContext context) => _context = context;

        // GET: CurrentAdmissions
        public async Task<IActionResult> Index(string? nursingUnitId)
        {
            var nursingUnits = from a in _context.NursingUnits
                               orderby a.ManagerLastName
                               select new { Name = a.ManagerLastName + " " + a.ManagerFirstName, a.NursingUnitId };

            ViewBag.NursingUnitId = new SelectList(nursingUnits, "NursingUnitId", "Name");

            var admissions = from a in _context.Admissions.Include(a => a.Patient)
                             .Where(a => a.DischargeDate == null)
                             .OrderBy(a => a.Patient.LastName).ThenBy(a => a.Patient.FirstName)
                             select a;

            ViewBag.Title = "Current Admissions";
            if (!string.IsNullOrEmpty(nursingUnitId))
            {
                ViewBag.Title = $"Current Admissions - {nursingUnitId}";
                admissions = admissions.Where(a => a.NursingUnitId == nursingUnitId);
            }

            return View(await admissions.AsNoTracking().ToListAsync());
        }

        // GET: CurrentAdmissions/Details/5
        public async Task<IActionResult> Details(int? id, DateTime? admissionDate)
        {
            if (id == null || admissionDate == null || _context.Admissions == null) return NotFound();

            var admission = await _context.Admissions
                .Include(a => a.AttendingPhysician)
                .Include(a => a.NursingUnit)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.PatientId == id && m.AdmissionDate == admissionDate);

            if (admission == null) return NotFound();

            return View(admission);
        }

        // GET: CurrentAdmissions/Create
        public IActionResult Create()
        {
            ViewData["AttendingPhysicianId"] = new SelectList(_context.Physicians, "PhysicianId", "PhysicianId");
            ViewData["NursingUnitId"] = new SelectList(_context.NursingUnits, "NursingUnitId", "NursingUnitId");
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId");

            return View();
        }

        // POST: CurrentAdmissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PatientId,AdmissionDate,DischargeDate,PrimaryDiagnosis,SecondaryDiagnoses,AttendingPhysicianId,NursingUnitId,Room,Bed")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["AttendingPhysicianId"] = new SelectList(_context.Physicians, "PhysicianId", "PhysicianId", admission.AttendingPhysicianId);
            ViewData["NursingUnitId"] = new SelectList(_context.NursingUnits, "NursingUnitId", "NursingUnitId", admission.NursingUnitId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", admission.PatientId);

            return View(admission);
        }

        // GET: CurrentAdmissions/Edit/5
        public async Task<IActionResult> Edit(int? id, DateTime? admissionDate)
        {
            if (id == null || admissionDate == null || _context.Admissions == null) return NotFound();

            var admission = await _context.Admissions.Include(a => a.Patient)
                .FirstOrDefaultAsync(a => a.PatientId == id && a.AdmissionDate == admissionDate);

            if (admission == null) return NotFound();

            ViewData["AttendingPhysicianId"] = new SelectList(_context.Physicians, "PhysicianId", "PhysicianId", admission.AttendingPhysicianId);
            ViewData["NursingUnitId"] = new SelectList(_context.NursingUnits, "NursingUnitId", "NursingUnitId", admission.NursingUnitId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", admission.PatientId);

            return View(admission);
        }

        // POST: CurrentAdmissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PatientId,AdmissionDate,DischargeDate,PrimaryDiagnosis,SecondaryDiagnoses,AttendingPhysicianId,NursingUnitId,Room,Bed")] Admission admission)
        {
            if (id != admission.PatientId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionExists(admission.PatientId, admission.AdmissionDate)) return NotFound(); else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["AttendingPhysicianId"] = new SelectList(_context.Physicians, "PhysicianId", "PhysicianId", admission.AttendingPhysicianId);
            ViewData["NursingUnitId"] = new SelectList(_context.NursingUnits, "NursingUnitId", "NursingUnitId", admission.NursingUnitId);
            ViewData["PatientId"] = new SelectList(_context.Patients, "PatientId", "PatientId", admission.PatientId);

            return View(admission);
        }

        // GET: CurrentAdmissions/Delete/5
        public async Task<IActionResult> Delete(int? id, DateTime? admissionDate)
        {
            if (id == null || admissionDate == null || _context.Admissions == null) return NotFound();

            var admission = await _context.Admissions
                .Include(a => a.AttendingPhysician)
                .Include(a => a.NursingUnit)
                .Include(a => a.Patient)
                .FirstOrDefaultAsync(m => m.PatientId == id && m.AdmissionDate == admissionDate);

            if (admission == null) return NotFound();

            return View(admission);
        }

        // POST: CurrentAdmissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, DateTime? admissionDate)
        {
            if (_context.Admissions == null) return Problem("Entity set 'CHDBContext.Admissions'  is null.");

            var admission = await _context.Admissions
                .FirstOrDefaultAsync(a => a.PatientId == id && a.AdmissionDate == admissionDate);

            if (admission != null) _context.Admissions.Remove(admission);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionExists(int id, DateTime? admissionDate) =>
            (_context.Admissions?.Any(e => e.PatientId == id && e.AdmissionDate == admissionDate)).GetValueOrDefault();
    }
}
