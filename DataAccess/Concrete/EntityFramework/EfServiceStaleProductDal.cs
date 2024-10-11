﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceStaleProductDal : EfEntityRepositoryBase<ServiceStaleProduct, BakeryAppContext>, IServiceStaleProductDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<ServiceStaleProduct>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

    }
}
