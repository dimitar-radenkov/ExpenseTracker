using System.Collections.Generic;
using ExpenseTracker.Models;

namespace ExpenseTracker.Api.Services.Contracts
{
    public interface IExpensesService
    {
        Expense Add(decimal amount, string descrition, long categoryId); 

        Expense Get(long expenseId);

        bool Update(long expenseId, decimal amount, string description, long categoryId);

        bool Delete(long expenseId);

        IEnumerable<Expense> GetByCategory(long categoryId);
    }
}
