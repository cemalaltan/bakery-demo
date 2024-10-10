using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ReceivedMoneyFromServiceManager : IReceivedMoneyFromServiceService
    {
        private readonly IReceivedMoneyFromServiceDal _receivedMoneyFromServiceDal;

        public ReceivedMoneyFromServiceManager(IReceivedMoneyFromServiceDal receivedMoneyFromServiceDal)
        {
            _receivedMoneyFromServiceDal = receivedMoneyFromServiceDal;
        }

        public async Task AddAsync(ReceivedMoneyFromService receivedMoneyFromService)
        {
            await _receivedMoneyFromServiceDal.Add(receivedMoneyFromService);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _receivedMoneyFromServiceDal.DeleteById(id);
        }

        public async Task DeleteAsync(ReceivedMoneyFromService receivedMoneyFromService)
        {
            await _receivedMoneyFromServiceDal.Delete(receivedMoneyFromService);
        }

        public async Task<List<ReceivedMoneyFromService>> GetAllAsync()
        {
            return await _receivedMoneyFromServiceDal.GetAll();
        }

        public async Task<ReceivedMoneyFromService> GetByDateAsync(DateTime date, int serviceType)
        {
            return await _receivedMoneyFromServiceDal.Get(d => d.Date.Date == date.Date && d.ServiceTypeId == serviceType);
        }

        public async Task UpdateAsync(ReceivedMoneyFromService receivedMoneyFromService)
        {
            await _receivedMoneyFromServiceDal.Update(receivedMoneyFromService);
        }
    }
}
