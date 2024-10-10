using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoughFactoryProductController : ControllerBase
    {


        private IDoughFactoryProductService _doughFactoryProductService;

        public DoughFactoryProductController(IDoughFactoryProductService doughFactoryProductService)
        {
            _doughFactoryProductService = doughFactoryProductService;
        }

        [HttpGet("GetDoughFactoryProducts")]
        public async Task<ActionResult> GetAllDoughfactoryProducts()
        {

            try
            {
                var result =await _doughFactoryProductService.GetAllProductsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }
        }

        [HttpGet("GetByDoughFactoryProductId")]
        public async Task<ActionResult> GetByDoughFactoryProductId(int doughFactoryProductId)
        {
            if (doughFactoryProductId <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
                var result = await _doughFactoryProductService.GetByIdAsync(doughFactoryProductId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, e.Message);
            }


        }

        [HttpPost("AddDoughFactoryProduct")]
        public async Task<ActionResult> AddProduct(DoughFactoryProduct product)
        {
            if (product == null)
            {
                return BadRequest("There is no data!");
            }
          await  _doughFactoryProductService.AddAsync(product);
            return Ok();
        }

        [HttpPut("UpdateDoughFactoryProduct")]
        public async Task<ActionResult> UpdateProduct(DoughFactoryProduct product)
        {
            if (product == null)
            {
                return BadRequest("There is no data!");
            }
           await _doughFactoryProductService.UpdateAsync(product);
            return Ok();
        }

    }
}
