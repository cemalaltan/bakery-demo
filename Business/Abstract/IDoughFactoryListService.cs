using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IDoughFactoryListService
    {
        Task<List<DoughFactoryList>> GetAllAsync();
        Task<int> AddAsync(DoughFactoryList doughFactoryList);
        Task DeleteAsync(DoughFactoryList doughFactoryList);
        Task UpdateAsync(DoughFactoryList doughFactoryList);
        Task<DoughFactoryList> GetByIdAsync(int id);
        Task<List<DoughFactoryListDto>> GetByDateAsync(DateTime date);

    }
}
