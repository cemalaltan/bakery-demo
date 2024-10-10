using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductionListDetailDal : EfEntityRepositoryBase<ProductionListDetail, BakeryAppContext>, IProductionListDetailDal
    {
        private readonly BakeryAppContext _context;
        public EfProductionListDetailDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public void AddList(List<ProductionListDetail> productionListDetail)
        {
          
                foreach (var production in productionListDetail)
                {
                    var deletedEntity = _context.Entry(production);
                    deletedEntity.State = EntityState.Added;
                }
            _context.SaveChanges();

           
        }


        public List<GetAddedProductsDto> GetAddedProducts(int id)
        {
           
                var addedProducts = (from plDetail in _context.Set<ProductionListDetail>()
                                     join product in _context.Set<Product>() on plDetail.ProductId equals product.Id
                                     where plDetail.ProductionListId == id
                                     select new GetAddedProductsDto
                                     {
                                         Id = plDetail.Id,
                                         ProductId = plDetail.ProductId,
                                         ProductName = product.Name, // Assuming you want the product name
                                         Price = plDetail.Price,
                                         ProductionListId = plDetail.ProductionListId,
                                         Quantity = plDetail.Quantity
                                     }).ToList();
                return addedProducts;
           
        }

        public bool IsExist(int id, int listId)
        {
           
                return (_context.ProductionListDetails?.Any(p => p.ProductId == id && p.ProductionListId == listId)).GetValueOrDefault();
           
        }

    }
}
