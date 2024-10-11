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
        public ActionResult GetAll()
        {
            var salaryPayments = _salaryPaymentService.GetAll();
            return Ok(salaryPayments);
        }

        [HttpGet("GetMonthlyReport")]
        public ActionResult GetById(int year, int month)
        {
            var report = _salaryPaymentService.SalaryPaymentReportByDate(year, month);   
            return Ok(report);
        }

        [HttpPost("AddSalaryPayment")]
        public ActionResult Add(SalaryPayment salaryPayment)
        {
            if (salaryPayment == null)
            {
                return BadRequest("There is no data!");
            }
            _salaryPaymentService.Add(salaryPayment);
            return Ok();
        }

        [HttpPut("UpdateSalaryPayment")]
        public ActionResult Update(SalaryPayment salaryPayment)
        {
            if (salaryPayment == null)
            {
                return BadRequest("There is no data!");
            }
            _salaryPaymentService.Update(salaryPayment);
            return Ok();
        }

        [HttpDelete("DeleteSalaryPayment")]
        public ActionResult DeleteById(int id)
        {
            _salaryPaymentService.DeleteById(id);
            return Ok();
        }
    }
}
