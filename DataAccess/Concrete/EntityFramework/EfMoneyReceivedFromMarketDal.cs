﻿using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMoneyReceivedFromMarketDal : EfEntityRepositoryBase<MoneyReceivedFromMarket, BakeryAppContext>, IMoneyReceivedFromMarketDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<MoneyReceivedFromMarket>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public bool IsExist(int marketId, DateTime date)
        {
            using (BakeryAppContext context = new())
            {              
                return context.Set<MoneyReceivedFromMarket>()
                                    .Any(m => m.MarketId == marketId && m.Date.Date == date.Date);
            }
        }

    }
}
