using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProductController : ControllerBase
    {

        private IServiceProductService _serviceProductService;
        

        public ServiceProductController(IServiceProductService serviceProductService)
        {
            _serviceProductService = serviceProductService; ;            
        }

        

        [HttpGet("GetAllServiceProduct")]
        public async Task<ActionResult> GetServiceProduct()
        {
            var result = await _serviceProductService.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("GetByIdServiceProduct")]
        public async Task<ActionResult> GetByIdServiceProduct(int id)
        {
            var result = await _serviceProductService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost("AddServiceProduct")]
        public async Task<ActionResult> AddServiceProduct(ServiceProduct serviceProduct)
        {
           await  _serviceProductService.AddAsync(serviceProduct);
            return Ok();
        }

        [HttpDelete("DeleteServiceProduct")]
        public async Task<ActionResult> DeleteServiceProduct(int id)
        {
           await _serviceProductService.DeleteByIdAsync(id);
            return Ok();
        }

        [HttpPut("UpdateServiceProduct")]
        public async Task<ActionResult> UpdateServiceProduct(ServiceProduct serviceProduct)
        {
           await _serviceProductService.UpdateAsync(serviceProduct);
            return Ok();
        }
    }
}