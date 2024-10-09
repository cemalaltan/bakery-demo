using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAccumulatedMoneyDeliveryService
    {

        
        List<AccumulatedMoneyDelivery> GetAll();
        List<AccumulatedMoneyDelivery> GetBetweenDates(DateTime startDate, DateTime endDate);
        AccumulatedMoneyDelivery? GetLastDelivery(int type);
        void Add(AccumulatedMoneyDelivery delivery);
        void DeleteById(int id);
        void Delete(AccumulatedMoneyDelivery delivery);
        void Update(AccumulatedMoneyDelivery delivery);
        AccumulatedMoneyDelivery GetById(int id);
    }
}
