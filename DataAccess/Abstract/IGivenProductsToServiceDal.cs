using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IGivenProductsToServiceDal : IEntityRepository<GivenProductsToService>
    {
       
        Task<List<GivenProductsToServiceTotalResultDto>> GetTotalQuantityResultByDate(DateTime date);

        Task<List<GivenProductsToService>> GetAllByDateAndServisTypeId(DateTime date, int servisTypeId);
    }
}
