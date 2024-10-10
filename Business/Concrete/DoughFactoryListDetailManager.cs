using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DoughFactoryListDetailManager : IDoughFactoryListDetailService
    {
        private readonly IDoughFactoryListDetailDal _doughFactoryListDetailDal;

        public DoughFactoryListDetailManager(IDoughFactoryListDetailDal doughFactoryListDetailDal)
        {
            _doughFactoryListDetailDal = doughFactoryListDetailDal;
        }

        public async Task AddAsync(DoughFactoryListDetail doughFactoryListDetail)
        {
            await _doughFactoryListDetailDal.Add(doughFactoryListDetail);
        }

        public async Task DeleteAsync(DoughFactoryListDetail doughFactoryListDetail)
        {
            await _doughFactoryListDetailDal.Delete(doughFactoryListDetail);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _doughFactoryListDetailDal.DeleteById(id);
        }

        public async Task<List<DoughFactoryListDetail>> GetAllAsync()
        {
            return await _doughFactoryListDetailDal.GetAll();
        }

        public async Task<List<DoughFactoryListDetail>> GetByDoughFactoryListAsync(int id)
        {
            return await _doughFactoryListDetailDal.GetAll(d => d.DoughFactoryListId == id);
        }

        public async Task<DoughFactoryListDetail> GetByIdAsync(int id)
        {
            return await _doughFactoryListDetailDal.Get(d => d.Id == id);
        }

        public async Task<bool> IsExistAsync(int id, int listId)
        {
            return  _doughFactoryListDetailDal.IsExist(id, listId);
        }

        public async Task UpdateAsync(DoughFactoryListDetail doughFactoryListDetail)
        {
            await _doughFactoryListDetailDal.Update(doughFactoryListDetail);
        }
    }
}
