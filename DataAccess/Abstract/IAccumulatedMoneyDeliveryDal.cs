using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IAccumulatedMoneyDeliveryDal : IEntityRepository<AccumulatedMoneyDelivery>
    {
        void DeleteById(int id);

        AccumulatedMoneyDelivery? GetLatestDelivery(int type);
    }
}
