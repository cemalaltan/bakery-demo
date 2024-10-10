using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMarketService
    {
        Task<List<Market>> GetAllAsync();
        Task<List<Market>> GetAllActiveAsync();
        Task AddAsync(Market market);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(Market market);
        Task UpdateAsync(Market market);
        Task<Market> GetByIdAsync(int id);
        Task<string> GetNameByIdAsync(int id);

    }
}
