using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMarketContractDal : EfEntityRepositoryBase<MarketContract, BakeryAppContext>, IMarketContractDal
    {
        private readonly BakeryAppContext _context;
        public EfMarketContractDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }



        public List<MarketContractDto> GetAllContractWithMarketsName()
        {
        
                var list = _context.MarketContractView.ToList();
                return list;
    
        }

        public List<Market> GetMarketsNotHaveContract()
        {
           
                var marketList = (from market in _context.Markets
                                  where market.IsActive
                                  join contract in _context.MarketContracts
                                  on market.Id equals contract.MarketId into gj
                                  from subContract in gj.DefaultIfEmpty()
                                  where subContract == null
                                  select market).ToList();

                return marketList;
          
            
        }
    }
}
