using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceProductDal : EfEntityRepositoryBase<ServiceProduct, BakeryAppContext>, IServiceProductDal
    {
        public EfServiceProductDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
