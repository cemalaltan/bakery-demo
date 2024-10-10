using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            return  _userDal.GetClaims(user);
        }

        public async Task AddAsync(User user)
        {
            await _userDal.Add(user);
        }

        public async Task<User> GetByMailAsync(string email)
        {
            return await _userDal.Get(u => u.Email == email);
        }

        public async Task<List<User>> GetUsersAsync()
        {
            return await _userDal.GetAll();
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _userDal.DeleteById(id);
        }
    }
}
