using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfExpenseDal : EfEntityRepositoryBase<Expense, BakeryAppContext>, IExpenseDal
    {
        public EfExpenseDal(BakeryAppContext context) : base(context)
        {
        }

       

    }
}
