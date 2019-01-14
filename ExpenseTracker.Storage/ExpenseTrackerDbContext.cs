using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Storage
{
    public class ExpenseTrackerDbContext : DbContext
    {
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

        public DbSet<Expense> Expenses { get; set; }

        public DbSet<IncomeCategory> IncomeCategories { get; set; }

        public DbSet<Income> Incomes { get; set; }

        public ExpenseTrackerDbContext(DbContextOptions options)
            :base(options)
        {
            
        }
    }
}
