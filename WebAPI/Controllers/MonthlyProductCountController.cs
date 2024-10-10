using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonthlyProductCountController : ControllerBase
    {
        private IMonthlyProductCountService _monthlyProductCount;


        public MonthlyProductCountController(IMonthlyProductCountService monthlyProductCount)
        {
            _monthlyProductCount = monthlyProductCount; 
        }

        [HttpGet("GetAllProducts")]
        public async Task<ActionResult> GetAllProducts()
        {
            try
            {
                var result = await _monthlyProductCount.GetAllProductsAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }
        }

        [HttpGet("GetAddedProducts")]
        public async Task<ActionResult> GetAddedProducts(int year, int month)
        {
            try
            {
                var result = await _monthlyProductCount.GetAddedProductsAsync(year, month);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }
        }

        [HttpPost("AddMonthlyProductCounting")]
        public async Task<ActionResult> AddMonthlyProductCounting(MonthlyProductCount monthlyProductCount)
        {
            try
            {
                if (monthlyProductCount == null)
                {
                    return BadRequest("There is no data!");
                }
               await _monthlyProductCount.AddAsync(monthlyProductCount);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpDelete("DeleteMonthlyProductCounting")]
        public async Task<ActionResult> DeleteMonthlyProductCounting(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id!");
                }
               await _monthlyProductCount.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpPut("UpdateMonthlyProductCounting")]
        public async Task<ActionResult> UpdateMonthlyProductCounting(MonthlyProductCount monthlyProductCount)
        {
            try
            {
                if (monthlyProductCount == null)
                {
                    return BadRequest("There is no data!");
                }
                await _monthlyProductCount.UpdateAsync(monthlyProductCount);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

    }
}
