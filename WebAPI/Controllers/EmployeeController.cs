using Business.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

     
        [HttpGet("GetAllEmployees")]
        public ActionResult GetAll()
        {
            var employees = _employeeService.GetAll();
            return Ok(employees);
        }

       
        [HttpGet("GetAllEmployeeById")]
        public ActionResult GetById(int id)
        {
            var employee = _employeeService.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost("AddEmployee")]
        public ActionResult Add(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("There is no data!");
            }
            _employeeService.Add(employee);
            return Ok();
        }

        [HttpPut("UpdateEmployee")]
        public ActionResult Update(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("There is no data!");
            }
            _employeeService.Update(employee);
            return Ok();
        }
    }
}
