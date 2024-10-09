using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMonthlyProductCountService
    {
        Dictionary<string, List<Product>> GetAllProducts();
        Dictionary<string, List<MonthlyProductCount>> GetAddedProducts(int year, int month);
        List<MonthlyProductCount> GetAll();
        void Add(MonthlyProductCount monthlyProductCount);
        void DeleteById(int id);
        void Delete(MonthlyProductCount monthlyProductCount);
        void Update(MonthlyProductCount monthlyProductCount);
        MonthlyProductCount GetById(int id);
    }
}
