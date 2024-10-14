using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfMarketContractDal : EfEntityRepositoryBase<MarketContract, BakeryAppContext>, IMarketContractDal
    {

        public void DeleteById(int id)
        {
            using (BakeryAppContext context = new())
            {
                var deletedEntity = context.Entry(context.Set<MarketContract>().Find(id));
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();

            }
        }

        public List<MarketContractDto> GetAllContractWithMarketsName()
        {
            using (BakeryAppContext context = new())
            {
                var list = (from mc in context.MarketContracts
                            join m in context.Markets on mc.MarketId equals m.Id
                            select new MarketContractDto
                            {
                                Id = mc.Id,
                                ServiceProductId = mc.ServiceProductId,
                                Price = mc.Price,
                                MarketId = mc.MarketId,
                                MarketName = m.Name,
                                IsActive = mc.IsActive
                            }).ToList();
                return list;
            }
        }

        public List<Market> GetMarketsNotHaveContract()
        {
            using (BakeryAppContext context = new())
            {
                var marketList = (from market in context.Markets
                                  where market.IsActive
                                  join contract in context.MarketContracts
                                  on market.Id equals contract.MarketId into gj
                                  from subContract in gj.DefaultIfEmpty()
                                  where subContract == null
                                  select market).ToList();

                return marketList;
            }

        }
    }
}
