using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDebtMarketService
    {
        Task<List<DebtMarket>> GetAllAsync();
        Task<List<DebtMarket>> GetDebtByMarketIdAsync(int marketId);
        Task<Dictionary<int, decimal>> GetTotalDebtsForMarketsAsync();
        Task AddAsync(DebtMarket debtMarket);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(DebtMarket debtMarket);
        Task UpdateAsync(DebtMarket debtMarket);
        Task<DebtMarket> GetByIdAsync(int id);
        Task<int> GetDebtIdByDateAndMarketIdAsync(DateTime date, int marketId);
        Task<bool> IsExistAsync(int id);

    }
}
