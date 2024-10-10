using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ServiceStaleProductManager : IServiceStaleProductService
    {
        private readonly IServiceStaleProductDal _serviceStaleProductDal;

        public ServiceStaleProductManager(IServiceStaleProductDal serviceStaleProductDal)
        {
            _serviceStaleProductDal = serviceStaleProductDal;
        }

        public async Task AddAsync(ServiceStaleProduct serviceStaleProduct)
        {
            await _serviceStaleProductDal.Add(serviceStaleProduct);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _serviceStaleProductDal.DeleteById(id);
        }

        public async Task DeleteAsync(ServiceStaleProduct serviceStaleProduct)
        {
            await _serviceStaleProductDal.Delete(serviceStaleProduct);
        }

        public async Task<List<ServiceStaleProduct>> GetAllAsync()
        {
            return await _serviceStaleProductDal.GetAll();
        }

        public async Task<ServiceStaleProduct> GetByIdAsync(int id)
        {
            return await _serviceStaleProductDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(ServiceStaleProduct serviceStaleProduct)
        {
            await _serviceStaleProductDal.Update(serviceStaleProduct);
        }

        public async Task<List<ServiceStaleProduct>> GetAllByDateAsync(DateTime date, int serviceTypeId)
        {
            return await _serviceStaleProductDal.GetAll(s => s.Date.Date == date.Date && s.ServiceTypeId == serviceTypeId);
        }
    }
}

