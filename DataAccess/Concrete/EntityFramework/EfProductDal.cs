using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, BakeryAppContext>, IProductDal
    {
        private readonly BakeryAppContext _context;
        public EfProductDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public List<Product> GetNotAddedProductsByListAndCategoryId(int listId, int categoryId)
        {
          
                if (listId == 0)
                {
                    var getCategoryProducts = _context.Products
                         .Where(p => p.CategoryId == categoryId && p.Status == true)
                         .ToList();

                    return getCategoryProducts;
                }
                else
                {
                    var productIdsInProductionList = _context.ProductionListDetails
                   .Where(m => m.ProductionListId == listId)
                   .Select(q => q.ProductId)
                   .ToList();

                    var productsNotInProductionList = _context.Products
                        .Where(p => p.CategoryId == categoryId && !productIdsInProductionList.Contains(p.Id) && p.Status == true)
                        .ToList();

                    return productsNotInProductionList;
              

            }
        }
    }
}
