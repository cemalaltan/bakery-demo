

using Core.Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserService
    {
        Task<List<OperationClaim>> GetClaimsAsync(User user);
        Task AddAsync(User user);
        Task DeleteByIdAsync(int id);
        Task<User> GetByMailAsync(string email);
        Task<List<User>> GetUsersAsync();

    }
    }
