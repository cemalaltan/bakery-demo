using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IProductsCountingDal : IEntityRepository<ProductsCounting>
    {
        Task AddList(List<ProductsCounting> productsCountings);
        Task<Dictionary<int, int>> GetDictionaryProductsCountingByDateAndCategory(DateTime date, int categoryId);
        Task<List<ProductsCountingDto>> GetProductsCountingByDateAndCategory(DateTime date, int categoryId);
    }
}
