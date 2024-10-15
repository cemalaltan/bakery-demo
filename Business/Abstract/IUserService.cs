

using Core.Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        List<OperationClaim> GetClaims(User user);
        void Add(User user);
        void AddUser(UserForRegisterDto user);
        void DeleteById(int id);
        User GetByMail(string email);
        List<User> GetUsers();
        void Update(User user);
    }
}
