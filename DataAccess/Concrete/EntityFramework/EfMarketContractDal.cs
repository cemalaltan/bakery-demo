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



        public async Task<List<Market>> GetMarketsNotHaveContract()
        {
            return await (from market in _context.Markets
                          where market.IsActive
                          join contract in _context.MarketContracts
                          on market.Id equals contract.MarketId into gj
                          from subContract in gj.DefaultIfEmpty()
                          where subContract == null
                          select market)
                         .ToListAsync();
        }

        public async Task<List<MarketContractDto>> GetAllContractWithMarketsName()
        {
            return await _context.MarketContractView.ToListAsync();
        }
    }
}
