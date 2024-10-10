using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class SalaryPaymentManager : ISalaryPaymentService
    {
        private readonly ISalaryPaymentDal _salaryPaymentDal;
        private readonly IAdvanceService _advanceService;
        private readonly IEmployeeService _employeeService;

        public SalaryPaymentManager(ISalaryPaymentDal salaryPaymentDal, IAdvanceService advanceService, IEmployeeService employeeService)
        {
            _salaryPaymentDal = salaryPaymentDal;
            _advanceService = advanceService;
            _employeeService = employeeService;
        }

        public async Task AddAsync(SalaryPayment salaryPayment)
        {
            await _salaryPaymentDal.Add(salaryPayment);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _salaryPaymentDal.DeleteById(id);
        }

        public async Task DeleteAsync(SalaryPayment salaryPayment)
        {
            await _salaryPaymentDal.Delete(salaryPayment);
        }

        public async Task<List<SalaryPayment>> GetAllAsync()
        {
            return await _salaryPaymentDal.GetAll();
        }

        public async Task<List<SalaryPayment>> GetAllByDateAsync(int year, int month)
        {
            return await _salaryPaymentDal.GetAll(sp => sp.Year == year && sp.Month == month);
        }

        public async Task<SalaryPayment> GetByIdAsync(int id)
        {
            return await _salaryPaymentDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(SalaryPayment salaryPayment)
        {
            await _salaryPaymentDal.Update(salaryPayment);
        }

        public async Task<List<SalaryPaymentReport>> SalaryPaymentReportByDateAsync(int year, int month)
        {
            var dateNow = DateTime.Now;
            List<Employee> employees;

            // Get all advances and salary payments for the given year and month
            var advances = await _advanceService.GetAllAdvancesByDateAsync(year, month);
            var salaryPayments = await GetAllByDateAsync(year, month);

            if (dateNow.Month == month && dateNow.Year == year)
            {
                employees = await _employeeService.GetAllActiveAsync();
            }
            else
            {
                // For non-current months, get all employees who have either an advance or a salary payment
                var employeeIdsWithTransactions = advances.Select(a => a.EmployeeId)
                    .Concat(salaryPayments.Select(sp => sp.EmployeeId))
                    .Distinct()
                    .ToList();

                employees = (await _employeeService.GetAllAsync())
                    .Where(e => employeeIdsWithTransactions.Contains(e.Id))
                    .ToList();
            }

            if (employees.Count == 0)
            {
                Console.WriteLine($"No relevant employees found for {month}/{year}");
                return new List<SalaryPaymentReport>();
            }

            Console.WriteLine($"Relevant employees count: {employees.Count}");
            Console.WriteLine($"Advances count: {advances.Count}");
            Console.WriteLine($"Salary payments count: {salaryPayments.Count}");

            // Create dictionaries for quick lookups by EmployeeId
            var advancesLookup = advances.GroupBy(a => a.EmployeeId)
                                         .ToDictionary(g => g.Key, g => g.Sum(a => a.Amount));
            var salaryPaymentsLookup = salaryPayments.ToDictionary(sp => sp.EmployeeId, sp => sp);

            // Prepare salary payment reports
            var salaryPaymentReports = employees.Select(employee => new SalaryPaymentReport
            {
                Name = $"{employee.FirstName} {employee.LastName}",
                Role = employee.Title,
                Salary = employee.Salary,
                Advance = advancesLookup.TryGetValue(employee.Id, out var advanceAmount) ? advanceAmount : 0,
                PaidAmount = salaryPaymentsLookup.TryGetValue(employee.Id, out var salaryPayment) ? salaryPayment.PaidAmount : 0
            }).ToList();

            return salaryPaymentReports;
        }
    }
}
