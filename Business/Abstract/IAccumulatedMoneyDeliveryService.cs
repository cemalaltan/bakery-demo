using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAccumulatedMoneyDeliveryService
    {


        Task<List<AccumulatedMoneyDelivery>> GetAllAsync();
        Task<List<AccumulatedMoneyDelivery>> GetBetweenDatesAsync(DateTime startDate, DateTime endDate);
        Task<AccumulatedMoneyDelivery?> GetLastDeliveryAsync(int type);
        Task AddAsync(AccumulatedMoneyDelivery delivery);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(AccumulatedMoneyDelivery delivery);
        Task UpdateAsync(AccumulatedMoneyDelivery delivery);
        Task<AccumulatedMoneyDelivery> GetByIdAsync(int id);

    }
}
