using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DoughFactoryListManager : IDoughFactoryListService
    {
        private readonly IDoughFactoryListDal _doughFactoryListDal;

        public DoughFactoryListManager(IDoughFactoryListDal doughFactoryListDal)
        {
            _doughFactoryListDal = doughFactoryListDal;
        }

        public async Task<int> AddAsync(DoughFactoryList doughFactoryList)
        {
            await _doughFactoryListDal.Add(doughFactoryList);
            return doughFactoryList.Id;
        }

        public async Task DeleteAsync(DoughFactoryList doughFactoryList)
        {
            await _doughFactoryListDal.Delete(doughFactoryList);
        }

        public async Task<List<DoughFactoryList>> GetAllAsync()
        {
            return await _doughFactoryListDal.GetAll();
        }

        public async Task<List<DoughFactoryListDto>> GetByDateAsync(DateTime date)
        {
            return  await _doughFactoryListDal.GetAllLists(date);
        }

        public async Task<DoughFactoryList> GetByIdAsync(int id)
        {
            return await _doughFactoryListDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(DoughFactoryList doughFactoryList)
        {
            await _doughFactoryListDal.Update(doughFactoryList);
        }
    }
}
