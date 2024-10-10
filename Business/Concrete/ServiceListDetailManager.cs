using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ServiceListDetailManager : IServiceListDetailService
    {
        private readonly IServiceListDetailDal _serviceListDetailDal;

        public ServiceListDetailManager(IServiceListDetailDal serviceListDetailDal)
        {
            _serviceListDetailDal = serviceListDetailDal;
        }

        public async Task AddAsync(ServiceListDetail serviceListDetail)
        {
            await _serviceListDetailDal.Add(serviceListDetail);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _serviceListDetailDal.DeleteById(id);
        }

        public async Task DeleteAsync(ServiceListDetail serviceListDetail)
        {
            await _serviceListDetailDal.Delete(serviceListDetail);
        }

        public async Task<List<ServiceListDetail>> GetAllAsync()
        {
            return await _serviceListDetailDal.GetAll();
        }

        public async Task<ServiceListDetail> GetByIdAsync(int id)
        {
            return await _serviceListDetailDal.Get(d => d.Id == id);
        }

        public async Task<int> GetIdByServiceListIdAndMarketContractIdAsync(int serviceListId, int marketContractId)
        {
            var serviceListDetail = await _serviceListDetailDal.Get(d => d.ServiceListId == serviceListId && d.MarketContractId == marketContractId);
            return serviceListDetail?.Id ?? 0;
        }

        public async Task UpdateAsync(ServiceListDetail serviceListDetail)
        {
            await _serviceListDetailDal.Update(serviceListDetail);
        }

        public async Task<List<ServiceListDetail>> GetByListIdAsync(int id)
        {
            return await _serviceListDetailDal.GetAll(p => p.ServiceListId == id);
        }

        public async Task DeleteByServiceListIdAndMarketContractIdAsync(int serviceListId, int marketContractId)
        {
           await  _serviceListDetailDal.DeleteByServiceListIdAndMarketContracId(serviceListId, marketContractId);
        }

        public async Task<ServiceListDetail> GetByServiceListIdAndMarketContractIdAsync(int serviceListId, int marketContractId)
        {
            return await _serviceListDetailDal.Get(p => p.ServiceListId == serviceListId && p.MarketContractId == marketContractId);
        }

        public async Task<bool> IsExistAsync(int serviceListId, int marketContractId)
        {
            return await _serviceListDetailDal.IsExist(serviceListId, marketContractId);
        }
    }
}
