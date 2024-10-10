using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IServiceListDetailDal : IEntityRepository<ServiceListDetail>
    {
     
        bool IsExist(int serviceListId, int marketContractId);

        void DeleteByServiceListIdAndMarketContracId(int serviceListId, int marketContracId);

    }
}
