using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task AddAsync(Category category);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(Category category);
        Task UpdateAsync(Category category);
        Task<Category> GetByIdAsync(int id);

    }
}
