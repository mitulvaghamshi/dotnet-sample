using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using asignment1.Data;
using asignment1.Models;
using System.Diagnostics;

namespace asignment1.Controllers
{
    public class CarsController : Controller
    {
        private readonly asignment1Context _context;

        public CarsController(asignment1Context context)
        {
            _context = context;
        }

        // GET: Cars
        public async Task<IActionResult> Index()
        {
            return View(await _context.Car.ToListAsync());
        }

        // GET: Cars/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                ErrorViewModel errorModel = new()
                {
                    Description = "Car Id Invalid."
                };
                return View("Error", errorModel);
            }

            var car = await _context.Car.FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                ErrorViewModel errorModel = new()
                {
                    RequestId = id.ToString(),
                    Description = $"Unable to find a car with id={id}."
                };
                return View("Error", errorModel);
            }

            return View(car);
        }

        // GET: Cars/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cars/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Make,Model,Color,Year,PurchaseDate,Kilometers")] Car car)
        {
            if (ModelState.IsValid)
            {
                _context.Add(car);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                ErrorViewModel errorModel = new()
                {
                    RequestId = id.ToString(),
                    Description = "Car Id Invalid."
                };
                return View("Error", errorModel);
            }

            var car = await _context.Car.FindAsync(id);

            if (car == null)
            {
                ErrorViewModel errorModel = new()
                {
                    RequestId = id.ToString(),
                    Description = $"Unable to find a car with id={id}."
                };
                return View("Error", errorModel);
            }

            return View(car);
        }

        // POST: Cars/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Make,Model,Color,Year,PurchaseDate,Kilometers")] Car car)
        {
            if (id != car.Id)
            {
                ErrorViewModel errorModel = new()
                {
                    RequestId = id.ToString(),
                    Description = $"Car ids don't match, id={id} not equal to Car.Id={car.Id}."
                };
                return View("Error", errorModel);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(car);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CarExists(car.Id))
                    {
                        ErrorViewModel errorModel = new()
                        {
                            RequestId = id.ToString(),
                            Description = $"Unable fo find a car with id={car.Id}."
                        };
                        return View("Error", errorModel);
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(car);
        }

        // GET: Cars/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                ErrorViewModel errorModel = new()
                {
                    RequestId = id.ToString(),
                    Description = "Car Id Invalid."
                };
                return View("Error", errorModel);
            }

            var car = await _context.Car.FirstOrDefaultAsync(m => m.Id == id);

            if (car == null)
            {
                ErrorViewModel errorModel = new()
                {
                    RequestId = id.ToString(),
                    Description = $"Unable fo find a car with id={id}."
                };
                return View("Error", errorModel);
            }

            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _context.Car.FindAsync(id);
            _context.Car.Remove(car);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CarExists(int id)
        {
            return _context.Car.Any(e => e.Id == id);
        }
    }
}
