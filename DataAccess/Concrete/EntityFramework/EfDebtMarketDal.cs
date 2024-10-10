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

        public async Task<bool> IsExist(int id)
        {
            return await _context.DebtMarkets.AnyAsync(p => p.Id == id);
        }

        public async Task<Dictionary<int, decimal>> GetTotalDebtsForMarkets()
        {
            var totalAmounts = await _context.DebtMarkets
                .GroupBy(dm => dm.MarketId)
                .Select(group => new
                {
                    MarketId = group.Key,
                    TotalAmount = group.Sum(dm => dm.Amount)
                })
                .ToDictionaryAsync(result => result.MarketId, result => result.TotalAmount);

            return totalAmounts;
        }

    }
}
