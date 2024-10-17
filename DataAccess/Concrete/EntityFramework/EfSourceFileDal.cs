using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
public class EfSourceFileDal : EfEntityRepositoryBase<SourceFile, BakeryAppContext>, ISourceFileDal
{
    public void DeleteById(int id)
    {
        using (BakeryAppContext context = new())
        {
            var deletedEntity = context.Entry(context.Set<SourceFile>().Find(id));
            deletedEntity.State = EntityState.Deleted;
            context.SaveChanges();

        }
    }
}
}