using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class ExpenseManager : IExpenseService
    {
        private readonly IExpenseDal _expenseDal;

        public ExpenseManager(IExpenseDal expenseDal)
        {
            _expenseDal = expenseDal;
        }

        public async Task AddAsync(Expense expense)
        {
            await _expenseDal.Add(expense);
        }

        public async Task DeleteByIdAsync(int id)
        {
            await _expenseDal.DeleteById(id);
        }

        public async Task DeleteAsync(Expense expense)
        {
            await _expenseDal.Delete(expense);
        }

        public async Task<List<Expense>> GetAllAsync()
        {
            return await _expenseDal.GetAll();
        }

        public async Task<Expense> GetByIdAsync(int id)
        {
            return await _expenseDal.Get(d => d.Id == id);
        }

        public async Task UpdateAsync(Expense expense)
        {
            await _expenseDal.Update(expense);
        }

        public async Task<List<Expense>> GetExpensesByDateAsync(DateTime date)
        {
            return await _expenseDal.GetAll(e => e.Date.Date == date.Date);
        }
    }
}
