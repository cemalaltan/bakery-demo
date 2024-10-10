using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IProductionListDetailDal : IEntityRepository<ProductionListDetail>
    {

        List<GetAddedProductsDto> GetAddedProducts(int id);

        bool IsExist(int id, int listId);
        void AddList(List<ProductionListDetail> productionListDetail);
    }
}
