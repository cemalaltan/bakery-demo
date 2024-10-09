using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IMarketContractService
    {
        List<MarketContract> GetAll();

        List<MarketContractDto> GetAllContractWithMarketsName();
        List<Market> GetMarketsNotHaveContract();
        void Add(MarketContract marketContract);
        void DeleteById(int id);
        void Delete(MarketContract marketContract);
        void Update(MarketContract marketContract);
        MarketContract GetById(int id);
        int GetIdByMarketId(int id);

        int GetMarketIdById(int id);
        decimal GetPriceById(int id);
    }
}
