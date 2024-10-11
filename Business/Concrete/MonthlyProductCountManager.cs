using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MonthlyProductCountManager : IMonthlyProductCountService
    {


        IMonthlyProductCountDal _monthlyProductCountDal;
        ICategoryService _categoryService;
        IProductService _productService;

        public MonthlyProductCountManager(IMonthlyProductCountDal monthlyProductCountDal, ICategoryService categoryService, IProductService productService)
        {
            _monthlyProductCountDal = monthlyProductCountDal;
            _categoryService = categoryService;
            _productService = productService;
        }

        public void Add(MonthlyProductCount monthlyProductCount)
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

   
            _monthlyProductCountDal.Add(monthlyProductCount);
        }

        public void DeleteById(int id)
        {
            _monthlyProductCountDal.DeleteById(id);
        }

        public void Delete(MonthlyProductCount MonthlyProductCount)
        {
            _monthlyProductCountDal.Delete(MonthlyProductCount);
        }
        public List<MonthlyProductCount> GetAll()
        {
           return _monthlyProductCountDal.GetAll();
        }

        public MonthlyProductCount GetById(int id)
        {
            return _monthlyProductCountDal.Get(d => d.Id == id);
        }

        public void Update(MonthlyProductCount MonthlyProductCount)
        {
            _monthlyProductCountDal.Update(MonthlyProductCount);
        }

        public Dictionary<string, List<Product>> GetAllProducts()
        {
            var activeCategories = _categoryService.GetAll()
       .Where(c => c.IsActive && c.Store)  
       .ToList();

            // Step 2: Create a dictionary to hold the products grouped by category name
            var productMap = new Dictionary<string, List<Product>>();

            foreach (var category in activeCategories)
            {
                // Step 3: Get products for each category
                var products = _productService.GetAll()
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

        public Dictionary<string, List<MonthlyProductCount>> GetAddedProducts(int year, int month)
        {
            var monthlyProductCounts = _monthlyProductCountDal.GetAll()
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
            return _monthlyProductCountDal.GetAll()
                .Any(mpc => mpc.Category == category
                            && mpc.Name == productName
                            && mpc.Year == year
                            && mpc.Month == month);
        }
    }



}
