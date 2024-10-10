using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IStaleProductDal : IEntityRepository<StaleProduct>
    {
       

        Dictionary<int, int> GetStaleProductsByDateAndCategory(DateTime date, int categoryId);
        List<StaleProductDto> GetByDateAndCategory(DateTime date, int categoryId);
        List<ProductNotAddedDto> GetProductsNotAddedToStale(DateTime date, int categoryId);
        bool IsExist(int productId, DateTime date);
    }
}
