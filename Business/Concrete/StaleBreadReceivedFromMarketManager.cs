using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StaleBreadReceivedFromMarketManager : IStaleBreadReceivedFromMarketService
    {
        private readonly IStaleBreadReceivedFromMarketDal _staleBreadReceivedFromMarketDal;

        public StaleBreadReceivedFromMarketManager(IStaleBreadReceivedFromMarketDal staleBreadReceivedFromMarketDal)
        {
            _staleBreadReceivedFromMarketDal = staleBreadReceivedFromMarketDal;
        }

        public async Task AddAsync(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {
            await _staleBreadReceivedFromMarketDal.Add(staleBreadReceivedFromMarket);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _staleBreadReceivedFromMarketDal.DeleteById(id);
        }

        public async Task DeleteAsync(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {
            await _staleBreadReceivedFromMarketDal.Delete(staleBreadReceivedFromMarket);
        }

        public async Task<List<StaleBreadReceivedFromMarket>> GetAllAsync()
        {
            return await _staleBreadReceivedFromMarketDal.GetAll();
        }

        public async Task<StaleBreadReceivedFromMarket> GetByIdAsync(int id)
        {
            return await _staleBreadReceivedFromMarketDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(StaleBreadReceivedFromMarket staleBreadReceivedFromMarket)
        {
            await _staleBreadReceivedFromMarketDal.Update(staleBreadReceivedFromMarket);
        }

        public async Task<StaleBreadReceivedFromMarket> GetByMarketIdAsync(int id, DateTime date)
        {
            return await _staleBreadReceivedFromMarketDal.Get(s => s.MarketId == id && s.Date.Date == date.Date);
        }

        public async Task DeleteByDateAndMarketIdAsync(DateTime date, int marketId)
        {
             _staleBreadReceivedFromMarketDal.DeleteByDateAndMarketId(date, marketId);
        }

        public async Task<int> GetStaleBreadCountByMarketIdAsync(int marketId, DateTime date)
        {
            StaleBreadReceivedFromMarket staleBreadReceivedFromMarket = await _staleBreadReceivedFromMarketDal.Get(s => s.MarketId == marketId && s.Date.Date == date.Date);
            return staleBreadReceivedFromMarket == null ? 0 : staleBreadReceivedFromMarket.Quantity;
        }

        public async Task<List<StaleBreadReceivedFromMarket>> GetByDateAsync(DateTime date)
        {
            return await _staleBreadReceivedFromMarketDal.GetAll(s => s.Date.Date == date.Date);
        }

        public async Task<bool> IsExistAsync(int marketId, DateTime date)
        {
            return  await _staleBreadReceivedFromMarketDal.IsExist(marketId, date);
        }
    }
}
