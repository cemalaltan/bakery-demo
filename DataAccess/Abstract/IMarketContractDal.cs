using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IMarketContractDal : IEntityRepository<MarketContract>
    {
      
        Task<List<Market>> GetMarketsNotHaveContract();
        Task<List<MarketContractDto>> GetAllContractWithMarketsName();
    }
}
