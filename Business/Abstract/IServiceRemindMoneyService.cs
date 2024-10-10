using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceRemindMoneyService
    {
        Task<List<ServiceRemindMoney>> GetAllAsync();
        Task AddAsync(ServiceRemindMoney serviceRemindMoney);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(ServiceRemindMoney serviceRemindMoney);
        Task UpdateAsync(ServiceRemindMoney serviceRemindMoney);
        Task<ServiceRemindMoney> GetByIdAsync(int id);

    }
}
