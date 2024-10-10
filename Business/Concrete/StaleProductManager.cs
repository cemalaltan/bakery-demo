using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class StaleProductManager : IStaleProductService
    {
        private readonly IStaleProductDal _staleProductDal;

        public StaleProductManager(IStaleProductDal staleProductDal)
        {
            _staleProductDal = staleProductDal;
        }

        public async Task AddAsync(StaleProduct staleProduct)
        {
            await _staleProductDal.Add(staleProduct);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _staleProductDal.DeleteById(id);
        }

        public async Task DeleteAsync(StaleProduct staleProduct)
        {
            await _staleProductDal.Delete(staleProduct);
        }

        public async Task<List<StaleProduct>> GetAllAsync()
        {
            return await _staleProductDal.GetAll();
        }

        public async Task<StaleProduct> GetByIdAsync(int id)
        {
            return await _staleProductDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(StaleProduct staleProduct)
        {
            await _staleProductDal.Update(staleProduct);
        }

        public async Task<List<StaleProductDto>> GetByDateAndCategoryAsync(DateTime date, int categoryId)
        {
            return await  _staleProductDal.GetByDateAndCategory(date, categoryId);
        }

        public async Task<List<ProductNotAddedDto>> GetProductsNotAddedToStaleAsync(DateTime date, int categoryId)
        {
            return await _staleProductDal.GetProductsNotAddedToStale(date, categoryId);
        }

        public async Task<int> GetQuantityStaleProductByDateAndProductIdAsync(DateTime date, int productId)
        {
            StaleProduct staleProduct = await _staleProductDal.Get(d => d.ProductId == productId && d.Date.Date == date.Date);
            return staleProduct == null ? 0 : staleProduct.Quantity;
        }

        public async Task<bool> IsExistAsync(int productId, DateTime date)
        {
            return await _staleProductDal.IsExist(productId, date);
        }

        public async Task<Dictionary<int, int>> GetStaleProductsByDateAndCategoryAsync(DateTime date, int categoryId)
        {
            return await _staleProductDal.GetStaleProductsByDateAndCategory(date, categoryId);
        }
    }
}
