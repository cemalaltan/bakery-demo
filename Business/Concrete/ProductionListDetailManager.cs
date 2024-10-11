﻿using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Business.Concrete
{
    public class ProductionListDetailManager : IProductionListDetailService
    {


        IProductionListDetailDal _productionListDetailDal;
        IProductionListService _productionListService;

        public ProductionListDetailManager(IProductionListService productionListService, IProductionListDetailDal productionListDetailDal)
        {
            _productionListDetailDal = productionListDetailDal;
            _productionListService = productionListService;
        }

        public void Add(ProductionListDetail productionListDetail)
        {
            _productionListDetailDal.Add(productionListDetail);
        }

        public void DeleteById(int id)
        {
            _productionListDetailDal.DeleteById(id);
        }

        public void Delete(ProductionListDetail productionListDetail)
        {
            _productionListDetailDal.Delete(productionListDetail);
        }
        public List<ProductionListDetail> GetAll()
        {
            return _productionListDetailDal.GetAll();
        }

        public ProductionListDetail GetById(int id)
        {
            return _productionListDetailDal.Get(d => d.Id == id);
        }

        public void Update(ProductionListDetail productionListDetail)
        {
            _productionListDetailDal.Update(productionListDetail);
        }

        public bool IsExist(int id, int listId)
        {
            return _productionListDetailDal.IsExist(id, listId);
        }

        public void AddList(List<ProductionListDetail> productionListDetail)
        {
            _productionListDetailDal.AddList(productionListDetail);
        }

        public List<GetAddedProductsDto> GetProductsByListId(int id)
        {
            return _productionListDetailDal.GetAddedProducts(id);

        }

        public ProductionListDetail GetProductionListDetailByDateAndProductId(DateTime date, Product product)
        {

            int productionListId = _productionListService.GetByDateAndCategoryId(date,product.CategoryId);

            if (productionListId <= 0)
            {
                return new ProductionListDetail { Quantity = 0, Price = 0 };
            }

            ProductionListDetail productionListDetail =
                    _productionListDetailDal.Get(d => d.ProductId == product.Id && d.ProductionListId == productionListId);
 
            return productionListDetail == null ? new ProductionListDetail { Quantity = 0, Price = 0 } : productionListDetail;
        }
    }
}
