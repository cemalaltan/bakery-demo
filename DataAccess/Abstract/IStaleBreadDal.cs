using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface IStaleBreadDal : IEntityRepository<StaleBread>
    {
   
        Task<List<DoughFactoryProduct>> GetDoughFactoryProductsByDate(DateTime date);
        Task<double> GetReport(DateTime date);
        Task<List<StaleBreadDto>> GetAllByDate(DateTime date);
        Task<bool> IsExist(int doughFactoryProductId, DateTime date);
    }
}
