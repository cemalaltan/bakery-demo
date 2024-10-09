using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IMarketContractDal : IEntityRepository<MarketContract>
    {
        void DeleteById(int id);
        List<Market> GetMarketsNotHaveContract();
        List<MarketContractDto> GetAllContractWithMarketsName();
    }
}
