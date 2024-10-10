using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBreadPriceService
    {
        Task<List<BreadPrice>> GetAllAsync();
        Task AddAsync(BreadPrice breadPrice);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(BreadPrice breadPrice);
        Task UpdateAsync(BreadPrice breadPrice);
        Task<BreadPrice> GetByIdAsync(int id);
        Task<decimal> BreadPriceByDateAsync(DateTime date);
        Task<bool> IsExistByDateAsync(DateTime date);

    }

}
