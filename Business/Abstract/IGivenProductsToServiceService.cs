using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IGivenProductsToServiceService
    {
        Task<List<GivenProductsToService>> GetAllAsync();
        Task<List<GivenProductsToService>> GetAllByDateAndServisTypeIdAsync(DateTime date, int servisTypeId);
        Task<List<GivenProductsToServiceTotalResultDto>> GetTotalQuantityByDateAsync(DateTime date);
        Task AddAsync(GivenProductsToService delivery);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(GivenProductsToService delivery);
        Task UpdateAsync(GivenProductsToService delivery);
        Task<GivenProductsToService> GetByIdAsync(int id);

    }
}
