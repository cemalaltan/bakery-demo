using Entities.Concrete;

namespace Business.Abstract
{
    public interface IServiceListDetailService
    {
        Task<List<ServiceListDetail>> GetAllAsync();
        Task<List<ServiceListDetail>> GetByListIdAsync(int id);
        Task<ServiceListDetail> GetByServiceListIdAndMarketContractIdAsync(int serviceListId, int marketContracId);
        Task<int> GetIdByServiceListIdAndMarketContractIdAsync(int serviceListId, int marketContracId);
        Task AddAsync(ServiceListDetail serviceListDetail);
        Task DeleteByIdAsync(int id);
        Task DeleteByServiceListIdAndMarketContractIdAsync(int serviceListId, int marketContracId);
        Task DeleteAsync(ServiceListDetail serviceListDetail);
        Task UpdateAsync(ServiceListDetail serviceListDetail);
        Task<ServiceListDetail> GetByIdAsync(int id);
        Task<bool> IsExistAsync(int serviceListId, int marketContractId);

    }
}
