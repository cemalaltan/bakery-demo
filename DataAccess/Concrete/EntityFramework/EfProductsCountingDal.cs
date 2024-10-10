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

        public async Task AddList(List<ProductsCounting> productsCountings)
        {
            await _context.ProductsCountings.AddRangeAsync(productsCountings);
            await _context.SaveChangesAsync();
        }

        public async Task<Dictionary<int, int>> GetDictionaryProductsCountingByDateAndCategory(DateTime date, int categoryId)
        {
            var productsCountingQuantities = await _context.ProductsCountings
                .Where(pc => pc.Date.Date == date.Date)
                .Join(_context.Products,
                      counting => counting.ProductId,
                      product => product.Id,
                      (counting, product) => new { Counting = counting, Product = product })
                .Where(pair => pair.Product != null && pair.Product.CategoryId == categoryId)
                .ToDictionaryAsync(pair => pair.Counting.ProductId, pair => pair.Counting.Quantity);

            return productsCountingQuantities;
        }

        public async Task<List<ProductsCountingDto>> GetProductsCountingByDateAndCategory(DateTime date, int categoryId)
        {
            var productsCountingList = await _context.ProductsCountings
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
                .ToListAsync();

            return productsCountingList;
        }
    }
}
