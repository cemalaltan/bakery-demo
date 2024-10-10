using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfPurchasedProductListDal : EfEntityRepositoryBase<PurchasedProductList, BakeryAppContext>, IPurchasedProductListDal
    {
        public EfPurchasedProductListDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
