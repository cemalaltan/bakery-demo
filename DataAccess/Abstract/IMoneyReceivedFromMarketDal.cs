using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IMoneyReceivedFromMarketDal : IEntityRepository<MoneyReceivedFromMarket>
    {
      
        bool IsExist(int marketId, DateTime date);
    }
}
