using Business.Abstract;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Constants;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Entities.DTOs;

namespace Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public List<OperationClaim> GetClaims(User user)
        {
            return _userDal.GetClaims(user);
        }

        public void Add(User user)
        {
            _userDal.Add(user); 
        }

        public void AddUser(UserForRegisterDto user)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            var newUser = new User
            {
                Email = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true,
                OperationClaimId= user.OperationClaimId

            };
            _userDal.Add(newUser);
        }

        public User GetByMail(string email)
        {
            return _userDal.Get(u => u.Email == email);
        }

        public List<User> GetUsers()
        {
            return _userDal.GetAll();
        }

        public void Update(User user)
        {
           var userE = _userDal.Get(u => u.Id == user.Id);
           userE.FirstName = user.FirstName;
           userE.LastName = user.LastName;
           userE.Email = user.Email;
           userE.Status = user.Status;
           
            
            _userDal.Update(user);
        }

        public void DeleteById(int id)
        {
            _userDal.DeleteById(id);
        }
    }
}
