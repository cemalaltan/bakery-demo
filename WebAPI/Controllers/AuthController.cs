using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;


namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(string userName,string password )
        {
        
            var userToLogin = await _authService.LoginAsync(new UserForLoginDto { UserName= userName,Password= password });
            if (!userToLogin.Success)
            {
                return BadRequest(userToLogin.Message);
            }


            var result = await _authService.CreateAccessTokenAsync(userToLogin.Data);
            if (result.Success)
            {
                LoginResponseDto loginResponseDto = new LoginResponseDto {

                 Id=   userToLogin.Data.Id,
                Name = userToLogin.Data.FirstName,
                   OperationClaimId = userToLogin.Data.OperationClaimId,
                   Token = result.Data.Token};
                return Ok(loginResponseDto);

               
            }

            return BadRequest(result.Message);
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            var userExists = await _authService.UserExistsAsync(userForRegisterDto.UserName);
            if (!userExists.Success)
            {
                return BadRequest(userExists.Message);
            }

            var registerResult = await _authService.RegisterAsync(userForRegisterDto, userForRegisterDto.Password);
            var result = await _authService.CreateAccessTokenAsync(registerResult.Data);
            if (result.Success)
            {
                return Ok(result.Data);
            }

            return BadRequest(result.Message);
        }

    }
}
