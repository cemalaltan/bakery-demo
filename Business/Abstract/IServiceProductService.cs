using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceProductService
    {
        Task<List<ServiceProduct>> GetAllAsync();
        Task AddAsync(ServiceProduct serviceProduct);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(ServiceProduct serviceProduct);
        Task UpdateAsync(ServiceProduct serviceProduct);
        Task<ServiceProduct> GetByIdAsync(int id);

    }
}
