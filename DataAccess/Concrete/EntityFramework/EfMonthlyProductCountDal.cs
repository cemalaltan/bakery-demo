using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMonthlyProductCountDal : EfEntityRepositoryBase<MonthlyProductCount, BakeryAppContext>, IMonthlyProductCountDal
    {
        public EfMonthlyProductCountDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
