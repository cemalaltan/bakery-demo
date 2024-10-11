using Entities.Concrete;

namespace Business.Abstract
{
    public interface ISalaryPaymentService
    {
        List<SalaryPayment> GetAll();
        void Add(SalaryPayment SalaryPayment);
        void DeleteById(int id);
        void Delete(SalaryPayment SalaryPayment);
        void Update(SalaryPayment SalaryPayment);
        SalaryPayment GetById(int id);
        List<SalaryPaymentReport> SalaryPaymentReportByDate(int year, int month);
    }
}