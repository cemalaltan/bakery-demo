using Business.Abstract;
using Entities.Concrete;
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
        public ActionResult GetAccumulatedMoney(int type)
        {
            var result = _accumulatedMoneyService.GetAllByType(type);
            return Ok(result);
        }

        [HttpGet("GetAccumulatedMoneyByDateRange")]
        public ActionResult GetAccumulatedMoneyByDateRange(DateTime startDate, DateTime endDate, int type)
        {
            var result = _accumulatedMoneyService.GetByDateRangeAndType(startDate, endDate, type);
            return Ok(result);
        }

        [HttpGet("GetAccumulatedMoneyByDate")]
        public ActionResult GetAccumulatedMoneyByDate(DateTime date, int type)
        {
            var result = _accumulatedMoneyService.GetByDateAndType(date, type);
            return Ok(result);
        }

        [HttpPost("AddAccumulatedMoney")]
        public ActionResult AddAccumulatedMoney(AccumulatedMoney accumulatedMoneyAmount)
        {
            _accumulatedMoneyService.Add(accumulatedMoneyAmount);
            return Ok();
        }

        [HttpDelete("DeleteAccumulatedMoney")]
        public ActionResult DeleteAccumulatedMoney(AccumulatedMoney accumulatedMoneyAmount)
        {
            _accumulatedMoneyService.Delete(accumulatedMoneyAmount);
            return Ok();
        }

        [HttpPut("UpdateAccumulatedMoney")]
        public ActionResult UpdateAccumulatedMoney(AccumulatedMoney accumulatedMoneyAmount)
        {
            _accumulatedMoneyService.Update(accumulatedMoneyAmount);
            return Ok();
        }
    }
}