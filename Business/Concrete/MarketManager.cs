using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MarketManager : IMarketService
    {
        private readonly IMarketDal _marketDal;

        public MarketManager(IMarketDal marketDal)
        {
            _marketDal = marketDal;
        }

        public async Task AddAsync(Market market)
        {
            await _marketDal.Add(market);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _marketDal.DeleteById(id);
        }

        public async Task DeleteAsync(Market market)
        {
            await _marketDal.Delete(market);
        }

        public async Task<List<Market>> GetAllActiveAsync()
        {
            return await _marketDal.GetAll(m => m.IsActive);
        }

        public async Task<List<Market>> GetAllAsync()
        {
            return await _marketDal.GetAll();
        }

        public async Task<Market> GetByIdAsync(int id)
        {
            return await _marketDal.Get(d => d.Id == id);
        }

        public async Task<string> GetNameByIdAsync(int id)
        {
            return (await _marketDal.Get(d => d.Id == id)).Name;
        }

        public async Task UpdateAsync(Market market)
        {
            await _marketDal.Update(market);
        }
    }
}
