using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductService
    {
        Task<List<Product>> GetAllAsync();
        Task<List<Product>> GetAllByCategoryIdAsync(int categoryId);
        Task AddAsync(Product product);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(Product product);
        Task UpdateAsync(Product product);
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetNotAddedProductsByListAndCategoryIdAsync(int listId, int categoryId);
        Task<List<Product>> GetAllProductsByCategoryIdAsync(int categoryId);
        Task<decimal> GetPriceByIdAsync(int id);

    }
}
