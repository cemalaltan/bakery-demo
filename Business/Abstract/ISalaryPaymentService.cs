using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISalaryPaymentService
    {
        Task AddAsync(SalaryPayment salaryPayment);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(SalaryPayment salaryPayment);
        Task<List<SalaryPayment>> GetAllAsync();
        Task<List<SalaryPayment>> GetAllByDateAsync(int year, int month);
        Task<SalaryPayment> GetByIdAsync(int id);
        Task UpdateAsync(SalaryPayment salaryPayment);
        Task<List<SalaryPaymentReport>> SalaryPaymentReportByDateAsync(int year, int month);
    }
}