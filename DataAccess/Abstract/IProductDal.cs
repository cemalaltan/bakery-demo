using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
    

        Task<List<Product>> GetNotAddedProductsByListAndCategoryId(int listId, int categoryId);
    }
}
