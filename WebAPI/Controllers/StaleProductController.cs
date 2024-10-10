using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaleProductController : ControllerBase
    {

        private IStaleProductService _staleProductService;


        public StaleProductController(IStaleProductService staleProductService)
        {
            _staleProductService = staleProductService;
        }

        [HttpGet("GetStaleProductsByDateAndCategory")]
        public async Task<ActionResult> GetStaleProductsByDateAndCategory(DateTime date, int categoryId)
        {
            try
            {
                var result = await _staleProductService.GetStaleProductsByDateAndCategoryAsync(date, categoryId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
        
        [HttpGet("GetByDateAndCategory")]
        public async Task<ActionResult> GetByDateAndCategory(DateTime date, int categoryId)
        {
            try
            {
                var result = await _staleProductService.GetByDateAndCategoryAsync(date, categoryId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetProductsNotAddedToStale")]
        public async Task<ActionResult> GetProductsNotAddedToStale(DateTime date, int categoryId)
        {
            try
            {
                var result = await _staleProductService.GetProductsNotAddedToStaleAsync(date, categoryId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddStaleProduct")]
        public async Task<ActionResult> AddStaleProduct(StaleProduct staleProduct)
        {
            try
            {
                if (await _staleProductService.IsExistAsync(staleProduct.ProductId, staleProduct.Date))
                {
                    return BadRequest(Messages.OncePerDay);
                }
                if (staleProduct == null || staleProduct.Quantity < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

               await _staleProductService.AddAsync(staleProduct);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteStaleProduct")]
        public async Task<ActionResult> DeleteStaleProduct(int id)
        {
            try
            {
               await _staleProductService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateStaleProduct")]
        public async Task<ActionResult> UpdateStaleProduct(StaleProduct staleProduct)
        {
            try
            {
               await _staleProductService.UpdateAsync(staleProduct);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}