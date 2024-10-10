using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfEmployeeDal : EfEntityRepositoryBase<Employee, BakeryAppContext>, IEmployeeDal
    {
        public EfEmployeeDal(BakeryAppContext context) : base(context)
        {
        }



    }
}
