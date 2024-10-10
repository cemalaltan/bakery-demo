using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AllServiceManager : IAllServiceService
    {
        private readonly IAllServiceDal _allServiceDal;

        public AllServiceManager(IAllServiceDal allServiceDal)
        {
            _allServiceDal = allServiceDal;
        }

        public async Task AddAsync(AllService allService)
        {
            await Task.Run(() => _allServiceDal.Add(allService));
        }

        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _allServiceDal.DeleteById(id));
        }

        public async Task DeleteAsync(AllService allService)
        {
            await Task.Run(() => _allServiceDal.Delete(allService));
        }

        public async Task<List<AllService>> GetAllAsync()
        {
            return await Task.Run(() => _allServiceDal.GetAll());
        }

        public async Task<AllService> GetByIdAsync(int id)
        {
            return await Task.Run(() => _allServiceDal.Get(d => d.Id == id));
        }

        public async Task UpdateAsync(AllService allService)
        {
            await Task.Run(() => _allServiceDal.Update(allService));
        }
    }
}
