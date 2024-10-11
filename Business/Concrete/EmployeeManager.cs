using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {


        IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal EmployeeDal)
        {
            _employeeDal = EmployeeDal;
        }

        public void Add(Employee Employee)
        {
            _employeeDal.Add(Employee);
        }

        public void DeleteById(int id)
        {
            _employeeDal.DeleteById(id);
        }

        public void Delete(Employee Employee)
        {
            _employeeDal.Delete(Employee);
        }
        public List<Employee> GetAll()
        {
            return _employeeDal.GetAll();
        }

        public Employee GetById(int id)
        {
            return _employeeDal.Get(d => d.Id == id);
        }

        public void Update(Employee Employee)
        {
            _employeeDal.Update(Employee);
        }

        public List<Employee> GetAllActive()
        {
            return _employeeDal.GetAll(d => d.Status == true);
        }
    }

}
