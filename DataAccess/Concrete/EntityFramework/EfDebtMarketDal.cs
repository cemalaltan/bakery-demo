using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfDebtMarketDal : EfEntityRepositoryBase<DebtMarket, BakeryAppContext>, IDebtMarketDal
    {
        private readonly BakeryAppContext _context;
        public EfDebtMarketDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public Dictionary<int, decimal> GetTotalDebtsForMarkets()
        {
        
                var totalAmounts = _context.DebtMarkets
                    .GroupBy(dm => dm.MarketId)
                    .Select(group => new { MarketId = group.Key, TotalAmount = group.Sum(dm => dm.Amount) })
                    .ToDictionary(result => result.MarketId, result => result.TotalAmount);

                return totalAmounts;
        
        }

        public bool IsExist(int id)
        {
        
                var entity = _context.DebtMarkets.FirstOrDefault(p => p.Id == id);
                return entity != null;

        }

    }
}
