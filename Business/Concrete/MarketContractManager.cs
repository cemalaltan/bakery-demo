using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MarketContractManager : IMarketContractService
    {
        private readonly IMarketContractDal _marketContractDal;

        public MarketContractManager(IMarketContractDal marketContractDal)
        {
            _marketContractDal = marketContractDal;
        }

        public async Task AddAsync(MarketContract marketContract)
        {
            await _marketContractDal.Add(marketContract);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _marketContractDal.DeleteById(id);
        }

        public async Task DeleteAsync(MarketContract marketContract)
        {
            await _marketContractDal.Delete(marketContract);
        }

        public async Task<List<MarketContract>> GetAllAsync()
        {
            return await _marketContractDal.GetAll();
        }

        public async Task<MarketContract> GetByIdAsync(int id)
        {
            return await _marketContractDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(MarketContract marketContract)
        {
            await _marketContractDal.Update(marketContract);
        }

        public async Task<decimal> GetPriceByIdAsync(int id)
        {
            return (await _marketContractDal.Get(p => p.Id == id)).Price;
        }

        public async Task<int> GetIdByMarketIdAsync(int id)
        {
            return (await _marketContractDal.Get(p => p.MarketId == id)).Id;
        }

        public async Task<int> GetMarketIdByIdAsync(int id)
        {
            return (await _marketContractDal.Get(p => p.Id == id)).MarketId;
        }

        public async Task<List<Market>> GetMarketsNotHaveContractAsync()
        {
            return  _marketContractDal.GetMarketsNotHaveContract();
        }

        public async Task<List<MarketContractDto>> GetAllContractWithMarketsNameAsync()
        {
            return  _marketContractDal.GetAllContractWithMarketsName();
        }
    }
}
