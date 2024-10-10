using Entities.Concrete;

namespace Business.Abstract
{
    public interface IStaleBreadReceivedFromMarketService
    {
        Task<List<StaleBreadReceivedFromMarket>> GetAllAsync();
        Task<List<StaleBreadReceivedFromMarket>> GetByDateAsync(DateTime date);
        Task<StaleBreadReceivedFromMarket> GetByMarketIdAsync(int id, DateTime date);
        Task<int> GetStaleBreadCountByMarketIdAsync(int marketId, DateTime date);
        Task AddAsync(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket);
        Task DeleteByIdAsync(int id);
        Task DeleteByDateAndMarketIdAsync(DateTime date, int marketId);
        Task DeleteAsync(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket);
        Task UpdateAsync(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket);
        Task<StaleBreadReceivedFromMarket> GetByIdAsync(int id);
        Task<bool> IsExistAsync(int marketId, DateTime date);

    }
}
