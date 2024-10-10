using Entities.Concrete;

namespace Business.Abstract
{
    public interface IDoughFactoryListDetailService
    {
        Task<List<DoughFactoryListDetail>> GetAllAsync();
        Task<List<DoughFactoryListDetail>> GetByDoughFactoryListAsync(int id);
        Task AddAsync(DoughFactoryListDetail doughFactoryListDetail);
        Task DeleteAsync(DoughFactoryListDetail doughFactoryListDetail);
        Task DeleteByIdAsync(int id);
        Task UpdateAsync(DoughFactoryListDetail doughFactoryListDetail);
        Task<DoughFactoryListDetail> GetByIdAsync(int id);
        Task<bool> IsExistAsync(int id, int listId);

    }
}
