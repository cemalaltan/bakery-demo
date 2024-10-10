using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IProductionListDetailService
    {
        Task<List<ProductionListDetail>> GetAllAsync();
        Task<List<GetAddedProductsDto>> GetProductsByListIdAsync(int id);
        Task<bool> IsExistAsync(int id, int listId);
        Task AddAsync(ProductionListDetail productionListDetail);
        Task AddListAsync(List<ProductionListDetail> productionListDetail);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(ProductionListDetail productionListDetail);
        Task UpdateAsync(ProductionListDetail productionListDetail);
        Task<ProductionListDetail> GetByIdAsync(int id);
        Task<ProductionListDetail> GetProductionListDetailByDateAndProductIdAsync(DateTime date, Product product);

    }
}
