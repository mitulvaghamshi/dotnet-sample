using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NorthwindAPI.Data;
using NorthwindAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NorthwindAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context) => _context = context;

        // GET: Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        //GET: Categories/GetCategoryBy/{id}
        [HttpGet("GetCategoryBy/{id}")]
        public async Task<ActionResult<Category>> GetCategoryBy(int id)
        {
            return await _context.Categories.FirstAsync(c => c.CategoryId == id);
        }
    }
}
