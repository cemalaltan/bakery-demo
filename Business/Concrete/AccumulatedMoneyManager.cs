using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AccumulatedMoneyManager : IAccumulatedMoneyService
    {
        private readonly IAccumulatedMoneyDal _accumulatedCashDal;

        public AccumulatedMoneyManager(IAccumulatedMoneyDal accumulatedCashDal)
        {
            _accumulatedCashDal = accumulatedCashDal;
        }

        public async Task AddAsync(AccumulatedMoney accumulatedCash)
        {
            await Task.Run(() => _accumulatedCashDal.Add(accumulatedCash));
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _accumulatedCashDal.DeleteById(id));
        }

        public async Task DeleteAsync(AccumulatedMoney accumulatedCash)
        {
            await Task.Run(() => _accumulatedCashDal.Delete(accumulatedCash));
        }

        public async Task<List<AccumulatedMoney>> GetAllByTypeAsync(int type)
        {
            return await Task.Run(() => _accumulatedCashDal.GetAll(a => a.Type == type));
        }

        public async Task<AccumulatedMoney> GetByIdAsync(int id)
        {
            return await Task.Run(() => _accumulatedCashDal.Get(d => d.Id == id));
        }

        public async Task UpdateAsync(AccumulatedMoney accumulatedCash)
        {
            await Task.Run(() => _accumulatedCashDal.Update(accumulatedCash));
        }

        public async Task<List<AccumulatedMoney>> GetByDateRangeAndTypeAsync(DateTime startDate, DateTime endDate, int type)
        {
            return await Task.Run(() =>
                _accumulatedCashDal.GetAll(a =>
                    a.CreatedAt.Date >= startDate.Date &&
                    a.CreatedAt.Date <= endDate.Date &&
                    a.Type == type));
        }

        public async Task<AccumulatedMoney> GetByDateAndTypeAsync(DateTime date, int type)
        {
            return await Task.Run(() =>
                _accumulatedCashDal.Get(a =>
                    a.CreatedAt.Date == date.Date &&
                    a.Type == type));
        }

        public async Task<decimal> GetTotalAccumulatedMoneyByDateAndTypeAsync(DateTime date, int type)
        {
            return await Task.Run(() =>
                _accumulatedCashDal.GetAll(a =>
                    a.CreatedAt > date &&
                    a.Type == type).Result.Sum(a => a.Amount));
        }
    }
}
