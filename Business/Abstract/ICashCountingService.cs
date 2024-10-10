using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICashCountingService
    {
        Task<List<CashCounting>> GetAllAsync();
        Task<List<CashCounting>> GetCashCountingByDateAsync(DateTime date);
        Task<CashCounting> GetOneCashCountingByDateAsync(DateTime date);
        Task AddAsync(CashCounting cashCounting);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(CashCounting cashCounting);
        Task UpdateAsync(CashCounting cashCounting);
        Task<CashCounting> GetByIdAsync(int id);

    }
}
