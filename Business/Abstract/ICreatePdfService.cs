using Entities.DTOs;

namespace Business.Abstract
{
	public interface ICreatePdfService
	{
        Task<byte[]> EndOfDayAccountCreatePdfAsync(DateTime date, EndOfDayResult endOfDayResult, decimal productsSoldInTheBakery);
        Task<byte[]> CreatePdfAsync(DateTime date);
        Task<byte[]> CreatePdfForHamurhaneAsync(DateTime date);
        Task<byte[]> CreatePdfForMarketServiceAsync(DateTime date);


    }
}
