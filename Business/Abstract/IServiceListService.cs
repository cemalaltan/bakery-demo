
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceListService 
    {
        Task<List<ServiceList>> GetAllAsync();
        Task<int> AddAsync(ServiceList serviceList);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(ServiceList serviceList);
        Task UpdateAsync(ServiceList serviceList);
        Task<ServiceList> GetByIdAsync(int id);
        Task<List<ServiceList>> GetByDateAsync(DateTime date);

    }
}
