using Business.Abstract;
using Entities.Concrete;

using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketContractController : ControllerBase
    {

        private IMarketContractService _marketContractService;
        

        public MarketContractController(IMarketContractService marketContractService)
        {
            _marketContractService = marketContractService;             
        }

        

        [HttpGet("GetAllMarketContract")]
        public async Task<ActionResult> GetMarketContract()
        {
            var result = await _marketContractService.GetAllContractWithMarketsNameAsync();
            return Ok(result);
        }

        [HttpGet("GetMarketsNotHaveContract")]
        public async Task<ActionResult> GetMarketsNotHaveContract()
        {
            var result = await _marketContractService.GetMarketsNotHaveContractAsync();
            return Ok(result);
        }

        [HttpGet("GetByIdMarketContract")]
        public async Task<ActionResult> GetByIdMarketContract(int id)
        {
            var result = await _marketContractService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("AddMarketContract")]
        public async Task<ActionResult> AddMarketContract(MarketContract marketContract)
        {
            await _marketContractService.AddAsync(marketContract);
            return Ok();
        }

        [HttpDelete("DeleteMarketContractById")]
        public async Task<ActionResult> DeleteMarketContractById(int  id)
        {
           await _marketContractService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPut("UpdateMarketContract")]
        public async Task<ActionResult> UpdateMarketContract(MarketContract marketContract)
        {
           await _marketContractService.UpdateAsync(marketContract);
            return Ok();
        }
    }
}