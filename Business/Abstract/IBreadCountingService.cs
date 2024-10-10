using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBreadCountingService
    {
        Task<List<BreadCounting>> GetAllAsync();
        Task<BreadCounting> GetBreadCountingByDateAsync(DateTime date);
        Task AddAsync(BreadCounting breadCounting);
        Task AddListAsync(List<BreadCounting> breadCounting);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(BreadCounting breadCounting);
        Task UpdateAsync(BreadCounting breadCounting);
        Task<BreadCounting> GetByIdAsync(int id);

    }
}
