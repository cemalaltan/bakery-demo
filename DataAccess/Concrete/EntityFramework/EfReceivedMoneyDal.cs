using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfReceivedMoneyDal : EfEntityRepositoryBase<ReceivedMoney, BakeryAppContext>, IReceivedMoneyDal
    {
        public EfReceivedMoneyDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
