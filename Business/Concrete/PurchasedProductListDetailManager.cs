using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PurchasedProductListDetailManager : IPurchasedProductListDetailService
    {
        private readonly IPurchasedProductListDetailDal _purchasedProductListDetailDal;

        public PurchasedProductListDetailManager(IPurchasedProductListDetailDal purchasedProductListDetailDal)
        {
            _purchasedProductListDetailDal = purchasedProductListDetailDal;
        }

        public async Task AddAsync(PurchasedProductListDetail purchasedProductListDetail)
        {
            await _purchasedProductListDetailDal.Add(purchasedProductListDetail);
        }

        public async Task DeleteByIdAsync(int id, int userId)
        {
        //     _purchasedProductListDetailDal.DeleteById(id, userId);
        }

        public async Task DeleteAsync(PurchasedProductListDetail purchasedProductListDetail)
        {
            await _purchasedProductListDetailDal.Delete(purchasedProductListDetail);
        }

        public async Task<List<PurchasedProductListDetail>> GetAllAsync()
        {
            return await _purchasedProductListDetailDal.GetAll();
        }

        public async Task<PurchasedProductListDetail> GetByIdAsync(int id)
        {
            return await _purchasedProductListDetailDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(PurchasedProductListDetail purchasedProductListDetail)
        {
            await _purchasedProductListDetailDal.Update(purchasedProductListDetail);
        }

        public async Task<List<PurchasedProductListDetail>> GetPurchasedProductListDetailByDateAsync(DateTime date)
        {
            return await _purchasedProductListDetailDal.GetAll(p => p.Date.Date == date.Date);
        }

        public async Task AddListAsync(List<PurchasedProductListDetail> purchasedProductListDetails)
        {
            foreach (var detail in purchasedProductListDetails)
            {
                await _purchasedProductListDetailDal.Add(detail);
            }
        }

        public async Task<bool> IsExistAsync(int productId, DateTime date)
        {
            return await _purchasedProductListDetailDal.Get(d => d.ProductId == productId && d.Date.Date == date.Date) != null;
        }

        public async Task UpdateListAsync(List<PurchasedProductListDetail> purchasedProductListDetails)
        {
            foreach (var detail in purchasedProductListDetails)
            {
                await _purchasedProductListDetailDal.Update(detail);
            }
        }

        public async Task<PurchasedProductListDetail> GetPurchasedProductListDetailByDateAndProductIdAsync(DateTime date, int productId)
        {
            var purchasedProductListDetail = await _purchasedProductListDetailDal.Get(d => d.ProductId == productId && d.Date.Date == date);
            return purchasedProductListDetail ?? new PurchasedProductListDetail { Price = 0, Quantity = 0 };
        }
    }
}
