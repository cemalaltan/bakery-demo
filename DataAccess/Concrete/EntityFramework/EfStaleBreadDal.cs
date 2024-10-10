using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{


    public class EfStaleBreadDal : EfEntityRepositoryBase<StaleBread, BakeryAppContext>, IStaleBreadDal
    {
        private readonly BakeryAppContext _context;
        public EfStaleBreadDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsExist(int doughFactoryProductId, DateTime date)
        {
            return await _context.StaleBread
                .AnyAsync(sp => sp.Date.Date == date.Date && sp.DoughFactoryProductId == doughFactoryProductId);
        }

        public async Task<List<StaleBreadDto>> GetAllByDate(DateTime date)
        {
            return await _context.StaleBread
                .Where(x => x.Date.Date == date.Date)
                .Join(_context.DoughFactoryProducts,
                    x => x.DoughFactoryProductId,
                    df => df.Id,
                    (x, df) => new StaleBreadDto
                    {
                        Id = x.Id,
                        Quantity = x.Quantity,
                        Date = x.Date,
                        DoughFactoryProductId = x.DoughFactoryProductId,
                        DoughFactoryProductName = df.Name
                    })
                .ToListAsync();
        }

        public async Task<List<DoughFactoryProduct>> GetDoughFactoryProductsByDate(DateTime date)
        {
            return await _context.DoughFactoryProducts
                .Where(df => !_context.StaleBread.Any(sb => sb.DoughFactoryProductId == df.Id && sb.Date.Date == date.Date) && df.Status == true)
                .ToListAsync();
        }

        public async Task<double> GetReport(DateTime date)
        {
            return await _context.StaleBread
                .Where(sb => sb.Date.Date == date.Date)
                .Join(_context.DoughFactoryProducts,
                    sb => sb.DoughFactoryProductId,
                    dfp => dfp.Id,
                    (sb, dfp) => new
                    {
                        BreadEquivalent = dfp.BreadEquivalent,
                        Quantity = sb.Quantity
                    })
                .SumAsync(item => item.BreadEquivalent * item.Quantity);
        }
    }
}
