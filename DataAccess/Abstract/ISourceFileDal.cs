
using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ISourceFileDal : IEntityRepository<SourceFile>
    {
        void DeleteById(int id);
    }
}