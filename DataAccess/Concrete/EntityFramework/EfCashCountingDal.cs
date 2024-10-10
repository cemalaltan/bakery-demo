using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCashCountingDal : EfEntityRepositoryBase<CashCounting, BakeryAppContext>, ICashCountingDal
    {
        public EfCashCountingDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
