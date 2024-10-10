using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfBreadCountingDal : EfEntityRepositoryBase<BreadCounting, BakeryAppContext>, IBreadCountingDal
    {
        public EfBreadCountingDal(BakeryAppContext context) : base(context)
        {
        }

    

    }
}
