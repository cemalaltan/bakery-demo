using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAdvanceService
    {
        List<Advance> GetAll();
        List<Advance> GetEmployeeAdvancesByDate(int id, int year, int month);
        List<Advance> GetAllAdvancesByDate(int year, int month);
        decimal GetTotalAdvancesAmountByDate(int id, int year, int month);
        void Add(Advance Advance);
        void DeleteById(int id);
        void Delete(Advance Advance);
        void Update(Advance Advance);
        Advance GetById(int id);
    }
}