using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private readonly IEmployeeDal _employeeDal;

        public EmployeeManager(IEmployeeDal employeeDal)
        {
            _employeeDal = employeeDal;
        }

        public async Task AddAsync(Employee employee)
        {
            await _employeeDal.Add(employee);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _employeeDal.DeleteById(id);
        }

        public async Task DeleteAsync(Employee employee)
        {
            await _employeeDal.Delete(employee);
        }

        public async Task<List<Employee>> GetAllAsync()
        {
            return await _employeeDal.GetAll();
        }

        public async Task<Employee> GetByIdAsync(int id)
        {
            return await _employeeDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(Employee employee)
        {
            await _employeeDal.Update(employee);
        }

        public async Task<List<Employee>> GetAllActiveAsync()
        {
            return await _employeeDal.GetAll(d => d.Status == true);
        }
    }
}
