using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductsCountingManager : IProductsCountingService
    {
        private readonly IProductsCountingDal _productsCountingDal;

        public ProductsCountingManager(IProductsCountingDal productsCountingDal)
        {
            _productsCountingDal = productsCountingDal;
        }

        public async Task AddAsync(ProductsCounting productsCounting)
        {
            await _productsCountingDal.Add(productsCounting);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _productsCountingDal.DeleteById(id);
        }

        public async Task DeleteAsync(ProductsCounting productsCounting)
        {
            await _productsCountingDal.Delete(productsCounting);
        }

        public async Task<List<ProductsCounting>> GetAllAsync()
        {
            return await _productsCountingDal.GetAll();
        }

        public async Task<ProductsCounting> GetByIdAsync(int id)
        {
            return await _productsCountingDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(ProductsCounting productsCounting)
        {
            await _productsCountingDal.Update(productsCounting);
        }

        public async Task<List<ProductsCounting>> GetProductsCountingByDateAsync(DateTime date)
        {
            return await _productsCountingDal.GetAll(p => p.Date.Date == date.Date);
        }

        public async Task<bool> IsExistAsync(int productId, DateTime date)
        {
            return await _productsCountingDal.Get(d => d.ProductId == productId && d.Date.Date == date.Date) != null;
        }

        public async Task<int> GetQuantityProductsCountingByDateAndProductIdAsync(DateTime date, int productId)
        {
            var productsCounting = await _productsCountingDal.Get(p => p.Date.Date == date.Date && p.ProductId == productId);
            return productsCounting?.Quantity ?? 0;
        }

        public async Task AddListAsync(List<ProductsCounting> productsCountings)
        {
             await _productsCountingDal.AddList(productsCountings);
        }

        public async Task<Dictionary<int, int>> GetDictionaryProductsCountingByDateAndCategoryAsync(DateTime date, int categoryId)
        {
            return  await _productsCountingDal.GetDictionaryProductsCountingByDateAndCategory(date, categoryId);
        }

        public async Task<List<ProductsCountingDto>> GetProductsCountingByDateAndCategoryAsync(DateTime date, int categoryId)
        {
            return  await _productsCountingDal.GetProductsCountingByDateAndCategory(date, categoryId);
        }
    }
}
