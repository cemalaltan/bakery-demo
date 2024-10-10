using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IStaleProductDal : IEntityRepository<StaleProduct>
    {
       

        Task<Dictionary<int, int>> GetStaleProductsByDateAndCategory(DateTime date, int categoryId);
        Task<List<StaleProductDto>> GetByDateAndCategory(DateTime date, int categoryId);
        Task<List<ProductNotAddedDto>> GetProductsNotAddedToStale(DateTime date, int categoryId);
        Task<bool> IsExist(int productId, DateTime date);
    }
}
