using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfGivenProductsToServiceDal : EfEntityRepositoryBase<GivenProductsToService, BakeryAppContext>, IGivenProductsToServiceDal
    {
        private readonly BakeryAppContext _context;
        public EfGivenProductsToServiceDal(BakeryAppContext context) : base(context)
        {
            _context = context;
        }


        public List<GivenProductsToService> GetAllByDateAndServisTypeId(DateTime date, int servisTypeId)
        {
         
                var result = _context.GivenProductsToServices
                    .Where(x => x.Date.Date == date.Date && x.ServiceTypeId == servisTypeId)
                    .ToList();

                return result;
          
        }

        public List<GivenProductsToServiceTotalResultDto> GetTotalQuantityResultByDate(DateTime date)
        {
         
                var result = _context.GivenProductsToServices
                    .Where(x => x.Date.Date == date.Date)
                    .GroupBy(x => x.ServiceTypeId)
                    .Select(group => new GivenProductsToServiceTotalResultDto
                    {
                        ServiceTypeName = _context.ServiceTypes.FirstOrDefault(st => st.Id == group.Key).Name,
                        TotalQuantity = group.Sum(x => x.Quantity)
                    })
                    .ToList();

                return result;
          
        }
    }
}
