using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvanceController : ControllerBase
    {
        private readonly IAdvanceService _advanceService;

        public AdvanceController(IAdvanceService advanceService)
        {
            _advanceService = advanceService;
        }

        [HttpGet("GetEmployeeAdvancesByDate")]
        public async Task<ActionResult> GetById(int id, int year, int month)
        {
            var advance = await _advanceService.GetEmployeeAdvancesByDateAsync(id, year, month);
            if (advance == null)
            {
                return NotFound();
            }
            return Ok(advance);
        }

        [HttpPost("AddAdvance")]
        public async Task<ActionResult> Add(Advance advance)
        {
            try {
                if (advance == null)
                {
                    return BadRequest("There is no data!");
                }
               await _advanceService.AddAsync(advance);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       
        }

        [HttpPut("UpdateAdvance")]
        public async Task<ActionResult> Update(Advance advance)
        {
            if (advance == null)
            {   
                return BadRequest("There is no data!");
            }
           await _advanceService.UpdateAsync(advance);
            return Ok();
        }

        [HttpDelete("DeleteAdvance")]
        public async Task<ActionResult> DeleteById(int id)
        {
           await _advanceService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
