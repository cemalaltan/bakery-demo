using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IAccumulatedMoneyDeliveryDal : IEntityRepository<AccumulatedMoneyDelivery>
    {
      

        AccumulatedMoneyDelivery? GetLatestDelivery(int type);
    }
}
