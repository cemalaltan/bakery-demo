using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ServiceTypeManager : IServiceTypeService
    {
        private readonly IServiceTypeDal _serviceTypeDal;

        public ServiceTypeManager(IServiceTypeDal serviceTypeDal)
        {
            _serviceTypeDal = serviceTypeDal;
        }

        public async Task AddAsync(ServiceType serviceType)
        {
            await _serviceTypeDal.Add(serviceType);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _serviceTypeDal.DeleteById(id);
        }

        public async Task DeleteAsync(ServiceType serviceType)
        {
            await _serviceTypeDal.Delete(serviceType);
        }

        public async Task<List<ServiceType>> GetAllAsync()
        {
            return await _serviceTypeDal.GetAll();
        }

        public async Task<ServiceType> GetByIdAsync(int id)
        {
            return await _serviceTypeDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(ServiceType serviceType)
        {
            await _serviceTypeDal.Update(serviceType);
        }
    }
}
