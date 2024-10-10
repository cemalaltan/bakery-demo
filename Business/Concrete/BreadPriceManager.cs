using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BreadPriceManager : IBreadPriceService
    {
        private readonly IBreadPriceDal _breadPriceDal;

        public BreadPriceManager(IBreadPriceDal breadPriceDal)
        {
            _breadPriceDal = breadPriceDal;
        }

        public async Task AddAsync(BreadPrice breadPrice)
        {
            await _breadPriceDal.Add(breadPrice);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _breadPriceDal.DeleteById(id);
        }

        public async Task DeleteAsync(BreadPrice breadPrice)
        {
            await _breadPriceDal.Delete(breadPrice);
        }

        public async Task<List<BreadPrice>> GetAllAsync()
        {
            return (await _breadPriceDal.GetAll()).OrderByDescending(p => p.Date.Date).ToList();
        }

        public async Task<BreadPrice> GetByIdAsync(int id)
        {
            return await _breadPriceDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(BreadPrice breadPrice)
        {
            await _breadPriceDal.Update(breadPrice);
        }

        public async Task<decimal> BreadPriceByDateAsync(DateTime date)
        {
            BreadPrice searchedPrice = await _breadPriceDal.Get(p => p.Date.Date == date.Date);

            if (searchedPrice != null)
            {
                return searchedPrice.Price;
            }
            else
            {
                BreadPrice? previousPrice = (await _breadPriceDal
                                    .GetAll(p => p.Date.Date < date.Date))
                                    .OrderByDescending(p => p.Date.Date)
                                    .FirstOrDefault();

                if (previousPrice != null)
                {
                    return previousPrice.Price;
                }
                else
                {
                    return 0;
                }
            }
        }

        public async Task<bool> IsExistByDateAsync(DateTime date)
        {
            BreadPrice searchedPrice = await _breadPriceDal.Get(p => p.Date.Date == date.Date);
            return searchedPrice != null;
        }
    }
}
