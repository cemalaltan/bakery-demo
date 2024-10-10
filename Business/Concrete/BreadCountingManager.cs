using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BreadCountingManager : IBreadCountingService
    {
        private readonly IBreadCountingDal _breadCountingDal;

        public BreadCountingManager(IBreadCountingDal breadCountingDal)
        {
            _breadCountingDal = breadCountingDal;  
        }

        public async Task AddAsync(BreadCounting breadCounting)
        {
            await _breadCountingDal.Add(breadCounting); 
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _breadCountingDal.DeleteById(id); 
        }

        public async Task DeleteAsync(BreadCounting breadCounting)
        {
            await _breadCountingDal.Delete(breadCounting);
        }

        public async Task<List<BreadCounting>> GetAllAsync()
        {
           return await _breadCountingDal.GetAll();
        }

        public async Task<BreadCounting> GetByIdAsync(int id)
        {
            return await _breadCountingDal.Get(d => d.Id == id); 
        }

        public async Task UpdateAsync(BreadCounting breadCounting)
        {
            await _breadCountingDal.Update(breadCounting); 
        }

        public async Task<BreadCounting> GetBreadCountingByDateAsync(DateTime date)
        {
            return await _breadCountingDal.Get(b => b.Date.Date == date.Date); 
        }

        public async Task AddListAsync(List<BreadCounting> breadCounting)
        {
            foreach (var item in breadCounting)
            {
                await _breadCountingDal.Add(item); 
            }
        }
    }
}
