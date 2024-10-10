using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreadPriceController : ControllerBase
    {


        private IBreadPriceService _breadPriceService;


        public BreadPriceController(IBreadPriceService breadPriceService)
        {
            _breadPriceService = breadPriceService; ;
        }

        [HttpGet("GetAllBreadPrices")]
        public async Task<ActionResult> GetAllBreadPrices()
        {
            try
            {
                List<BreadPrice> price = await _breadPriceService.GetAllAsync();
                return Ok(price);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetBreadPriceByDate")]
        public async Task<ActionResult> GetBreadPriceByDate(DateTime date)
        {
            try
            {
                decimal price = await _breadPriceService.BreadPriceByDateAsync(date);
                return Ok(price);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddBreadPrice")]
        public async Task<ActionResult> AddBreadPrice(BreadPrice breadPrice)
        {

            try
            {
                if (await _breadPriceService.IsExistByDateAsync(breadPrice.Date))
                {
                    return BadRequest(Messages.OncePerDay);
                }

                if (breadPrice == null || breadPrice.Price <= 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

               await  _breadPriceService.AddAsync(breadPrice);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteBreadPriceById")]
        public async Task<ActionResult> DeleteBreadPriceById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
               await _breadPriceService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateBreadPrice")]
        public async Task<ActionResult> UpdateBreadPrice(BreadPrice breadPrice)
        {
            if (breadPrice == null || breadPrice.Price <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                if (breadPrice.Price == 0)
                {
                   await _breadPriceService.DeleteByIdAsync(breadPrice.Id);
                }
                else
                {
                   await _breadPriceService.UpdateAsync(breadPrice);

                }


                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}
