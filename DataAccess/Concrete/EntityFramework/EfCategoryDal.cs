using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfCategoryDal : EfEntityRepositoryBase<Category, BakeryAppContext>, ICategoryDal
    {
        public EfCategoryDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
