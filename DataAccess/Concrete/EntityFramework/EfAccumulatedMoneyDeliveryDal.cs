using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAccumulatedMoneyDeliveryDal : EfEntityRepositoryBase<AccumulatedMoneyDelivery, BakeryAppContext>, IAccumulatedMoneyDeliveryDal
    {
        private readonly BakeryAppContext _context;
        public EfAccumulatedMoneyDeliveryDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }


        public async Task<AccumulatedMoneyDelivery?> GetLatestDelivery(int type)
        {
            return await _context.Set<AccumulatedMoneyDelivery>()
                .Where(d => d.Type == type)
                .OrderByDescending(d => d.CreatedAt)
                .FirstOrDefaultAsync();
        }


    }
}
