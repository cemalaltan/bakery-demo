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
        public async Task<ActionResult> GetAccumulatedMoney(int type)
        {

            decimal total = await getAccumulatedTotalMoneyByType(type);
            return Ok(total);
        }

        [HttpGet("GetAccumulatedMoneyDeliveryByDateRange")]
        public async Task<ActionResult> GetAccumulatedMoneyDeliveryByDateRange(DateTime startDate, DateTime endDate)
        {
            var result = await _accumulatedMoneyDeliveryService.GetBetweenDatesAsync(startDate,endDate);
            return Ok(result);
        }
        
        [HttpGet("GetAllAccumulatedMoneyDelivery")]
        public async Task<ActionResult> GetAllAccumulatedMoneyDelivery()
        {
            var result = await _accumulatedMoneyDeliveryService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetAccumulatedMoneyDeliveryById")]
        public async Task<ActionResult> GetAccumulatedMoneyDeliveryById(int id)
        {
            var result = await _accumulatedMoneyDeliveryService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("AddAccumulatedMoneyDelivery")]
        public async Task<ActionResult> AddAccumulatedMoneyDelivery(AccumulatedMoneyDelivery accumulatedMoneyDelivery)
        {
            decimal total = await  getAccumulatedTotalMoneyByType(accumulatedMoneyDelivery.Type);
            if (accumulatedMoneyDelivery.AccumulatedAmount > total || accumulatedMoneyDelivery.AccumulatedAmount < 0)
            {
                return BadRequest("Accumulated amount can not be greater than total accumulated money");
            }

            decimal remaining = total + accumulatedMoneyDelivery.AccumulatedAmount - accumulatedMoneyDelivery.Amount;
            accumulatedMoneyDelivery.AccumulatedAmount = remaining;

            await _accumulatedMoneyDeliveryService.AddAsync(accumulatedMoneyDelivery);
            return Ok();
        }

        [HttpDelete("DeleteAccumulatedMoneyDelivery")]
        public async Task<ActionResult> DeleteAccumulatedMoneyDelivery(AccumulatedMoneyDelivery accumulatedMoneyDelivery)
        {
           await _accumulatedMoneyDeliveryService.DeleteAsync(accumulatedMoneyDelivery);
            return Ok();
        }

        [HttpPut("UpdateAccumulatedMoneyDelivery")]
        public async Task<ActionResult> UpdateAccumulatedMoneyDelivery(AccumulatedMoneyDelivery accumulatedMoneyDelivery)
        {
            AccumulatedMoneyDelivery originalData = await _accumulatedMoneyDeliveryService.GetByIdAsync(accumulatedMoneyDelivery.Id);
            var total = originalData.AccumulatedAmount + originalData.Amount;
            if (accumulatedMoneyDelivery.AccumulatedAmount > total || accumulatedMoneyDelivery.AccumulatedAmount < 0)
            {
                return BadRequest("Accumulated amount can not be greater than total accumulated money");
            }
            decimal remaining = total  - accumulatedMoneyDelivery.Amount;
            accumulatedMoneyDelivery.AccumulatedAmount = remaining;
            _accumulatedMoneyDeliveryService.UpdateAsync(accumulatedMoneyDelivery);
            return Ok();
        }

        async Task<decimal> getAccumulatedTotalMoneyByType(int type)
        {
            var lastDelivery = await _accumulatedMoneyDeliveryService.GetLastDeliveryAsync(type);
            DateTime date = lastDelivery == null ? new DateTime(2024, 1, 1) : lastDelivery.CreatedAt;

            var accumulatedMoney = await _accumulatedMoneyService.GetTotalAccumulatedMoneyByDateAndTypeAsync(date, type);
            var total = accumulatedMoney + (lastDelivery?.AccumulatedAmount ?? 0);
            return total;
        }

    }
}