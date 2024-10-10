using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IMarketContractService
    {
        Task<List<MarketContract>> GetAllAsync();
        Task<List<MarketContractDto>> GetAllContractWithMarketsNameAsync();
        Task<List<Market>> GetMarketsNotHaveContractAsync();
        Task AddAsync(MarketContract marketContract);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(MarketContract marketContract);
        Task UpdateAsync(MarketContract marketContract);
        Task<MarketContract> GetByIdAsync(int id);
        Task<int> GetIdByMarketIdAsync(int id);
        Task<int> GetMarketIdByIdAsync(int id);
        Task<decimal> GetPriceByIdAsync(int id);

    }
}
