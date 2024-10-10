using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAccumulatedMoneyService
    {
        Task<List<AccumulatedMoney>> GetAllByTypeAsync(int type);
        Task<List<AccumulatedMoney>> GetByDateRangeAndTypeAsync(DateTime startDate, DateTime endDate, int type);
        Task AddAsync(AccumulatedMoney accumulatedMoney);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(AccumulatedMoney accumulatedMoney);
        Task UpdateAsync(AccumulatedMoney accumulatedMoney);
        Task<AccumulatedMoney> GetByIdAsync(int id);
        Task<AccumulatedMoney> GetByDateAndTypeAsync(DateTime date, int type);
        Task<decimal> GetTotalAccumulatedMoneyByDateAndTypeAsync(DateTime date, int type);

    }
}
