using Entities.Concrete;

namespace Business.Abstract
{
    public interface IExpenseService
    {
        Task<List<Expense>> GetAllAsync();
        Task AddAsync(Expense expense);
        Task<List<Expense>> GetExpensesByDateAsync(DateTime date);
        Task DeleteByIdAsync(int id);
        Task DeleteAsync(Expense expense);
        Task UpdateAsync(Expense expense);
        Task<Expense> GetByIdAsync(int id);

    }
}
