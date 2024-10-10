using Entities.Concrete;

namespace Business.Abstract
{
    public interface IPurchasedProductListDetailService
    {
        Task<List<PurchasedProductListDetail>> GetAllAsync();
        Task<List<PurchasedProductListDetail>> GetPurchasedProductListDetailByDateAsync(DateTime date);
        Task AddAsync(PurchasedProductListDetail purchasedProductListDetail);
        Task AddListAsync(List<PurchasedProductListDetail> purchasedProductListDetails);
        Task DeleteByIdAsync(int id, int userId);
        Task DeleteAsync(PurchasedProductListDetail purchasedProductListDetail);
        Task UpdateAsync(PurchasedProductListDetail purchasedProductListDetail);
        Task UpdateListAsync(List<PurchasedProductListDetail> purchasedProductListDetails);
        Task<PurchasedProductListDetail> GetByIdAsync(int id);
        Task<PurchasedProductListDetail> GetPurchasedProductListDetailByDateAndProductIdAsync(DateTime date, int productId);
        Task<bool> IsExistAsync(int productId, DateTime date);

    }
}
