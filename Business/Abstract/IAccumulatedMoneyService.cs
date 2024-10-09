using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAccumulatedMoneyService
    {
        List<AccumulatedMoney> GetAllByType(int type);
        List<AccumulatedMoney> GetByDateRangeAndType(DateTime startDate, DateTime endDate, int type);
        void Add(AccumulatedMoney accumulatedMoney);
        void DeleteById(int id);
        void Delete(AccumulatedMoney accumulatedMoney);
        void Update(AccumulatedMoney accumulatedMoney);
        AccumulatedMoney GetById(int id);
        AccumulatedMoney GetByDateAndType(DateTime date, int type);

        decimal GetTotalAccumulatedMoneyByDateAndType(DateTime date, int type);
    }
}
