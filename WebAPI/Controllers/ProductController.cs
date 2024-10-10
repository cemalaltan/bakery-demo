using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private IProductService _productService;
    


        public ProductController(IProductService productService)
        {
            _productService = productService; 
        }

        [HttpGet("GetAllProductsBycategoryId")]
        public async Task<ActionResult> GetAllProductsBycategoryId(int categoryId)
        {
            var result = await _productService.GetAllByCategoryIdAsync(categoryId);
            return Ok(result);
        }
        
       
        [HttpPost("AddProduct")]
        public async Task<ActionResult> AddProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("There is no data!");
            }
           await _productService.AddAsync(product);
            return Ok();
        }


        [HttpPut("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct(Product product)
        {
            if (product == null)
            {
                return BadRequest("There is no data!");
            }
          await  _productService.UpdateAsync(product);
            return Ok();
        }
    }
}