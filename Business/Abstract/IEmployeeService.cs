using Entities.Concrete;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllAsync();
        Task<List<Employee>> GetAllActiveAsync();
        Task AddAsync(Employee employee);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task<Employee> GetByIdAsync(int id);

    }
}