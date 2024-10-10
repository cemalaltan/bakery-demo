using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStaleProductDal : EfEntityRepositoryBase<StaleProduct, BakeryAppContext>, IStaleProductDal
    {
        private readonly BakeryAppContext _context;
        public EfStaleProductDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<StaleProductDto>> GetByDateAndCategory(DateTime date, int categoryId)
        {
            var staleProductDtos = await _context.StaleProducts
                .Where(s => s.Date.Date == date.Date)
                .Select(stale => new
                {
                    Stale = stale,
                    Product = _context.Products.FirstOrDefault(p => p.Id == stale.ProductId)
                })
                .Where(pair => pair.Product != null && pair.Product.CategoryId == categoryId)
                .Select(pair => new StaleProductDto
                {
                    Id = pair.Stale.Id,
                    ProductId = pair.Stale.ProductId,
                    ProductName = pair.Product.Name,
                    Quantity = pair.Stale.Quantity,
                    Date = pair.Stale.Date
                })
                .ToListAsync();

            return staleProductDtos;
        }

        public async Task<List<ProductNotAddedDto>> GetProductsNotAddedToStale(DateTime date, int categoryId)
        {
            var productsNotAddedToStale = await _context.Products
                .Where(p => p.CategoryId == categoryId && p.Status == true)
                .Where(p => !_context.StaleProducts.Any(sp => sp.Date.Date == date.Date && sp.ProductId == p.Id))
                .Select(p => new ProductNotAddedDto
                {
                    Id = p.Id,
                    Name = p.Name,
                })
                .ToListAsync();

            return productsNotAddedToStale;
        }

        public async Task<Dictionary<int, int>> GetStaleProductsByDateAndCategory(DateTime date, int categoryId)
        {
            var staleProductQuantities = await _context.StaleProducts
                .Where(s => s.Date.Date == date.Date)
                .Join(_context.Products,
                      stale => stale.ProductId,
                      product => product.Id,
                      (stale, product) => new { Stale = stale, Product = product })
                .Where(pair => pair.Product != null && pair.Product.CategoryId == categoryId)
                .ToDictionaryAsync(pair => pair.Stale.ProductId, pair => pair.Stale.Quantity);

            return staleProductQuantities;
        }

        public async Task<bool> IsExist(int productId, DateTime date)
        {
            return await _context.StaleProducts
                .AnyAsync(sp => sp.Date.Date == date.Date && sp.ProductId == productId);
        }
    }
}
