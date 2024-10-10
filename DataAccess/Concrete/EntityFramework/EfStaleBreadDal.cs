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

        public bool IsExist(int doughFactoryProductId, DateTime date)
        {
         

                bool exists = _context.StaleBread
                    .Any(sp => sp.Date.Date == date.Date && sp.DoughFactoryProductId == doughFactoryProductId);

                return exists;
        
        }

        public List<StaleBreadDto> GetAllByDate(DateTime date)
        {
           
                var doughFactoryProducts = _context.StaleBread
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
                    }
                    )
                    .ToList();

                return doughFactoryProducts;
         
        }

        public List<DoughFactoryProduct> GetDoughFactoryProductsByDate(DateTime date)
        {
          
                var doughFactoryProducts = _context.DoughFactoryProducts
                   .Where(df => !_context.StaleBread.Any(sb => sb.DoughFactoryProductId == df.Id && sb.Date.Date == date.Date) && df.Status == true)
                    .ToList();

                return doughFactoryProducts;
            
        }

        public double GetReport(DateTime date)
        {
           
                var result = _context.StaleBread
                    .Where(sb => sb.Date.Date == date.Date)
                    .Join(_context.DoughFactoryProducts,
                        sb => sb.DoughFactoryProductId,
                        dfp => dfp.Id,
                        (sb, dfp) => new
                        {
                            BreadEquivalent = dfp.BreadEquivalent,
                            Quantity = sb.Quantity
                        })
                    .Sum(item => item.BreadEquivalent * item.Quantity);

                return result;
          
        }
    }
}
