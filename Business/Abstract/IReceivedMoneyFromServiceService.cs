using Entities.Concrete;

namespace Business.Abstract
{
    public interface IReceivedMoneyFromServiceService
    {
        Task<List<ReceivedMoneyFromService>> GetAllAsync();
        Task AddAsync(ReceivedMoneyFromService allService);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(ReceivedMoneyFromService allService);
        Task UpdateAsync(ReceivedMoneyFromService allService);
        Task<ReceivedMoneyFromService> GetByDateAsync(DateTime date, int serviceType);

    }
}
