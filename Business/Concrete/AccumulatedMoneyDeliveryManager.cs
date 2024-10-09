using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AccumulatedMoneyDeliveryManager : IAccumulatedMoneyDeliveryService
    {


        IAccumulatedMoneyDeliveryDal _deliveryDal;

        public AccumulatedMoneyDeliveryManager(IAccumulatedMoneyDeliveryDal deliveryDal, IAccumulatedMoneyDal netEldenAmountDal)
        {
            _deliveryDal = deliveryDal;
        }

        public void Add(AccumulatedMoneyDelivery delivery)
        {
            _deliveryDal.Add(delivery);
        }

        public void DeleteById(int id)
        {
            _deliveryDal.DeleteById(id);
        }

        public void Delete(AccumulatedMoneyDelivery delivery)
        {
            _deliveryDal.Delete(delivery);
        }
        public List<AccumulatedMoneyDelivery> GetAll()
        {
            return _deliveryDal.GetAll();
        }

        public AccumulatedMoneyDelivery GetById(int id)
        {
            return _deliveryDal.Get(d => d.Id == id);
        }

        public void Update(AccumulatedMoneyDelivery delivery)
        {
            _deliveryDal.Update(delivery);
        }


        public List<AccumulatedMoneyDelivery> GetBetweenDates(DateTime startDate, DateTime endDate)
        {
            return _deliveryDal.GetAll(delivery => delivery.CreatedAt >= startDate && delivery.CreatedAt <= endDate.AddDays(1));
        }

        public AccumulatedMoneyDelivery? GetLastDelivery(int type)
        {
            return _deliveryDal.GetLatestDelivery(type);
        }
    }
}
