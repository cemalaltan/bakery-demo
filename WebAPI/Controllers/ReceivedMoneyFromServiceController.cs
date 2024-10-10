using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceivedMoneyFromServiceController : ControllerBase
    {

        private IReceivedMoneyFromServiceService _receivedMoneyFromServiceService;
        public ReceivedMoneyFromServiceController(IReceivedMoneyFromServiceService receivedMoneyFromServiceService)
        {
            _receivedMoneyFromServiceService = receivedMoneyFromServiceService;
        }

        [HttpGet("GetAllReceivedMoneyFromService")]
        public async Task<ActionResult> GetAllReceivedMoneyFromService()
        {
            var result = await _receivedMoneyFromServiceService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetReceivedMoneyFromServiceByDateAndServiceType")]
        public async Task<ActionResult> GetAllReceivedMoneyFromServiceByDate(DateTime date,int serviceType)
        {
            var result = await _receivedMoneyFromServiceService.GetByDateAsync(date,serviceType);
            if (result != null)
            {
                return Ok(result);

            }
            return NoContent();
        }


        [HttpPost("AddReceivedMoneyFromService")]
        public async Task<ActionResult> AddReceivedMoneyFromService(ReceivedMoneyFromService receivedMoneyFromService)
        {
            if (receivedMoneyFromService == null)
            {
                return BadRequest("There is no data!");
            }
            var isAdded = await _receivedMoneyFromServiceService.GetByDateAsync(receivedMoneyFromService.Date, receivedMoneyFromService.ServiceTypeId);
            if (isAdded != null)
            {
                return BadRequest("Already Added!");
            }

           await _receivedMoneyFromServiceService.AddAsync(receivedMoneyFromService);
            return Ok();
        }

        [HttpDelete("DeleteReceivedMoneyFromServiceById")]
        public async Task<ActionResult> DeleteReceivedMoneyFromService(int id)
        {
            if (id == 0)
            {
                return BadRequest("There is no data!");
            }
           await _receivedMoneyFromServiceService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPut("UpdateReceivedMoneyFromService")]
        public async Task<ActionResult> UpdateReceivedMoneyFromService(ReceivedMoneyFromService receivedMoneyFromService)
        {
            if (receivedMoneyFromService == null)
            {
                return BadRequest("There is no data!");
            }
          await  _receivedMoneyFromServiceService.UpdateAsync(receivedMoneyFromService);
            return Ok();
        }
    }
}
