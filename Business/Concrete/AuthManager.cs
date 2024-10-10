using Business.Abstract;
using Business.Constants;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.DTOs;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;
        private readonly ITokenHelper _tokenHelper;

        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }

        public async Task<IDataResult<User>> RegisterAsync(UserForRegisterDto userForRegisterDto, string password)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var user = new User
            {
                Email = userForRegisterDto.UserName,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                OperationClaimId = userForRegisterDto.OperationClaimId
            };

            await _userService.AddAsync(user); // Ensure this method is async in IUserService
            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public async Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto)
        {
            var userToCheck = await _userService.GetByMailAsync(userForLoginDto.UserName); // Ensure this method is async
            if (userToCheck == null || userToCheck.Status == false)
            {
                return new ErrorDataResult<User>(Messages.UserNotFound);
            }

            if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        public async Task<IResult> UserExistsAsync(string email)
        {
            if (await _userService.GetByMailAsync(email) != null) // Ensure this method is async
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }

        public async Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user)
        {
            var claims = await _userService.GetClaimsAsync(user); // Ensure this method is async
            var accessToken = _tokenHelper.CreateToken(user, claims);
            return new SuccessDataResult<AccessToken>(accessToken, Messages.AccessTokenCreated);
        }
    }
}
