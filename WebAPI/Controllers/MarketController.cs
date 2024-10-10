using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketController : ControllerBase
    {

        private IMarketService _marketService;


        public MarketController(IMarketService marketService)
        {
            _marketService = marketService;
        }



        [HttpGet("GetAllMarket")]
        public async Task<ActionResult> GetMarket()
        {
            try
            {
                var result = await _marketService.GetAllAsync();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetByIdMarket")]
        public async Task<ActionResult> GetByIdMarket(int id)
        {
            try
            {
                var result =await _marketService.GetByIdAsync(id);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddMarket")]
        public async Task<ActionResult> AddMarket(Market market)
        {
            try
            {
               await _marketService.AddAsync(market);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteMarketById")]
        public async Task<ActionResult> DeleteMarketById(int id)
        {
            try
            {
               await _marketService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateMarket")]
        public async Task<ActionResult> UpdateMarket(Market market)
        {
            try
            {
               await _marketService.UpdateAsync(market);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}