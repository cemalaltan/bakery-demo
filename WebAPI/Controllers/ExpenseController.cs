using Business.Abstract;
using Business.Constants;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {

        private IExpenseService _expenseService;


        public ExpenseController(IExpenseService expenseService)
        {
            _expenseService = expenseService; ;
        }

        [HttpGet("GetExpensesByDate")]
        public async Task<ActionResult> GetExpense(DateTime date)
        {
            try
            {
                if (date.Date > DateTime.Now.Date)
                {
                    return BadRequest("Invalid date!");
                }
                Console.WriteLine(date.ToString());
                var result =await _expenseService.GetExpensesByDateAsync(date);
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpPost("AddExpense")]
        public async Task<ActionResult> AddExpense(Expense expense)
        {
            try
            {
                if (expense == null)
                {
                    return BadRequest("There is no data!");
                }
              await  _expenseService.AddAsync(expense);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpDelete("DeleteExpense")]
        public async Task<ActionResult> DeleteExpense(int  id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(Messages.WrongInput);
                }
               await _expenseService.DeleteByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }

        [HttpPut("UpdateExpense")]
        public async Task<ActionResult> UpdateExpense(Expense expense)
        {
            try
            {
                if (expense == null)
                {
                    return BadRequest("There is no data!");
                }
               await _expenseService.UpdateAsync(expense);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500, "Daha sonra tekrar deneyin...");
            }

        }
    }
}