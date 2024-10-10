using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceTypeService
    {
        Task<List<ServiceType>> GetAllAsync();
        Task AddAsync(ServiceType serviceType);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(ServiceType serviceType);
        Task UpdateAsync(ServiceType serviceType);
        Task<ServiceType> GetByIdAsync(int id);

    }
}
