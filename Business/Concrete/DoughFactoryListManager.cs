﻿using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class DoughFactoryListManager : IDoughFactoryListService
    {
        IDoughFactoryListDal _doughFactoryListDal;

        public DoughFactoryListManager(IDoughFactoryListDal doughFactoryListDal)
        {
            _doughFactoryListDal = doughFactoryListDal;

        }

        public int Add(DoughFactoryList doughFactoryList)
        {
            _doughFactoryListDal.Add(doughFactoryList);
            return doughFactoryList.Id;
        }

        public void Delete(DoughFactoryList doughFactoryList)
        {
            _doughFactoryListDal.Delete(doughFactoryList);
        }

        public List<DoughFactoryList> GetAll()
        {
            return _doughFactoryListDal.GetAll();
        }

        public List<DoughFactoryListDto> GetByDate(DateTime date)
        {
            return _doughFactoryListDal.GetAllLists(date);
        }

        public DoughFactoryList GetById(int id)
        {
            return _doughFactoryListDal.Get(d => d.Id == id);
        }

        public void Update(DoughFactoryList doughFactoryList)
        {
            _doughFactoryListDal.Update(doughFactoryList);
        }
    }

}
