using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfServiceListDetailDal : EfEntityRepositoryBase<ServiceListDetail, BakeryAppContext>, IServiceListDetailDal
    {
        private readonly BakeryAppContext _context;
        public EfServiceListDetailDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }

        public void DeleteByServiceListIdAndMarketContracId(int serviceListId, int marketContracId)
        {
           
                var deletedEntity = _context.Entry(_context.Set<ServiceListDetail>().FirstOrDefault(s=>s.ServiceListId ==serviceListId && s.MarketContractId == marketContracId));
                if (deletedEntity != null)
                {
                    deletedEntity.State = EntityState.Deleted;
                _context.SaveChanges();
                }                
          
        }

        public bool IsExist(int serviceListId, int marketContractId)
        {
                        
                return _context.Set<ServiceListDetail>().Any(s => s.MarketContractId == marketContractId && s.ServiceListId == serviceListId);
       
        }

    }
}
