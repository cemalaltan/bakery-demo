using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IMonthlyProductCountDal : IEntityRepository<MonthlyProductCount>
    {
        void DeleteById(int id);
    }
}
