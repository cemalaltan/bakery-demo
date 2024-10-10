using Entities.Concrete;

namespace Business.Abstract
{
    public interface IProductionListService
    {
        Task<List<ProductionList>> GetAllAsync();
        Task<int> AddAsync(ProductionList productionList);
        Task DeleteByIdAsync(int id);
        Task<int> GetByDateAndCategoryIdAsync(DateTime date, int categoryId);
        Task<List<int>> GetByDateAsync(DateTime date);
        Task DeleteAsync(ProductionList productionList);
        Task UpdateAsync(ProductionList productionList);
        Task<ProductionList> GetByIdAsync(int id);

    }
}
