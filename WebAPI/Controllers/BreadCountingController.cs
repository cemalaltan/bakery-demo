using Business.Abstract;
using Business.Constants;
using Castle.Core.Internal;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
   // [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class BreadCountingController : ControllerBase
    {

        private IBreadCountingService _breadCountingService;


        public BreadCountingController(IBreadCountingService breadCountingService)
        {
            _breadCountingService = breadCountingService; ;
        }
        //[Authorize]
        //[Authorize(Roles = "admin")]
        [HttpGet("GetBreadCountingByDate")]
        public async Task<ActionResult> GetBreadCountingByDate(DateTime date)
        {
            try
            {
                var result =await _breadCountingService.GetBreadCountingByDateAsync(date);
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

        // needs some changes

        [HttpPost("AddBreadCounting")]
        public async Task<ActionResult> AddBreadCounting(BreadCounting breadCounting)
        {

            try
            {
                if ( await _breadCountingService.GetBreadCountingByDateAsync(breadCounting.Date) != null)
                {
                    return BadRequest(Messages.OncePerDay);
                }

                if (breadCounting == null || breadCounting.Quantity < 0)
                {
                    return BadRequest(Messages.WrongInput);
                }

               await _breadCountingService.AddAsync(breadCounting);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpDelete("DeleteBreadCountingById")]
        public async Task<ActionResult> DeleteBreadCountingById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(Messages.WrongInput);
            }
            try
            {
               await _breadCountingService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        [HttpPut("UpdateBreadCounting")]
        public async Task<ActionResult> UpdateBreadCounting(BreadCounting breadCounting)
        {
            if (breadCounting == null || breadCounting.Quantity < 0)
            {
                return BadRequest(Messages.WrongInput);
            }

            try
            {
                if (breadCounting.Quantity == 0)
                {
                 await   _breadCountingService.DeleteByIdAsync(breadCounting.Id);
                }
                else
                {
                    _breadCountingService.UpdateAsync(breadCounting);

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