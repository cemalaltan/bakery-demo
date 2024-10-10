using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DebtMarketManager : IDebtMarketService
    {
        private readonly IDebtMarketDal _debtMarketDal;

        public DebtMarketManager(IDebtMarketDal debtMarketDal)
        {
            _debtMarketDal = debtMarketDal;
        }

        public async Task AddAsync(DebtMarket debtMarket)
        {
            await _debtMarketDal.Add(debtMarket);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _debtMarketDal.DeleteById(id);
        }

        public async Task DeleteAsync(DebtMarket debtMarket)
        {
            await _debtMarketDal.Delete(debtMarket);
        }

        public async Task<List<DebtMarket>> GetAllAsync()
        {
            return await _debtMarketDal.GetAll();
        }

        public async Task<DebtMarket> GetByIdAsync(int id)
        {
            return await _debtMarketDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(DebtMarket debtMarket)
        {
            await _debtMarketDal.Update(debtMarket);
        }

        public async Task<int> GetDebtIdByDateAndMarketIdAsync(DateTime date, int marketId)
        {
            DebtMarket debtMarket = await _debtMarketDal.Get(d => d.Date.Date == date.Date && d.MarketId == marketId && d.Amount > 0);
            return debtMarket == null ? 0 : debtMarket.Id;
        }

        public async Task<bool> IsExistAsync(int id)
        {
            return _debtMarketDal.IsExist(id);
        }

        public async Task<List<DebtMarket>> GetDebtByMarketIdAsync(int marketId)
        {
            return await _debtMarketDal.GetAll(d => d.MarketId == marketId);
        }

        public async Task<Dictionary<int, decimal>> GetTotalDebtsForMarketsAsync()
        {
            return  _debtMarketDal.GetTotalDebtsForMarkets();
        }
    }
}
