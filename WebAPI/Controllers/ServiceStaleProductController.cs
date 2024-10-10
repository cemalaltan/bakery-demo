using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceStaleProductController : ControllerBase
    {

        private IServiceStaleProductService _serviceStaleProductService;


        public ServiceStaleProductController(IServiceStaleProductService serviceStaleProductService)
        {
            _serviceStaleProductService = serviceStaleProductService; ;
        }

        [HttpGet("GetServiceStaleProductListByDateAndServiceTypeId")]
        public async Task<ActionResult> GetServiceStaleProductListByDateAndServiceTypeId(DateTime date, int serviceTypeId)
        {
            try
            {
                var result = await _serviceStaleProductService.GetAllByDateAsync(date, serviceTypeId);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        

        [HttpPost("AddServiceStaleProduct")]
        public async Task<ActionResult> AddServiceStaleProduct(ServiceStaleProduct serviceStaleProduct)
        {

            try
            {

                if (serviceStaleProduct == null || serviceStaleProduct.Quantity < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

               await _serviceStaleProductService.AddAsync(serviceStaleProduct);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteServiceStaleProduct")]
        public async Task<ActionResult> DeleteServiceStaleProduct(int id)
        {
            try
            {
               await _serviceStaleProductService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateServiceStaleProduct")]
        public async Task<ActionResult> UpdateServiceStaleProduct(ServiceStaleProduct serviceStaleProduct)
        {
            try
            {
               await _serviceStaleProductService.UpdateAsync(serviceStaleProduct);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}
