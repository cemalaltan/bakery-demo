using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AdvanceManager : IAdvanceService
    {
        private readonly IAdvanceDal _advanceDal;
        private readonly ISalaryPaymentDal _salaryPaymentDal;

        public AdvanceManager(IAdvanceDal advanceDal, ISalaryPaymentDal salaryPaymentDal)
        {
            _advanceDal = advanceDal;
            _salaryPaymentDal = salaryPaymentDal;
        }

        public async Task AddAsync(Advance advance)
        {
            // Check if the salary has already been paid
            var salaryPayments = await _salaryPaymentDal.GetAll(d =>
                d.EmployeeId == advance.EmployeeId &&
                d.Year == advance.Year &&
                d.Month == advance.Month);

            bool isSalaryPaid = salaryPayments.Count > 0;

            if (isSalaryPaid)
            {
                throw new Exception("Salary has already been paid for this month!");
            }

            // Add the advance asynchronously
            await _advanceDal.Add(advance);
        }


        public async Task DeleteByIdAsync(int id)
        {
            await Task.Run(() => _advanceDal.DeleteById(id));
        }

        public async Task DeleteAsync(Advance advance)
        {
            await Task.Run(() => _advanceDal.Delete(advance));
        }

        public async Task<List<Advance>> GetAllAsync()
        {
            return await Task.Run(() => _advanceDal.GetAll());
        }

        public async Task<Advance> GetByIdAsync(int id)
        {
            return await Task.Run(() => _advanceDal.Get(d => d.Id == id));
        }

        public async Task UpdateAsync(Advance advance)
        {
            await Task.Run(() => _advanceDal.Update(advance));
        }

        public async Task<List<Advance>> GetEmployeeAdvancesByDateAsync(int id, int year, int month)
        {
            return await Task.Run(() => _advanceDal.GetAll(d =>
                d.EmployeeId == id &&
                d.Year == year &&
                d.Month == month));
        }

        public async Task<decimal> GetTotalAdvancesAmountByDateAsync(int id, int year, int month)
        {
            return await Task.Run(() =>
                _advanceDal.GetAll(d =>
                    d.EmployeeId == id &&
                    d.Year == year &&
                    d.Month == month).Result.Sum(d => d.Amount));
        }

        public async Task<List<Advance>> GetAllAdvancesByDateAsync(int year, int month)
        {
            return await Task.Run(() => _advanceDal.GetAll(d =>
                d.Year == year &&
                d.Month == month));
        }
    }
}
