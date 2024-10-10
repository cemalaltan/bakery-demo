using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SystemAvailabilityTimeController : ControllerBase
    {
        private ISystemAvailabilityTimeService _systemAvailabilityTimeService;
        public SystemAvailabilityTimeController(ISystemAvailabilityTimeService systemAvailabilityTimeService)
        {
            _systemAvailabilityTimeService = systemAvailabilityTimeService;
        }
        [HttpGet("GetSystemAvailabilityTime")]
        public async Task<ActionResult> GetSystemAvailabilityTime()
        {

            try
            {
                var result = await _systemAvailabilityTimeService.GetSystemAvailabilityTimeAsync();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateSystemAvailabilityTime")]
        public async Task<ActionResult> UpdateMoneyReceivedFromMarket(SystemAvailabilityTime systemAvailabilityTime)
        {
            try
            {
                if (systemAvailabilityTime == null || systemAvailabilityTime.CloseTime == systemAvailabilityTime.OpenTime )
                {
                    return BadRequest(Messages.WrongInput);
                }
         


                await _systemAvailabilityTimeService.UpdateSystemAvailabilityTimeAsync(systemAvailabilityTime);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }


            return Ok();
        }
    }
}
