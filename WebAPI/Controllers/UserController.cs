using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
                 _userService = userService;
        }


        [HttpDelete("DeleteById/{id}")]
        public async Task<ActionResult> DeleteById(int id)
        {
           await _userService.DeleteByIdAsync(id);
            return Ok();
        }


        [HttpGet("GetUsers")]
        public async Task<ActionResult> GetAll()

        {

            var users =  _userService.GetUsersAsync().Result
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.FirstName,
                Surname = u.LastName,
                UserName = u.Email,
                OperationClaimId = u.OperationClaimId,
            })
            .ToList();

            return Ok(users);

        }


    }
}
