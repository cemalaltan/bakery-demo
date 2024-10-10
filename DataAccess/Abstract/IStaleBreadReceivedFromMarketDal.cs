using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IStaleBreadReceivedFromMarketDal : IEntityRepository<StaleBreadReceivedFromMarket>
    {
        Task DeleteByDateAndMarketId(DateTime date, int marketId);
        Task<bool> IsExist(int marketId, DateTime date);
    }
}
