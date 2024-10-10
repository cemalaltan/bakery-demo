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
        public async Task<ActionResult> GetAll()
        {
            var employees = await _employeeService.GetAllAsync();
            return Ok(employees);
        }

       
        [HttpGet("GetAllEmployeeById")]
        public async Task<ActionResult> GetById(int id)
        {
            var employee = await _employeeService.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        public async Task<ActionResult> Add(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("There is no data!");
            }
           await _employeeService.AddAsync(employee);
            return Ok();
        }

        [HttpPut("UpdateEmployee")]
        public async Task<ActionResult> Update(Employee employee)
        {
            if (employee == null)
            {
                return BadRequest("There is no data!");
            }
           await _employeeService.UpdateAsync(employee);
            return Ok();
        }
    }
}
