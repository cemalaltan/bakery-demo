using Business.Abstract;
using Core.Entities.Concrete;
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
        
        [HttpGet("GetUsers")]
        public IActionResult GetAll()
        {
            var users = _userService.GetUsers()
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.FirstName,
                Surname = u.LastName,
                UserName = u.Email,
                OperationClaimId = u.OperationClaimId,
                Status = u.Status,  
                
            })
            .ToList();

            return Ok(users);
        }
        
        [HttpPut("UpdateUser")]
        public IActionResult UpdateUser(User user)
        {
            _userService.Update(user);
            return Ok();
        }
        
        [HttpPost("AddUser")]
        public IActionResult AddUser(UserForRegisterDto user)
        {
            _userService.AddUser(user);
            return Ok();
        }
        
        [HttpPut("ChangePassword")]
        public IActionResult ChangePassword(int id , String password)
        {
           // _userService.Update(user);
            return Ok();
        }

        

    }
}
