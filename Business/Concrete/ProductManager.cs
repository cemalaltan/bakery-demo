using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private readonly IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public async Task AddAsync(Product product)
        {
            await _productDal.Add(product);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _productDal.DeleteById(id);
        }

        public async Task DeleteAsync(Product product)
        {
            await _productDal.Delete(product);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _productDal.GetAll();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _productDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            await _productDal.Update(product);
        }

        public async Task<List<Product>> GetNotAddedProductsByListAndCategoryIdAsync(int listId, int categoryId)
        {
            return  _productDal.GetNotAddedProductsByListAndCategoryId(listId, categoryId);
        }

        public async Task<decimal> GetPriceByIdAsync(int id)
        {
            return (await _productDal.Get(p => p.Id == id)).Price;
        }

        public async Task<List<Product>> GetAllByCategoryIdAsync(int categoryId)
        {
            return await _productDal.GetAll(p => p.CategoryId == categoryId && p.Status == true);
        }

        public async Task<List<Product>> GetAllProductsByCategoryIdAsync(int categoryId)
        {
            return await _productDal.GetAll(p => p.CategoryId == categoryId);
        }
    }
}
