using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IProductsCountingService
    {
        Task<List<ProductsCounting>> GetAllAsync();
        Task<List<ProductsCounting>> GetProductsCountingByDateAsync(DateTime date);
        Task<Dictionary<int, int>> GetDictionaryProductsCountingByDateAndCategoryAsync(DateTime date, int categoryId);
        Task<List<ProductsCountingDto>> GetProductsCountingByDateAndCategoryAsync(DateTime date, int categoryId);
        Task AddAsync(ProductsCounting productsCounting);
        Task AddListAsync(List<ProductsCounting> productsCounting);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(ProductsCounting productsCounting);
        Task UpdateAsync(ProductsCounting productsCounting);
        Task<ProductsCounting> GetByIdAsync(int id);
        Task<int> GetQuantityProductsCountingByDateAndProductIdAsync(DateTime date, int productId);
        Task<bool> IsExistAsync(int productId, DateTime date);

    }
}
