using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMarketService
    {
        List<Market> GetAll();
        List<Market> GetAllActive();
        void Add(Market market);
        void DeleteById(int id);
        void Delete(Market market);
        void Update(Market market);
        Market GetById(int id);
        string GetNameById(int id);
    }
}
