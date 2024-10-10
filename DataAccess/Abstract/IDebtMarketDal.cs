using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IDebtMarketDal : IEntityRepository<DebtMarket>
    {

        Task<bool> IsExist(int id);
        Task<Dictionary<int, decimal>> GetTotalDebtsForMarkets();
    }
}
