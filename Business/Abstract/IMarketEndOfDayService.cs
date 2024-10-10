using Entities.DTOs;
using static Business.Concrete.MarketEndOfDayService;

namespace Business.Abstract
{
    public interface IMarketEndOfDayService
    {
        Task<List<PaymentMarket>> CalculateMarketEndOfDayAsync(DateTime date);
        Task<List<MarketBreadDetails>> MarketsEndOfDayCalculationWithDetailAsync(DateTime date);
        Task<decimal> TotalAmountFromMarketsAsync(DateTime date);

    }

}
