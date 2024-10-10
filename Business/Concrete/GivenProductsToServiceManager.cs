using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class GivenProductsToServiceManager : IGivenProductsToServiceService
    {
        private readonly IGivenProductsToServiceDal _givenProductsToServiceDal;

        public GivenProductsToServiceManager(IGivenProductsToServiceDal givenProductsToServiceDal)
        {
            _givenProductsToServiceDal = givenProductsToServiceDal;
        }

        public async Task AddAsync(GivenProductsToService givenProductsToService)
        {
            await _givenProductsToServiceDal.Add(givenProductsToService);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _givenProductsToServiceDal.DeleteById(id);
        }

        public async Task DeleteAsync(GivenProductsToService givenProductsToService)
        {
            await _givenProductsToServiceDal.Delete(givenProductsToService);
        }

        public async Task<List<GivenProductsToService>> GetAllAsync()
        {
            return await _givenProductsToServiceDal.GetAll();
        }

        public async Task<GivenProductsToService> GetByIdAsync(int id)
        {
            return await _givenProductsToServiceDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(GivenProductsToService givenProductsToService)
        {
            await _givenProductsToServiceDal.Update(givenProductsToService);
        }

        public async Task<List<GivenProductsToServiceTotalResultDto>> GetTotalQuantityByDateAsync(DateTime date)
        {
            return await  _givenProductsToServiceDal.GetTotalQuantityResultByDate(date);
        }

        public async Task<List<GivenProductsToService>> GetAllByDateAndServisTypeIdAsync(DateTime date, int servisTypeId)
        {
            return  await _givenProductsToServiceDal.GetAllByDateAndServisTypeId(date, servisTypeId);
        }
    }
}
