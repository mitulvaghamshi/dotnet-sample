using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PhysicianAPI.Data;
using PhysicianAPI.Models;

namespace PhysicianAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PhysiciansController : ControllerBase
{
    private readonly CHDBContext _context;

    public PhysiciansController(CHDBContext context) => _context = context;

    // GET: Physicians
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Physician>>> GetPhysicians()
    {
        if (_context.Physicians == null) return NotFound();

        return await _context.Physicians.AsNoTracking().ToListAsync();
    }

    // GET: Physicians/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Physician>> GetPhysician(int id)
    {
        if (_context.Physicians == null) return NotFound();

        var physician = await _context.Physicians.FindAsync(id);

        if (physician == null) return NotFound();

        return physician;
    }

    // PUT: Physicians/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPhysician(int id, Physician physician)
    {
        if (id != physician.PhysicianId) return BadRequest();

        _context.Entry(physician).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!PhysicianExists(id)) return NotFound(); else throw;
        }

        return NoContent();
    }

    // POST: Physicians
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Physician>> PostPhysician(Physician physician)
    {
        if (_context.Physicians == null) return Problem("Entity set 'CHDBContext.Physicians' is null.");

        physician.PhysicianId = _context.Physicians.Max(p => p.PhysicianId) + 1;
        _context.Physicians.Add(physician);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            if (PhysicianExists(physician.PhysicianId)) return Conflict(); else throw;
        }

        return CreatedAtAction("GetPhysician", new { id = physician.PhysicianId }, physician);
    }

    // DELETE: Physicians/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePhysician(int id)
    {
        if (_context.Physicians == null) return NotFound();

        var physician = await _context.Physicians.FindAsync(id);
        if (physician == null) return NotFound();

        _context.Physicians.Remove(physician);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool PhysicianExists(int id) => (_context.Physicians?.Any(e => e.PhysicianId == id)).GetValueOrDefault();
}
