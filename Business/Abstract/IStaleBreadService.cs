using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IStaleBreadService
    {
  Task<List<StaleBread>> GetAllAsync();
Task<List<StaleBreadDto>> GetAllByDateAsync(DateTime date);
Task AddAsync(StaleBread staleBread);
Task DeleteByIdAsync(int id);
Task DeleteAsync(StaleBread staleBread);
Task UpdateAsync(StaleBread staleBread);
Task<StaleBread> GetByIdAsync(int id);
Task<double> GetStaleBreadDailyReportAsync(DateTime date);
Task<List<DoughFactoryProduct>> GetDoughFactoryProductsAsync(DateTime date);
Task<bool> IsExistAsync(int doughFactoryProductId, DateTime date);

    }
}
