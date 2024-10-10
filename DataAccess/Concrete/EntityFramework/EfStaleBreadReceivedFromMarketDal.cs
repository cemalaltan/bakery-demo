using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfStaleBreadReceivedFromMarketDal : EfEntityRepositoryBase<StaleBreadReceivedFromMarket, BakeryAppContext>, IStaleBreadReceivedFromMarketDal
    {
        private readonly BakeryAppContext _context;
        public EfStaleBreadReceivedFromMarketDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task DeleteByDateAndMarketId(DateTime date, int marketId)
        {
            var entity = await _context.Set<StaleBreadReceivedFromMarket>()
                .FirstOrDefaultAsync(s => s.Date.Date == date.Date && s.MarketId == marketId);

            if (entity != null)
            {
                _context.Set<StaleBreadReceivedFromMarket>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> IsExist(int marketId, DateTime date)
        {
            return await _context.Set<StaleBreadReceivedFromMarket>()
                .AnyAsync(s => s.MarketId == marketId && s.Date.Date == date.Date);
        }
    }
}
