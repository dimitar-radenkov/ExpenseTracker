using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Api.Services.Contracts
{
    public interface IExpensesService
    {
        Task<Expense> AddAsync(
            decimal amount, 
            string descrition,
            long categoryId, 
            string userId); 

        Expense Get(long expenseId);

        Task UpdateAsync(
            long expenseId, 
            decimal amount, 
            string description,
            long categoryId, 
            string userId);

        Task DeleteAsync(long expenseId);

        Task<IEnumerable<Expense>> GetAllAsync(string userId);

        Task<IEnumerable<Expense>> GetAllByPeriodAsync(string userId, DateTime from, DateTime to);
    }
}
