using Business.Abstract;
using Entities.Concrete;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GivenProductsToServiceController : ControllerBase
    {

        private IGivenProductsToServiceService _givenProductsToServiceService;


        public GivenProductsToServiceController(IGivenProductsToServiceService givenProductsToServiceService)
        {
            _givenProductsToServiceService = givenProductsToServiceService; ;
        }

        [HttpGet("GetGivenProductsToServiceByDateAndServisTypeId")]
        public async Task<ActionResult> GetGivenProductsToServiceByDateAndServisTypeId(DateTime date, int servisTypeId)
        {
            try
            {
                var result =await _givenProductsToServiceService.GetAllByDateAndServisTypeIdAsync(date, servisTypeId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetGivenProductsToServiceDayResultByDateAndServiceType")]
        public async Task<ActionResult> GetGivenProductsToServiceDayResult(DateTime date)
        {
            try
            {
                var result = await _givenProductsToServiceService.GetTotalQuantityByDateAsync(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetByIdGivenProductsToService")]
        public async Task<ActionResult> GetByIdGivenProductsToService(int id)
        {
            try
            {
                var result = await _givenProductsToServiceService.GetByIdAsync(id);
                return Ok(result);

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddGivenProductsToService")]
        public async Task<ActionResult> AddGivenProductsToService(GivenProductsToService givenProductsToService)
        {
            try
            {
                await _givenProductsToServiceService.AddAsync(givenProductsToService);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteGivenProductsToServiceById")]
        public async Task<ActionResult> DeleteGivenProductsToServiceById(int id)
        {
            try
            {
               await _givenProductsToServiceService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateGivenProductsToService")]
        public async Task<ActionResult> UpdateGivenProductsToService(GivenProductsToService givenProductsToService)
        {
            try
            {
               await _givenProductsToServiceService.UpdateAsync(givenProductsToService);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}