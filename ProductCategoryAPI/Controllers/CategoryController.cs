using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductCategoryAPI.Data;
using ProductCategoryAPI.Models;

namespace ProductCategoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }


        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]

        public async Task<IActionResult> Get()
        {
            var Categories = await _context.Category.ToListAsync();
            return Ok(Categories);
        }

        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]

        //public IActionResult Post([FromBody] Category category)
        //{
        //    if (!ModelState.IsValid) {
        //        return BadRequest(ModelState);
        //    }
        //   _context.Category.Add(category);
        //   _context.SaveChanges();
        //    return Created($"Created successfully.",category);
        //}

        public async Task<IActionResult> Post( Category category)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }
            _context.Category.Add(category);
           await _context.SaveChangesAsync();
            return Created();
        }




        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> Update(Category category )
        {
             _context.Category.Update(category);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[HttpPost]
        //public IActionResult Post([FromBody] Category category)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    _context.Category.Add(category);
        //    _context.SaveChanges();
        //    return Created($"Created successfully.", category);
        //}

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult Delete(int Id)
        {
            if (Id == 0)
            {
                return BadRequest($"Invaild Id");
            }

            var category = _context.Category.Find(Id);

            if (category == null)
            {
                return NotFound($"This Id was not founded.");
            }

            _context.Category.Remove(category);
            _context.SaveChanges();
            return Ok($"Deleted successfully.");
        }


    }
}
