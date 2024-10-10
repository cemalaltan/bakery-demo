using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceRemindMoneyDal : EfEntityRepositoryBase<ServiceRemindMoney, BakeryAppContext>, IServiceRemindMoneyDal
    {
        public EfServiceRemindMoneyDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
