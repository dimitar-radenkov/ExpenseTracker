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

        bool Update(long expenseId, decimal amount, string description, long categoryId);

        bool Delete(long expenseId);

        IEnumerable<Expense> GetByCategory(long categoryId);
    }
}
