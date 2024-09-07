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
       
        public async Task<IActionResult> Get()
        {
            var GetProducts = await _context.Product.Include(_ => _.Category).ToListAsync();
            return Ok(GetProducts);
        }

        //public ActionResult Get()
        //{
        //    var Protects = _context.Product.ToList();
        //    return Ok(Protects);
        //}


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return Created();
        }


  

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            _context.Product.Update(product);
            await _context.SaveChangesAsync();
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
