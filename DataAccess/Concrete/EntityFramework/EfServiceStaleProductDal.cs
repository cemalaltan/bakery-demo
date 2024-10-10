using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceStaleProductDal : EfEntityRepositoryBase<ServiceStaleProduct, BakeryAppContext>, IServiceStaleProductDal
    {
        public EfServiceStaleProductDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
