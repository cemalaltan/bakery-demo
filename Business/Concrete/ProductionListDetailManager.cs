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
    public class ProductionListDetailManager : IProductionListDetailService
    {
        private readonly IProductionListDetailDal _productionListDetailDal;
        private readonly IProductionListService _productionListService;

        public ProductionListDetailManager(IProductionListService productionListService, IProductionListDetailDal productionListDetailDal)
        {
            _productionListDetailDal = productionListDetailDal;
            _productionListService = productionListService;
        }

        public async Task AddAsync(ProductionListDetail productionListDetail)
        {
            await _productionListDetailDal.Add(productionListDetail);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _productionListDetailDal.DeleteById(id);
        }

        public async Task DeleteAsync(ProductionListDetail productionListDetail)
        {
            await _productionListDetailDal.Delete(productionListDetail);
        }

        public async Task<List<ProductionListDetail>> GetAllAsync()
        {
            return await _productionListDetailDal.GetAll();
        }

        public async Task<ProductionListDetail> GetByIdAsync(int id)
        {
            return await _productionListDetailDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(ProductionListDetail productionListDetail)
        {
            await _productionListDetailDal.Update(productionListDetail);
        }

        public async Task<bool> IsExistAsync(int id, int listId)
        {
            return  await _productionListDetailDal.IsExist(id, listId);
        }

        public async Task AddListAsync(List<ProductionListDetail> productionListDetails)
        {
             await _productionListDetailDal.AddList(productionListDetails);
        }

        public async Task<List<GetAddedProductsDto>> GetProductsByListIdAsync(int id)
        {
            return  await _productionListDetailDal.GetAddedProducts(id);
        }

        public async Task<ProductionListDetail> GetProductionListDetailByDateAndProductIdAsync(DateTime date, Product product)
        {
            int productionListId = await _productionListService.GetByDateAndCategoryIdAsync(date, product.CategoryId);

            if (productionListId <= 0)
            {
                return new ProductionListDetail { Quantity = 0, Price = 0 };
            }

            ProductionListDetail productionListDetail = await
                _productionListDetailDal.Get(d => d.ProductId == product.Id && d.ProductionListId == productionListId);

            return productionListDetail ?? new ProductionListDetail { Quantity = 0, Price = 0 };
        }
    }
}
