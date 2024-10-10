using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CashCountingController : ControllerBase
    {

        private ICashCountingService _cashCountingService;


        public CashCountingController(ICashCountingService cashCountingService)
        {
            _cashCountingService = cashCountingService; ;
        }


        [HttpGet("GetCashCountingByDate")]
        public async Task<ActionResult> GetCashCountingByDate(DateTime date)
        {
            try
            {
                var result = await _cashCountingService.GetOneCashCountingByDateAsync(date);
                if (result != null)
                {
                    return Ok(result);
                }
                return NoContent();

            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPost("AddCashCounting")]
        public async Task<ActionResult> AddCashCounting(CashCounting cashCounting)
        {
            if (cashCounting == null || cashCounting.TotalMoney < 0 || cashCounting.RemainedMoney < 0)
            {
                return BadRequest(Messages.WrongInput);
            }


            try
            {
                if (_cashCountingService.GetOneCashCountingByDateAsync(cashCounting.Date) != null)
                {
                    return BadRequest(Messages.OncePerDay);
                }
               await _cashCountingService.AddAsync(cashCounting);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteCashCountingById")]
        public async Task<ActionResult> DeleteCashCountingById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
               await _cashCountingService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateCashCounting")]
        public async Task<ActionResult> UpdateCashCounting(CashCounting cashCounting)
        {
            if (cashCounting == null || cashCounting.TotalMoney < 0 || cashCounting.RemainedMoney < 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                if (cashCounting.TotalMoney == 0 && cashCounting.RemainedMoney == 0)
                {
                 await   _cashCountingService.DeleteByIdAsync(cashCounting.Id);
                }
                else
                {
                  await  _cashCountingService.UpdateAsync(cashCounting);
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