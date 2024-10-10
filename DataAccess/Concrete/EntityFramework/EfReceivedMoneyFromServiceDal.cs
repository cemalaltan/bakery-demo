using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReceivedMoneyFromServiceDal : EfEntityRepositoryBase<ReceivedMoneyFromService, BakeryAppContext>, IReceivedMoneyFromServiceDal
    {
        public EfReceivedMoneyFromServiceDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
