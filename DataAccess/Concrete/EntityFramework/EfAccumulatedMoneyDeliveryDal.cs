using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAccumulatedMoneyDeliveryDal : EfEntityRepositoryBase<AccumulatedMoneyDelivery, BakeryAppContext>, IAccumulatedMoneyDeliveryDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<AccumulatedMoneyDelivery>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public AccumulatedMoneyDelivery? GetLatestDelivery(int type)
        {
            using (BakeryAppContext context = new BakeryAppContext())
            {
                return context.Set<AccumulatedMoneyDelivery>()
                    .Where(d => d.Type == type)
                    .OrderByDescending(d => d.CreatedAt)
                    .FirstOrDefault();
            }
        }


    }
}
