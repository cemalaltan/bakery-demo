using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceTypeDal : EfEntityRepositoryBase<ServiceType, BakeryAppContext>, IServiceTypeDal
    {
        public EfServiceTypeDal(BakeryAppContext context) : base(context)
        {
        }

    
    }
}
