using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceListDal : EfEntityRepositoryBase<ServiceList, BakeryAppContext>, IServiceListDal
    {
        public EfServiceListDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
