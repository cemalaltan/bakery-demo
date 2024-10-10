using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductsCountingDal : EfEntityRepositoryBase<ProductsCounting, BakeryAppContext>, IProductsCountingDal
    {
        private readonly BakeryAppContext _context;
        public EfProductsCountingDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public void AddList(List<ProductsCounting> productsCountings)
        {

            _context.ProductsCountings.AddRange(productsCountings);
            _context.SaveChanges();
          
        }

   

        public Dictionary<int, int> GetDictionaryProductsCountingByDateAndCategory(DateTime date, int categoryId)
        {
          
                var productsCountingQuantities = _context.ProductsCountings
                    .Where(pc => pc.Date.Date == date.Date)
                    .Join(_context.Products,
                          counting => counting.ProductId,
                          product => product.Id,
                          (counting, product) => new { Counting = counting, Product = product })
                    .Where(pair => pair.Product != null && pair.Product.CategoryId == categoryId)
                    .ToDictionary(pair => pair.Counting.ProductId, pair => pair.Counting.Quantity);

                return productsCountingQuantities;
           
        }

        public List<ProductsCountingDto> GetProductsCountingByDateAndCategory(DateTime date, int categoryId)
        {
           
                var productsCountingList = _context.ProductsCountings
                    .Where(pc => pc.Date.Date == date.Date)
                    .Join(_context.Products,
                          counting => counting.ProductId,
                          product => product.Id,
                          (counting, product) => new { Counting = counting, Product = product })
                    .Where(pair => pair.Product != null && pair.Product.CategoryId == categoryId)
                    .Select(pair => new ProductsCountingDto
                    {
                        Id = pair.Counting.Id,
                        ProductId = pair.Counting.ProductId,
                        Quantity = pair.Counting.Quantity,
                        ProductName = pair.Product.Name,
                        Date = pair.Counting.Date,
                    })
                    .ToList();

                return productsCountingList;
            
        }
    }
}
