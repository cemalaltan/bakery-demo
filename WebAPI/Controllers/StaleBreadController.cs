using Business.Abstract;
using Business.Constants;
using Entities.Concrete;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaleBreadController : ControllerBase
    {

        private IStaleBreadService _staleBreadService;


        public StaleBreadController(IStaleBreadService staleBreadService)
        {
            _staleBreadService = staleBreadService; ;
        }



        [HttpGet("GetStaleBreadListByDate")]
        public async Task<ActionResult> GetStaleBreadListByDate(DateTime date)
        {
            try
            {
                var result = await _staleBreadService.GetAllByDateAsync(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetStaleBreadDailyReport")]
        public async Task<ActionResult> GetStaleBreadDailyReport(DateTime date)
        {
            try
            {
                var result = await _staleBreadService.GetStaleBreadDailyReportAsync(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpGet("GetDoughFactoryProducts")]
        public async Task<ActionResult> GetDoughFactoryProducts(DateTime date)
        {
            try
            {
                var result = await _staleBreadService.GetDoughFactoryProductsAsync(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }



        [HttpPost("AddStaleBread")]
        public async Task<ActionResult> AddStaleBread(StaleBread staleBread)
        {

            try
            {
                if (await _staleBreadService.IsExistAsync(staleBread.DoughFactoryProductId, staleBread.Date))
                {
                    return BadRequest(Messages.OncePerDay);
                }
                if (staleBread == null || staleBread.Quantity < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }


               await _staleBreadService.AddAsync(staleBread);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteStaleBread")]
        public async Task<ActionResult> DeleteStaleBread(int id)
        {
            try
            {
               await _staleBreadService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateStaleBread")]
        public async Task<ActionResult> UpdateStaleBread(StaleBread staleBread)
        {
            try
            {
               await _staleBreadService.UpdateAsync(staleBread);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}