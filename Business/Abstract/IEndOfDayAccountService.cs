using Entities.DTOs;

namespace Business.Abstract
{
    public interface IEndOfDayAccountService
    {

        Task<EndOfDayResult> GetEndOfDayAccountDetailAsync(DateTime date);
        Task<decimal> GetProductsSoldInTheBakeryAsync(DateTime date);
        Task<decimal> GetPastaneDailyRevenueAsync(DateTime date);


    }

}
