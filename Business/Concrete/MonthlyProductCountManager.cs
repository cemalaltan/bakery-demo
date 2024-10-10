using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class MonthlyProductCountManager : IMonthlyProductCountService
    {
        private readonly IMonthlyProductCountDal _monthlyProductCountDal;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public MonthlyProductCountManager(IMonthlyProductCountDal monthlyProductCountDal, ICategoryService categoryService, IProductService productService)
        {
            _monthlyProductCountDal = monthlyProductCountDal;
            _categoryService = categoryService;
            _productService = productService;
        }

        public async Task AddAsync(MonthlyProductCount monthlyProductCount)
        {
            // Step 1: Check if the record already exists
            bool exists = IsMonthlyProductCountExists(
                monthlyProductCount.Category,
                monthlyProductCount.Name,
                monthlyProductCount.Year,
                monthlyProductCount.Month
            );

            if (exists)
            {
                throw new InvalidOperationException("This product count has already been added for the specified month and year.");
            }

            await _monthlyProductCountDal.Add(monthlyProductCount);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _monthlyProductCountDal.DeleteById(id);
        }

        public async Task DeleteAsync(MonthlyProductCount monthlyProductCount)
        {
            await _monthlyProductCountDal.Delete(monthlyProductCount);
        }

        public async Task<List<MonthlyProductCount>> GetAllAsync()
        {
            return await _monthlyProductCountDal.GetAll();
        }

        public async Task<MonthlyProductCount> GetByIdAsync(int id)
        {
            return await _monthlyProductCountDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(MonthlyProductCount monthlyProductCount)
        {
            await _monthlyProductCountDal.Update(monthlyProductCount);
        }

        public async Task<Dictionary<string, List<Product>>> GetAllProductsAsync()
        {
            var activeCategories =  _categoryService.GetAllAsync().Result
                .Where(c => c.IsActive && c.Store)
                .ToList();

            // Step 2: Create a dictionary to hold the products grouped by category name
            var productMap = new Dictionary<string, List<Product>>();

            foreach (var category in activeCategories)
            {
                // Step 3: Get products for each category
                var products =  _productService.GetAllAsync().Result
                    .Where(p => p.CategoryId == category.Id && p.Status) // Assuming Status means active product
                    .ToList();

                // Step 4: Add the category name as the key and the list of products as the value
                if (products.Any())
                {
                    productMap[category.Name] = products;
                }
            }

            return productMap;
        }

        public async Task<Dictionary<string, List<MonthlyProductCount>>> GetAddedProductsAsync(int year, int month)
        {
            var monthlyProductCounts =  _monthlyProductCountDal.GetAll().Result
                .Where(mpc => mpc.Year == year && mpc.Month == month)
                .ToList();

            // Step 2: Create a dictionary to hold the product counts grouped by category name
            var productCountMap = new Dictionary<string, List<MonthlyProductCount>>();

            foreach (var productCount in monthlyProductCounts)
            {
                // Step 3: Add the MonthlyProductCount to the respective category in the dictionary
                if (!productCountMap.ContainsKey(productCount.Category))
                {
                    productCountMap[productCount.Category] = new List<MonthlyProductCount>();
                }

                productCountMap[productCount.Category].Add(productCount);
            }

            return productCountMap;
        }

        public bool IsMonthlyProductCountExists(string category, string productName, int year, int month)
        {
            return _monthlyProductCountDal.GetAll().Result
                .Any(mpc => mpc.Category == category
                            && mpc.Name == productName
                            && mpc.Year == year
                            && mpc.Month == month);
        }
    }
}
