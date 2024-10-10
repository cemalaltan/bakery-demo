using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IMoneyReceivedFromMarketDal : IEntityRepository<MoneyReceivedFromMarket>
    {
      
        Task<bool> IsExist(int marketId, DateTime date);
    }
}
