using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IProductionListDetailDal : IEntityRepository<ProductionListDetail>
    {

        Task<List<GetAddedProductsDto>> GetAddedProducts(int id);

        Task<bool> IsExist(int id, int listId);
        Task AddList(List<ProductionListDetail> productionListDetail);
    }
}
