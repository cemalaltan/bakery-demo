﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfDoughFactoryListDal : EfEntityRepositoryBase<DoughFactoryList, BakeryAppContext>, IDoughFactoryListDal
    {
        private readonly BakeryAppContext _context;
        public EfDoughFactoryListDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public List<DoughFactoryListDto> GetAllLists(DateTime date)
        {
        
                var doughListDto = (
                    from dough in _context.DoughFactoryLists
                    join user in _context.Users on dough.UserId equals user.Id
                    where dough.Date.Date == date.Date
                    select new DoughFactoryListDto
                    {
                        Id = dough.Id,
                        UserId = dough.UserId,
                        UserName = user.FirstName+" "+user.LastName, // Assuming there's a property like UserName in your User entity
                        Date = dough.Date
                    }
                ).ToList();

                return doughListDto;
         
        }
    }
}
