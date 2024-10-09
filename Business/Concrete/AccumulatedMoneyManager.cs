using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AccumulatedMoneyManager : IAccumulatedMoneyService
    {


        IAccumulatedMoneyDal _accumulatedCashDal;
        
        public AccumulatedMoneyManager(IAccumulatedMoneyDal accumulatedCashDal)
        {
            _accumulatedCashDal = accumulatedCashDal;  
        }

        public void Add(AccumulatedMoney accumulatedCashDal)
        {
            _accumulatedCashDal.Add(accumulatedCashDal);
        }

        public void DeleteById(int id)
        {
            _accumulatedCashDal.DeleteById(id);
        }

        public void Delete(AccumulatedMoney accumulatedCashDal)
        {
            _accumulatedCashDal.Delete(accumulatedCashDal);
        }
        public List<AccumulatedMoney> GetAllByType(int type)
        {
           return _accumulatedCashDal.GetAll( a => a.Type == type);
        }

        public AccumulatedMoney GetById(int id)
        {
            return _accumulatedCashDal.Get(d => d.Id == id);
        }

        public void Update(AccumulatedMoney netEldenAmount)
        {
            _accumulatedCashDal.Update(netEldenAmount);
        }

        public List<AccumulatedMoney> GetByDateRangeAndType(DateTime startDate, DateTime endDate, int type)
        {
            return _accumulatedCashDal.GetAll(a => a.CreatedAt.Date >= startDate.Date && a.CreatedAt.Date <= endDate.Date.Date && a.Type == type);
        }

        public AccumulatedMoney GetByDateAndType(DateTime date, int type)
        {
            return _accumulatedCashDal.Get(a => a.CreatedAt.Date == date.Date && a.Type == type);
        }

        public decimal GetTotalAccumulatedMoneyByDateAndType(DateTime date, int type)
        {
            return _accumulatedCashDal.GetAll(a => a.CreatedAt > date && a.Type == type).Sum(a => a.Amount);
        }
    }
}
