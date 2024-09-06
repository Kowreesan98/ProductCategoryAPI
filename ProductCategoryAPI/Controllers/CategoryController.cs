using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public ActionResult Get()
        {
            var Categories = _context.Category.ToList();

            return Ok(Categories);
        }

        [HttpPost]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
           
            category.Products ??= new List<Product>();

            _context.Category.Add(category);

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(PostCategory), new { id = category.Id }, category);
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, Category updatedCategory)
        {
            if (id != updatedCategory.Id)
            {
                return BadRequest("Category ID mismatch.");
            }

            var existingCategory = await _context.Category
                .Include(c => c.Products)  
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existingCategory == null)
            {
                return NotFound($"Category with ID {id} not found.");
            }

            existingCategory.Name = updatedCategory.Name;
            existingCategory.CreatedAt = updatedCategory.CreatedAt;

            if (updatedCategory.Products != null)
            {
                existingCategory.Products.Clear();
                foreach (var product in updatedCategory.Products)
                {
                    existingCategory.Products.Add(new Product
                    {
                        Id = product.Id,  
                        Name = product.Name,
                        Price = product.Price,
                        CategoryId = id,  
                        CreatedAt = product.CreatedAt
                    });
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }


        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[HttpPost]

        //public ActionResult Create([FromBody] Category category)
        //{

        //    _context.Category.Add(category);
        //    _context.SaveChanges();

        //    return Ok();
        //}

        //public ActionResult Post(AddCategoryDto addCategoryDto)
        //{

        //    var categoriesEntity = new Category()
        //    {
        //        Name = addCategoryDto.Name,
        //        CreatedAt = DateTime.UtcNow,
        //        Products = new List<Product>()

        //    };

        //    _context.Category.Add(categoriesEntity);
        //    _context.SaveChanges();

        //    return Ok(categoriesEntity);
        //}


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult Delete(int Id)
        {
            if (Id == 0 )
            {
                return BadRequest($"Invaild Id");
            }

            var category = _context.Category.Find(Id);

            if(category == null)
            {
                return NotFound($"This Id was not founded.");
            }

            _context.Category.Remove(category);
            _context.SaveChanges();
            return Ok($"Deleted successfully.");
        }

    }
}
