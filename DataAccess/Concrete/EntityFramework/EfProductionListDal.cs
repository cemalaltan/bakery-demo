using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductionListDal : EfEntityRepositoryBase<ProductionList, BakeryAppContext>, IProductionListDal
    {
        public EfProductionListDal(BakeryAppContext context) : base(context)
        {
        }

   

    }
}
