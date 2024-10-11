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
        public ActionResult GetById(int id, int year, int month)
        {
            var advance = _advanceService.GetEmployeeAdvancesByDate(id, year, month);
            if (advance == null)
            {
                return NotFound();
            }
            return Ok(advance);
        }

        [HttpPost("AddAdvance")]
        public ActionResult Add(Advance advance)
        {
            try {
                if (advance == null)
                {
                    return BadRequest("There is no data!");
                }
                _advanceService.Add(advance);
                return Ok();
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
       
        }

        [HttpPut("UpdateAdvance")]
        public ActionResult Update(Advance advance)
        {
            if (advance == null)
            {   
                return BadRequest("There is no data!");
            }
            _advanceService.Update(advance);
            return Ok();
        }

        [HttpDelete("DeleteAdvance")]
        public ActionResult DeleteById(int id)
        {
            _advanceService.DeleteById(id);
            return Ok();
        }
    }
}
