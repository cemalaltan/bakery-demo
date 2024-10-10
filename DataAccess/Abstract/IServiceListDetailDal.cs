using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IServiceListDetailDal : IEntityRepository<ServiceListDetail>
    {
     
        Task<bool> IsExist(int serviceListId, int marketContractId);

        Task DeleteByServiceListIdAndMarketContracId(int serviceListId, int marketContracId);

    }
}
