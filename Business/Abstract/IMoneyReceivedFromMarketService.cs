using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMoneyReceivedFromMarketService
    {
        Task<List<MoneyReceivedFromMarket>> GetAllAsync();
        Task<List<MoneyReceivedFromMarket>> GetByMarketIdAsync(int id);
        Task<MoneyReceivedFromMarket> GetByMarketIdAndDateAsync(int id, DateTime date);
        Task<List<MoneyReceivedFromMarket>> GetByDateAsync(DateTime date);
        Task AddAsync(MoneyReceivedFromMarket moneyReceivedFromMarket);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(MoneyReceivedFromMarket moneyReceivedFromMarket);
        Task UpdateAsync(MoneyReceivedFromMarket moneyReceivedFromMarket);
        Task<MoneyReceivedFromMarket> GetByIdAsync(int id);
        Task<bool> IsExistAsync(int marketId, DateTime date);

    }
}
