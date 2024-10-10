using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceStaleProductService
    {
        Task<List<ServiceStaleProduct>> GetAllAsync();
        Task<List<ServiceStaleProduct>> GetAllByDateAsync(DateTime date, int serviceTypeId);
        Task AddAsync(ServiceStaleProduct serviceStaleProduct);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(ServiceStaleProduct serviceStaleProduct);
        Task UpdateAsync(ServiceStaleProduct serviceStaleProduct);
        Task<ServiceStaleProduct> GetByIdAsync(int id);

    }
}
