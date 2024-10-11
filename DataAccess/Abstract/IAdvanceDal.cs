using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IAdvanceDal : IEntityRepository<Advance>
    {
        void DeleteById(int id);
    }
}
