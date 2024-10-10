using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMonthlyProductCountService
    {
        Task<Dictionary<string, List<Product>>> GetAllProductsAsync();
        Task<Dictionary<string, List<MonthlyProductCount>>> GetAddedProductsAsync(int year, int month);
        Task<List<MonthlyProductCount>> GetAllAsync();
        Task AddAsync(MonthlyProductCount monthlyProductCount);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(MonthlyProductCount monthlyProductCount);
        Task UpdateAsync(MonthlyProductCount monthlyProductCount);
        Task<MonthlyProductCount> GetByIdAsync(int id);

    }
}
