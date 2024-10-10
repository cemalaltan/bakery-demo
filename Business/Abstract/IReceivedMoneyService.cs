using Entities.Concrete;

namespace Business.Abstract
{
    public interface IReceivedMoneyService
    {
        Task<List<ReceivedMoney>> GetAllAsync();
        Task AddAsync(ReceivedMoney receivedMoney);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(ReceivedMoney receivedMoney);
        Task UpdateAsync(ReceivedMoney receivedMoney);
        Task<ReceivedMoney> GetByIdAsync(int id);

    }
}
