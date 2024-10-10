using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalaryPaymentController : ControllerBase
    {
        private readonly ISalaryPaymentService _salaryPaymentService;

        public SalaryPaymentController(ISalaryPaymentService salaryPaymentService)
        {
            _salaryPaymentService = salaryPaymentService;
        }

        [HttpGet("GetAllSalaryPayments")]
        public async Task<ActionResult> GetAll()
        {
            var salaryPayments = await _salaryPaymentService.GetAllAsync();
            return Ok(salaryPayments);
        }

        [HttpGet("GetMonthlyReport")]
        public async Task<ActionResult> GetById(int year, int month)
        {
            var report = await _salaryPaymentService.SalaryPaymentReportByDateAsync(year, month);   
            return Ok(report);
        }

        [HttpPost("AddSalaryPayment")]
        public async Task<ActionResult> Add(SalaryPayment salaryPayment)
        {
            if (salaryPayment == null)
            {
                return BadRequest("There is no data!");
            }
            await _salaryPaymentService.AddAsync(salaryPayment);
            return Ok();
        }

        [HttpPut("UpdateSalaryPayment")]
        public async Task<ActionResult> Update(SalaryPayment salaryPayment)
        {
            if (salaryPayment == null)
            {
                return BadRequest("There is no data!");
            }
           await _salaryPaymentService.UpdateAsync(salaryPayment);
            return Ok();
        }

        [HttpDelete("DeleteSalaryPayment")]
        public async Task<ActionResult> DeleteById(int id)
        {
           await _salaryPaymentService.DeleteByIdAsync(id);
            return Ok();
        }
    }
}
