using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IDoughFactoryListDetailDal : IEntityRepository<DoughFactoryListDetail>
    {
    
        bool IsExist(int id, int listId);
    }
}
