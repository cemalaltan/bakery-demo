using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceListDetailDal : EfEntityRepositoryBase<ServiceListDetail, BakeryAppContext>, IServiceListDetailDal
    {
        private readonly BakeryAppContext _context;
        public EfServiceListDetailDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsExist(int serviceListId, int marketContractId)
        {
            return await _context.Set<ServiceListDetail>()
                .AnyAsync(s => s.MarketContractId == marketContractId && s.ServiceListId == serviceListId);
        }

        public async Task DeleteByServiceListIdAndMarketContracId(int serviceListId, int marketContracId)
        {
            var entity = await _context.Set<ServiceListDetail>()
                .FirstOrDefaultAsync(s => s.ServiceListId == serviceListId && s.MarketContractId == marketContracId);

            if (entity != null)
            {
                _context.Set<ServiceListDetail>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

    }
}
