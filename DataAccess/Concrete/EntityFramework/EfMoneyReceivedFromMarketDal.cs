using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMoneyReceivedFromMarketDal : EfEntityRepositoryBase<MoneyReceivedFromMarket, BakeryAppContext>, IMoneyReceivedFromMarketDal
    {
        private readonly BakeryAppContext _context;

        public EfMoneyReceivedFromMarketDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }



        public async Task<bool> IsExist(int marketId, DateTime date)
        {

            return await _context.MoneyReceivedFromMarkets.AnyAsync(m => m.MarketId == marketId && m.Date.Date == date.Date);
          
        }

    }
}
