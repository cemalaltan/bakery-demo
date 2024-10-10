using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ServiceListManager : IServiceListService
    {
        private readonly IServiceListDal _serviceListDal;

        public ServiceListManager(IServiceListDal serviceListDal)
        {
            _serviceListDal = serviceListDal;
        }

        public async Task<int> AddAsync(ServiceList serviceList)
        {
            await _serviceListDal.Add(serviceList);
            return serviceList.Id;
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _serviceListDal.DeleteById(id);
        }

        public async Task DeleteAsync(ServiceList serviceList)
        {
            await _serviceListDal.Delete(serviceList);
        }

        public async Task<List<ServiceList>> GetAllAsync()
        {
            return await _serviceListDal.GetAll();
        }

        public async Task<ServiceList> GetByIdAsync(int id)
        {
            return await _serviceListDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(ServiceList serviceList)
        {
            await _serviceListDal.Update(serviceList);
        }

        public async Task<List<ServiceList>> GetByDateAsync(DateTime date)
        {
            return await _serviceListDal.GetAll(d => d.Date.Date == date.Date);
        }
    }
}
