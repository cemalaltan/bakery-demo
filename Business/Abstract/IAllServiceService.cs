using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAllServiceService
    {
        Task<List<AllService>> GetAllAsync();
        Task AddAsync(AllService allService);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(AllService allService);
        Task UpdateAsync(AllService allService);
        Task<AllService> GetByIdAsync(int id);

    }

}
