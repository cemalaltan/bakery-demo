using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ServiceRemindMoneyManager : IServiceRemindMoneyService
    {
        private readonly IServiceRemindMoneyDal _serviceRemindMoneyDal;

        public ServiceRemindMoneyManager(IServiceRemindMoneyDal serviceRemindMoneyDal)
        {
            _serviceRemindMoneyDal = serviceRemindMoneyDal;
        }

        public async Task AddAsync(ServiceRemindMoney serviceRemindMoney)
        {
            await _serviceRemindMoneyDal.Add(serviceRemindMoney);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _serviceRemindMoneyDal.DeleteById(id);
        }

        public async Task DeleteAsync(ServiceRemindMoney serviceRemindMoney)
        {
            await _serviceRemindMoneyDal.Delete(serviceRemindMoney);
        }

        public async Task<List<ServiceRemindMoney>> GetAllAsync()
        {
            return await _serviceRemindMoneyDal.GetAll();
        }

        public async Task<ServiceRemindMoney> GetByIdAsync(int id)
        {
            return await _serviceRemindMoneyDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(ServiceRemindMoney serviceRemindMoney)
        {
            await _serviceRemindMoneyDal.Update(serviceRemindMoney);
        }
    }
}
