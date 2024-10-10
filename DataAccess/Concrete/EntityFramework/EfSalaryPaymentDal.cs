using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfSalaryPaymentDal : EfEntityRepositoryBase<SalaryPayment, BakeryAppContext>, ISalaryPaymentDal
    {
        public EfSalaryPaymentDal(BakeryAppContext context) : base(context)
        {
        }
    }
}
