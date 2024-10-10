using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAdvanceService
    {
        Task<List<Advance>> GetAllAsync();
        Task<List<Advance>> GetEmployeeAdvancesByDateAsync(int id, int year, int month);
        Task<List<Advance>> GetAllAdvancesByDateAsync(int year, int month);
        Task<decimal> GetTotalAdvancesAmountByDateAsync(int id, int year, int month);
        Task AddAsync(Advance advance);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(Advance advance);
        Task UpdateAsync(Advance advance);
        Task<Advance> GetByIdAsync(int id);

    }
}