using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IAccumulatedMoneyDal : IEntityRepository<AccumulatedMoney>
    {
        void DeleteById(int id);
    }
}
