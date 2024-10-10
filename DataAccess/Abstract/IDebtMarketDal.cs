using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IDebtMarketDal : IEntityRepository<DebtMarket>
    {

        bool IsExist(int id);
        Dictionary<int, decimal> GetTotalDebtsForMarkets();
    }
}
