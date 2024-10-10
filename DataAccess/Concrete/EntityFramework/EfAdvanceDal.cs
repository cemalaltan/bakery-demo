using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfAdvanceDal : EfEntityRepositoryBase<Advance, BakeryAppContext>, IAdvanceDal
    {
        public EfAdvanceDal(BakeryAppContext context) : base(context)
        {
        }

    }
}
