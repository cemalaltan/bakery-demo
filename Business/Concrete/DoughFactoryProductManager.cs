using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DoughFactoryProductManager : IDoughFactoryProductService
    {
        private readonly IDoughFactoryProductDal _doughFactoryProductDal;

        public DoughFactoryProductManager(IDoughFactoryProductDal doughFactoryProductDal)
        {
            _doughFactoryProductDal = doughFactoryProductDal;
        }

        public async Task AddAsync(DoughFactoryProduct doughFactoryProduct)
        {
            await _doughFactoryProductDal.Add(doughFactoryProduct);
        }

        public async Task DeleteAsync(DoughFactoryProduct doughFactoryProduct)
        {
            await _doughFactoryProductDal.Delete(doughFactoryProduct);
        }

        public async Task<List<DoughFactoryProduct>> GetAllAsync()
        {
            return await _doughFactoryProductDal.GetAll(d => d.Status == true);
        }

        public async Task<List<DoughFactoryProduct>> GetAllProductsAsync()
        {
            return await _doughFactoryProductDal.GetAll();
        }

        public async Task<DoughFactoryProduct> GetByIdAsync(int id)
        {
            return await _doughFactoryProductDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(DoughFactoryProduct doughFactoryProduct)
        {
            await _doughFactoryProductDal.Update(doughFactoryProduct);
        }
    }
}
