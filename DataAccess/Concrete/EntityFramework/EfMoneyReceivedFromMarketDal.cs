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



        public bool IsExist(int marketId, DateTime date)
        {
                       
                return _context.Set<MoneyReceivedFromMarket>()
                                    .Any(m => m.MarketId == marketId && m.Date.Date == date.Date);
          
        }

    }
}
