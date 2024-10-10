using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPurchasedProductListDetailDal : EfEntityRepositoryBase<PurchasedProductListDetail, BakeryAppContext>, IPurchasedProductListDetailDal
    {
        public EfPurchasedProductListDetailDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
