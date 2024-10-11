using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ISalaryPaymentDal : IEntityRepository<SalaryPayment>
    {
        void DeleteById(int id);
    }
}
