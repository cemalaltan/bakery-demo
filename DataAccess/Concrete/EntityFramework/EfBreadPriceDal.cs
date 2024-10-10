using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBreadPriceDal : EfEntityRepositoryBase<BreadPrice, BakeryAppContext>, IBreadPriceDal
    {
        public EfBreadPriceDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
