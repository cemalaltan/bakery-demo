using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IStaleProductService
    {
        Task<List<StaleProduct>> GetAllAsync();
        Task<Dictionary<int, int>> GetStaleProductsByDateAndCategoryAsync(DateTime date, int categoryId);
        Task AddAsync(StaleProduct staleProduct);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(StaleProduct staleProduct);
        Task UpdateAsync(StaleProduct staleProduct);
        Task<StaleProduct> GetByIdAsync(int id);
        Task<int> GetQuantityStaleProductByDateAndProductIdAsync(DateTime date, int productId);
        Task<List<StaleProductDto>> GetByDateAndCategoryAsync(DateTime date, int categoryId);
        Task<List<ProductNotAddedDto>> GetProductsNotAddedToStaleAsync(DateTime date, int categoryId);
        Task<bool> IsExistAsync(int productId, DateTime date);

    }
}
