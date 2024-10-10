using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ServiceProductManager : IServiceProductService
    {
        private readonly IServiceProductDal _serviceProductDal;

        public ServiceProductManager(IServiceProductDal serviceProductDal)
        {
            _serviceProductDal = serviceProductDal;
        }

        public async Task AddAsync(ServiceProduct serviceProduct)
        {
            await _serviceProductDal.Add(serviceProduct);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _serviceProductDal.DeleteById(id);
        }

        public async Task DeleteAsync(ServiceProduct serviceProduct)
        {
            await _serviceProductDal.Delete(serviceProduct);
        }

        public async Task<List<ServiceProduct>> GetAllAsync()
        {
            return await _serviceProductDal.GetAll();
        }

        public async Task<ServiceProduct> GetByIdAsync(int id)
        {
            return await _serviceProductDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(ServiceProduct serviceProduct)
        {
            await _serviceProductDal.Update(serviceProduct);
        }
    }
}
