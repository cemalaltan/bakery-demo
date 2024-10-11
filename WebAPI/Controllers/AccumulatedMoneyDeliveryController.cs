using Business.Abstract;
using Castle.DynamicProxy.Generators;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccumulatedMoneyDeliveryController : ControllerBase
    {

        private IAccumulatedMoneyDeliveryService _accumulatedMoneyDeliveryService;
        private IAccumulatedMoneyService _accumulatedMoneyService;
  
        public AccumulatedMoneyDeliveryController(IAccumulatedMoneyDeliveryService deliveryService, IAccumulatedMoneyService accumulatedMoneyService)
        {
            _accumulatedMoneyDeliveryService = deliveryService;
            _accumulatedMoneyService = accumulatedMoneyService;
        }

        

        [HttpGet("GetAccumulatedMoney")]
        public ActionResult GetAccumulatedMoney(int type)
        {

            decimal total = getAccumulatedTotalMoneyByType(type);
            return Ok(total);
        }

        [HttpGet("GetAccumulatedMoneyDeliveryByDateRange")]
        public ActionResult GetAccumulatedMoneyDeliveryByDateRange(DateTime startDate, DateTime endDate)
        {
            var result = _accumulatedMoneyDeliveryService.GetBetweenDates(startDate,endDate);
            return Ok(result);
        }
        
        [HttpGet("GetAllAccumulatedMoneyDelivery")]
        public ActionResult GetAllAccumulatedMoneyDelivery()
        {
            var result = _accumulatedMoneyDeliveryService.GetAll();
            return Ok(result);
        }

        [HttpGet("GetAccumulatedMoneyDeliveryById")]
        public ActionResult GetAccumulatedMoneyDeliveryById(int id)
        {
            var result = _accumulatedMoneyDeliveryService.GetById(id);
            return Ok(result);
        }

        [HttpPost("AddAccumulatedMoneyDelivery")]
        public ActionResult AddAccumulatedMoneyDelivery(AccumulatedMoneyDelivery accumulatedMoneyDelivery)
        {
            decimal total = getAccumulatedTotalMoneyByType(accumulatedMoneyDelivery.Type);
            if (accumulatedMoneyDelivery.AccumulatedAmount > total || accumulatedMoneyDelivery.AccumulatedAmount < 0)
            {
                return BadRequest("Accumulated amount can not be greater than total accumulated money");
            }

            decimal remaining = total + accumulatedMoneyDelivery.AccumulatedAmount - accumulatedMoneyDelivery.Amount;
            accumulatedMoneyDelivery.AccumulatedAmount = remaining;

            _accumulatedMoneyDeliveryService.Add(accumulatedMoneyDelivery);
            return Ok();
        }

        [HttpDelete("DeleteAccumulatedMoneyDelivery")]
        public ActionResult DeleteAccumulatedMoneyDelivery(AccumulatedMoneyDelivery accumulatedMoneyDelivery)
        {
            _accumulatedMoneyDeliveryService.Delete(accumulatedMoneyDelivery);
            return Ok();
        }

        [HttpPut("UpdateAccumulatedMoneyDelivery")]
        public ActionResult UpdateAccumulatedMoneyDelivery(AccumulatedMoneyDelivery accumulatedMoneyDelivery)
        {
            AccumulatedMoneyDelivery originalData = _accumulatedMoneyDeliveryService.GetById(accumulatedMoneyDelivery.Id);
            var total = originalData.AccumulatedAmount + originalData.Amount;
            if (accumulatedMoneyDelivery.AccumulatedAmount > total || accumulatedMoneyDelivery.AccumulatedAmount < 0)
            {
                return BadRequest("Accumulated amount can not be greater than total accumulated money");
            }
            decimal remaining = total  - accumulatedMoneyDelivery.Amount;
            accumulatedMoneyDelivery.AccumulatedAmount = remaining;
            _accumulatedMoneyDeliveryService.Update(accumulatedMoneyDelivery);
            return Ok();
        }

        decimal getAccumulatedTotalMoneyByType(int type)
        {
            var lastDelivery = _accumulatedMoneyDeliveryService.GetLastDelivery(type);
            DateTime date = lastDelivery == null ? new DateTime(2024, 1, 1) : lastDelivery.CreatedAt;

            var accumulatedMoney = _accumulatedMoneyService.GetTotalAccumulatedMoneyByDateAndType(date, type);
            var total = accumulatedMoney + (lastDelivery?.AccumulatedAmount ?? 0);
            return total;
        }

    }
}