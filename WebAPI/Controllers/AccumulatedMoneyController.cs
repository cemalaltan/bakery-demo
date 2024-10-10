using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccumulatedMoneyController : ControllerBase
    {

        private IAccumulatedMoneyService _accumulatedMoneyService;
        

        public AccumulatedMoneyController(IAccumulatedMoneyService accumulatedMoneyAmountService)
        {
            _accumulatedMoneyService = accumulatedMoneyAmountService; ;            
        }

        [HttpGet("GetAllAccumulatedMoney")]
        public async Task<ActionResult> GetAccumulatedMoney(int type)
        {
            var result = await _accumulatedMoneyService.GetAllByTypeAsync(type);
            return Ok(result);
        }

        [HttpGet("GetAccumulatedMoneyByDateRange")]
        public async Task<ActionResult> GetAccumulatedMoneyByDateRange(DateTime startDate, DateTime endDate, int type)
        {
            var result = await _accumulatedMoneyService.GetByDateRangeAndTypeAsync(startDate, endDate, type);
            return Ok(result);
        }

        [HttpGet("GetAccumulatedMoneyByDate")]
        public async Task<ActionResult> GetAccumulatedMoneyByDate(DateTime date, int type)
        {
            var result = await _accumulatedMoneyService.GetByDateAndTypeAsync(date, type);
            return Ok(result);
        }

        [HttpPost("AddAccumulatedMoney")]
        public async Task<ActionResult> AddAccumulatedMoney(AccumulatedMoney accumulatedMoneyAmount)
        {
            await _accumulatedMoneyService.AddAsync(accumulatedMoneyAmount);
            return Ok();
        }

        [HttpDelete("DeleteAccumulatedMoney")]
        public async Task<ActionResult>  DeleteAccumulatedMoney(AccumulatedMoney accumulatedMoneyAmount)
        {
            await _accumulatedMoneyService.DeleteAsync(accumulatedMoneyAmount);
            return Ok();
        }

        [HttpPut("UpdateAccumulatedMoney")]
        public async Task<ActionResult> UpdateAccumulatedMoney(AccumulatedMoney accumulatedMoneyAmount)
        {
            await _accumulatedMoneyService.UpdateAsync(accumulatedMoneyAmount);
            return Ok();
        }
    }
}