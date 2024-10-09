using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class SalaryPaymentManager : ISalaryPaymentService
    {


        ISalaryPaymentDal _salaryPaymentDal;
        IAdvanceService _advanceService;
        IEmployeeService _employeeService;

        public SalaryPaymentManager(ISalaryPaymentDal SalaryPaymentDal, IAdvanceService advanceService, IEmployeeService employeeService)
        {
            _salaryPaymentDal = SalaryPaymentDal;
            _advanceService = advanceService;
            _employeeService = employeeService;
        }

        public void Add(SalaryPayment SalaryPayment)
        {
            _salaryPaymentDal.Add(SalaryPayment);
        }

        public void DeleteById(int id)
        {
            _salaryPaymentDal.DeleteById(id);
        }

        public void Delete(SalaryPayment SalaryPayment)
        {
            _salaryPaymentDal.Delete(SalaryPayment);
        }
        public List<SalaryPayment> GetAll()
        {
            return _salaryPaymentDal.GetAll();
        }

        public List<SalaryPayment> GetAllByDate(int year, int month)
        {
            return _salaryPaymentDal.GetAll(sp => sp.Year == year && sp.Month == month);
        }
        public SalaryPayment GetById(int id)
        {
            return _salaryPaymentDal.Get(d => d.Id == id);
        }

        public void Update(SalaryPayment SalaryPayment)
        {
            _salaryPaymentDal.Update(SalaryPayment);
        }

        public List<SalaryPaymentReport> SalaryPaymentReportByDate(int year, int month)
        {
            var dateNow = DateTime.Now;
            List<Employee> employees;

            // Get all advances and salary payments for the given year and month
            var advances = _advanceService.GetAllAdvancesByDate(year, month);
            var salaryPayments = GetAllByDate(year, month);

            if (dateNow.Month == month && dateNow.Year == year)
            {
                employees = _employeeService.GetAllActive();
            }
            else
            {
                // For non-current months, get all employees who have either an advance or a salary payment
                var employeeIdsWithTransactions = advances.Select(a => a.EmployeeId)
                    .Concat(salaryPayments.Select(sp => sp.EmployeeId))
                    .Distinct()
                    .ToList();

                employees = _employeeService.GetAll()
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
