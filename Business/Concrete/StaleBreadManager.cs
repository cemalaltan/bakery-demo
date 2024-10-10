using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StaleBreadManager : IStaleBreadService
    {
        private readonly IStaleBreadDal _staleBreadDal;

        public StaleBreadManager(IStaleBreadDal staleBreadDal)
        {
            _staleBreadDal = staleBreadDal;
        }

        public async Task AddAsync(StaleBread staleBread)
        {
            await _staleBreadDal.Add(staleBread);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _staleBreadDal.DeleteById(id);
        }

        public async Task DeleteAsync(StaleBread staleBread)
        {
            await _staleBreadDal.Delete(staleBread);
        }

        public async Task<List<StaleBread>> GetAllAsync()
        {
            return await _staleBreadDal.GetAll();
        }

        public async Task<StaleBread> GetByIdAsync(int id)
        {
            return await _staleBreadDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(StaleBread staleBread)
        {
            await _staleBreadDal.Update(staleBread);
        }

        public async Task<double> GetStaleBreadDailyReportAsync(DateTime date)
        {
            return  await _staleBreadDal.GetReport(date);
        }

        public async Task<List<DoughFactoryProduct>> GetDoughFactoryProductsAsync(DateTime date)
        {
            return  await _staleBreadDal.GetDoughFactoryProductsByDate(date);
        }

        public async Task<List<StaleBreadDto>> GetAllByDateAsync(DateTime date)
        {
            return  await _staleBreadDal.GetAllByDate(date);
        }

        public async Task<bool> IsExistAsync(int doughFactoryProductId, DateTime date)
        {
            return  await _staleBreadDal.IsExist(doughFactoryProductId, date);
        }
    }
}
