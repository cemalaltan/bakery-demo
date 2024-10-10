using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CashCountingManager : ICashCountingService
    {
        private readonly ICashCountingDal _cashCountingDal;

        public CashCountingManager(ICashCountingDal cashCountingDal)
        {
            _cashCountingDal = cashCountingDal;
        }

        public async Task AddAsync(CashCounting cashCounting)
        {
            await _cashCountingDal.Add(cashCounting);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _cashCountingDal.DeleteById(id);
        }

        public async Task DeleteAsync(CashCounting cashCounting)
        {
            await _cashCountingDal.Delete(cashCounting);
        }

        public async Task<List<CashCounting>> GetAllAsync()
        {
            return await _cashCountingDal.GetAll();
        }

        public async Task<CashCounting> GetByIdAsync(int id)
        {
            return await _cashCountingDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(CashCounting cashCounting)
        {
            await _cashCountingDal.Update(cashCounting);
        }

        public async Task<List<CashCounting>> GetCashCountingByDateAsync(DateTime date)
        {
            return await _cashCountingDal.GetAll(c => c.Date.Date == date.Date);
        }

        public async Task<CashCounting> GetOneCashCountingByDateAsync(DateTime date)
        {
            return await _cashCountingDal.Get(c => c.Date.Date == date.Date);
        }
    }
}
