using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductCategoryAPI.Data;
using ProductCategoryAPI.Models;


namespace ProductCategoryApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Get()
        {
            var Protects = _context.Product.ToList();
            return Ok(Protects);
        }



        //[HttpPost]
        // public async Task<ActionResult<Product>> PostProduct(Product product)
        // {
        //     _context.Product.Add(product);
        //     await _context.SaveChangesAsync();

        //     return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        // }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public ActionResult Post([FromBody] Product product)
        {
            _context.Product.Add(product);
            _context.SaveChanges();

            return Ok();

        }

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

            //var product = _context.Product.Find(Id);
            var product = _context.Product.Find(Id);

            if (product == null)
            {
                return NotFound($"This Id was not founded.");
            }

            _context.Product.Remove(product);
            _context.SaveChanges();
            return Ok($"Deleted successfully.");
        }



    }

}
