namespace Business.Abstract;

public interface IGeneralAccountPdfService
{
   byte[] GetGeneralAccountPdfByDate(DateTime date);
}