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
        public ActionResult GetAllProducts()
        {
            try
            {
                var result = _monthlyProductCount.GetAllProducts();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }
        }

        [HttpGet("GetAddedProducts")]
        public ActionResult GetAddedProducts(int year, int month)
        {
            try
            {
                var result = _monthlyProductCount.GetAddedProducts(year, month);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }
        }

        [HttpPost("AddMonthlyProductCounting")]
        public ActionResult AddMonthlyProductCounting(MonthlyProductCount monthlyProductCount)
        {
            try
            {
                if (monthlyProductCount == null)
                {
                    return BadRequest("There is no data!");
                }
                _monthlyProductCount.Add(monthlyProductCount);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpDelete("DeleteMonthlyProductCounting")]
        public ActionResult DeleteMonthlyProductCounting(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Invalid id!");
                }
                _monthlyProductCount.DeleteById(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpPut("UpdateMonthlyProductCounting")]
        public ActionResult UpdateMonthlyProductCounting(MonthlyProductCount monthlyProductCount)
        {
            try
            {
                if (monthlyProductCount == null)
                {
                    return BadRequest("There is no data!");
                }
                _monthlyProductCount.Update(monthlyProductCount);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

    }
}
