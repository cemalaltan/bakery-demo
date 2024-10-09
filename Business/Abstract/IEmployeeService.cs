using Entities.Concrete;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        List<Employee> GetAll();
        List<Employee> GetAllActive();
        void Add(Employee Employee);
        void DeleteById(int id);
        void Delete(Employee Employee);
        void Update(Employee Employee);
        Employee GetById(int id);
    }
}