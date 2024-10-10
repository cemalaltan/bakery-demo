using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMarketDal : EfEntityRepositoryBase<Market, BakeryAppContext>, IMarketDal
    {
        public EfMarketDal(BakeryAppContext context) : base(context)
        {
        }



    }
}
