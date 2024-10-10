using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AccumulatedMoneyDeliveryManager : IAccumulatedMoneyDeliveryService
    {
        private readonly IAccumulatedMoneyDeliveryDal _deliveryDal;

        public AccumulatedMoneyDeliveryManager(IAccumulatedMoneyDeliveryDal deliveryDal)
        {
            _deliveryDal = deliveryDal;
        }

        public async Task AddAsync(AccumulatedMoneyDelivery delivery)
        {
            await Task.Run(() => _deliveryDal.Add(delivery));
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _deliveryDal.DeleteById(id));
        }

        public async Task DeleteAsync(AccumulatedMoneyDelivery delivery)
        {
            await Task.Run(() => _deliveryDal.Delete(delivery));
        }

        public async Task<List<AccumulatedMoneyDelivery>> GetAllAsync()
        {
            return await Task.Run(() => _deliveryDal.GetAll());
        }

        public async Task<AccumulatedMoneyDelivery> GetByIdAsync(int id)
        {
            return await Task.Run(() => _deliveryDal.Get(d => d.Id == id));
        }

        public async Task UpdateAsync(AccumulatedMoneyDelivery delivery)
        {
            await Task.Run(() => _deliveryDal.Update(delivery));
        }

        public async Task<List<AccumulatedMoneyDelivery>> GetBetweenDatesAsync(DateTime startDate, DateTime endDate)
        {
            return await Task.Run(() =>
                _deliveryDal.GetAll(delivery => delivery.CreatedAt >= startDate && delivery.CreatedAt <= endDate.AddDays(1)));
        }

        public async Task<AccumulatedMoneyDelivery?> GetLastDeliveryAsync(int type)
        {
            return await Task.Run(() => _deliveryDal.GetLatestDelivery(type));
        }
    }
}
