using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAllServiceDal : EfEntityRepositoryBase<AllService, BakeryAppContext>, IAllServiceDal
    {
        public EfAllServiceDal(BakeryAppContext context) : base(context)
        {
        }

       

    }
}
