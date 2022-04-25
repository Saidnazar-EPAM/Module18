#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Module18.Data;
using Module18.Models;

namespace Module18.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly NorthwindContext _context;

        public CategoriesController(NorthwindContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all categories
        /// </summary>
        /// <returns></returns>
        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        /// <summary>
        /// Update picture of category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <param name="picture"></param>
        /// <returns></returns>
        [HttpPost("UpdatePicture/{id}")]
        public async Task<IActionResult> UploadFiles(int id, IFormFile picture)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();
            using(var stream = new MemoryStream())
            {
                await picture.CopyToAsync(stream);
                category.Picture = stream.ToArray();
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        /// <summary>
        /// Get picture of category
        /// </summary>
        /// <param name="id">Category id</param>
        /// <returns></returns>
        [HttpGet("GetPicture/{id}")]
        public async Task<IActionResult> DownloadFile(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();
            //Pictures are BMP images wrapped within OLE objects. So we have to remove the OLE header.
            return File(category.Picture.Skip(78).ToArray(), "image/bmp", $"{category.CategoryName}.bmp");
        }
    }
}
