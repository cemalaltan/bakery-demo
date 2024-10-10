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

        public void DeleteByDateAndMarketId(DateTime date, int marketId)
        {
          
                var deletedEntity = _context.Entry(_context.Set<StaleBreadReceivedFromMarket>().FirstOrDefault(s => s.Date.Date == date.Date && s.MarketId == marketId));
                if (deletedEntity != null)
                {
                    deletedEntity.State = EntityState.Deleted;
                _context.SaveChanges();
                }
           
        }

        public bool IsExist(int marketId, DateTime date)
        {
          
                return _context.Set<StaleBreadReceivedFromMarket>()
                    .Any(s => s.MarketId == marketId && s.Date.Date == date.Date);
           
        }
    }
}
