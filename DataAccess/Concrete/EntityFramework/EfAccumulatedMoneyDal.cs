using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAccumulatedMoneyDal : EfEntityRepositoryBase<AccumulatedMoney, BakeryAppContext>, IAccumulatedMoneyDal
    {
        public EfAccumulatedMoneyDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
