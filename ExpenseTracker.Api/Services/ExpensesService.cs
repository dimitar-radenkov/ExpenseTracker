using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ExpenseTracker.Api.Services.Contracts;
using ExpenseTracker.Models;
using ExpenseTracker.Storage;

namespace ExpenseTracker.Api.Services
{
    public class ExpensesService : IExpensesService
    {
        private readonly ExpenseTrackerDbContext db;

        public ExpensesService(ExpenseTrackerDbContext db)
        {
            this.db = db;
        }

        public async Task<Expense> AddAsync(
            decimal amount,
            string descrition, 
            long categoryId,
            string userId)
        {
            var entity = new Expense
            {
                Amount = amount,
                CategoryId = categoryId,
                Description = descrition,
                Date = DateTime.UtcNow,
                UserId = userId,
            };

            await this.db.Expenses.AddAsync(entity);
            await this.db.SaveChangesAsync();

            return entity;        
        }

        public bool Delete(long expenseId)
        {
            throw new System.NotImplementedException();
        }

        public Expense Get(long expenseId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Expense> GetByCategory(long categoryId)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(long expenseId, decimal amount, string description, long categoryId)
        {
            throw new System.NotImplementedException();
        }
    }
}
