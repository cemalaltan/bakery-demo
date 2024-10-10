using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MoneyReceivedFromMarketManager : IMoneyReceivedFromMarketService
    {
        private readonly IMoneyReceivedFromMarketDal _moneyReceivedFromMarketDal;

        public MoneyReceivedFromMarketManager(IMoneyReceivedFromMarketDal moneyReceivedFromMarketDal)
        {
            _moneyReceivedFromMarketDal = moneyReceivedFromMarketDal;
        }

        public async Task AddAsync(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            await _moneyReceivedFromMarketDal.Add(moneyReceivedFromMarket);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _moneyReceivedFromMarketDal.DeleteById(id);
        }

        public async Task DeleteAsync(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            await _moneyReceivedFromMarketDal.Delete(moneyReceivedFromMarket);
        }

        public async Task<List<MoneyReceivedFromMarket>> GetAllAsync()
        {
            return await _moneyReceivedFromMarketDal.GetAll();
        }

        public async Task<MoneyReceivedFromMarket> GetByIdAsync(int id)
        {
            return await _moneyReceivedFromMarketDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(MoneyReceivedFromMarket moneyReceivedFromMarket)
        {
            await _moneyReceivedFromMarketDal.Update(moneyReceivedFromMarket);
        }

        public async Task<List<MoneyReceivedFromMarket>> GetByMarketIdAsync(int id)
        {
            return await _moneyReceivedFromMarketDal.GetAll(s => s.MarketId == id);
        }

        public async Task<List<MoneyReceivedFromMarket>> GetByDateAsync(DateTime date)
        {
            return await _moneyReceivedFromMarketDal.GetAll(d => d.Date.Date == date.Date);
        }

        public async Task<MoneyReceivedFromMarket> GetByMarketIdAndDateAsync(int id, DateTime date)
        {
            return await _moneyReceivedFromMarketDal.Get(s => s.MarketId == id && s.Date.Date == date.Date);
        }

        public async Task<bool> IsExistAsync(int marketId, DateTime date)
        {
            return  _moneyReceivedFromMarketDal.IsExist(marketId, date);
        }
    }
}
