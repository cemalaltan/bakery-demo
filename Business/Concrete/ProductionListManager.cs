using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ProductionListManager : IProductionListService
    {
        private readonly IProductionListDal _productionListDal;

        public ProductionListManager(IProductionListDal productionListDal)
        {
            _productionListDal = productionListDal;
        }

        public async Task<int> AddAsync(ProductionList productionList)
        {
            await _productionListDal.Add(productionList);
            return productionList.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _productionListDal.DeleteById(id);
        }

        public async Task DeleteAsync(ProductionList productionList)
        {
            await _productionListDal.Delete(productionList);
        }

        public async Task<List<ProductionList>> GetAllAsync()
        {
            return await _productionListDal.GetAll();
        }

        public async Task<ProductionList> GetByIdAsync(int id)
        {
            return await _productionListDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(ProductionList productionList)
        {
            await _productionListDal.Update(productionList);
        }

        public async Task<int> GetByDateAndCategoryIdAsync(DateTime date, int categoryId)
        {
            ProductionList productList = await _productionListDal.Get(p => p.Date.Date == date.Date && p.CategoryId == categoryId);
            return productList == null ? 0 : productList.Id;
        }

        public async Task<List<int>> GetByDateAsync(DateTime date)
        {
            List<ProductionList> productList =await _productionListDal.GetAll(p => p.Date.Date == date.Date);
            if (productList != null && productList.Any())
            {
                List<int> productListIds = productList.Select(p => p.Id).ToList();
                return productListIds;
            }
            else
            {
                return null;
            }
        }
    }
}
