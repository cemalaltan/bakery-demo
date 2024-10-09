using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AdvanceManager : IAdvanceService
    {


        IAdvanceDal _advanceDal;
        ISalaryPaymentDal _salaryPaymentDal;

        public AdvanceManager(IAdvanceDal AdvanceDal, ISalaryPaymentDal salaryPaymentDal)
        {
            _advanceDal = AdvanceDal;
            _salaryPaymentDal = salaryPaymentDal;
        }

        public void Add(Advance Advance)
        {
            bool isSalaryPaid = _salaryPaymentDal.GetAll(d => d.EmployeeId == Advance.EmployeeId && d.Year == Advance.Year && d.Month == Advance.Month).Count > 0;
            if (isSalaryPaid)
            {
                throw new Exception("Salary has already been paid for this month!");
            }
            _advanceDal.Add(Advance);
        }

        public void DeleteById(int id)
        {
            _advanceDal.DeleteById(id);
        }

        public void Delete(Advance Advance)
        {
            _advanceDal.Delete(Advance);
        }
        public List<Advance> GetAll()
        {
            return _advanceDal.GetAll();
        }

        public Advance GetById(int id)
        {
            return _advanceDal.Get(d => d.Id == id);
        }

        public void Update(Advance Advance)
        {
            _advanceDal.Update(Advance);
        }

        public List<Advance> GetEmployeeAdvancesByDate(int id, int year, int month)
        {
            return _advanceDal.GetAll(d => d.EmployeeId == id && d.Year == year && d.Month == month);
        }

        public decimal GetTotalAdvancesAmountByDate(int id, int year, int month)
        {
            return _advanceDal.GetAll(d => d.EmployeeId == id && d.Year == year && d.Month == month).Sum(d => d.Amount);
        }

        public List<Advance> GetAllAdvancesByDate(int year, int month)
        {
            return _advanceDal.GetAll(d => d.Year == year && d.Month == month);
        }
    }

}
