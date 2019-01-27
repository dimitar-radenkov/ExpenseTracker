using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Api.Services.Contracts;
using ExpenseTracker.Models;
using ExpenseTracker.Storage;
using Microsoft.EntityFrameworkCore;

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

        public async Task DeleteAsync(long expenseId)
        {
            var expense = this.db.Expenses
                .First(x => x.Id == expenseId);

            this.db.Expenses.Remove(expense);
            await this.db.SaveChangesAsync();
        }

        public Expense Get(long expenseId)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IEnumerable<Expense>> GetAllAsync(string userId)
        {
            return await this.db.Expenses
                .Where(x => x.UserId == userId)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task<IEnumerable<Expense>> GetAllByPeriodAsync(string userId, DateTime from, DateTime to)
        {
            throw new NotImplementedException();
        }


        public async Task UpdateAsync(
            long expenseId,
            decimal amount,
            string description, 
            long categoryId, 
            string userId)
        {
            var expense = this.db.Expenses
                .First(x => x.Id == expenseId && x.UserId == userId);

            expense.Amount = amount;
            expense.CategoryId = categoryId;
            expense.Description = description;

            this.db.Expenses.Update(expense);
            await this.db.SaveChangesAsync();
        }
    }
}
