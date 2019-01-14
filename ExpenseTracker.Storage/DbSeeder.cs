using System.Linq;
using ExpenseTracker.Models;

namespace ExpenseTracker.Storage
{
    public static class DbSeeder
    {
        public static void Seed(ExpenseTrackerDbContext context)
        {

            if (context.ExpenseCategories.Any() ||
                context.IncomeCategories.Any())
            {
                return;
            }

            //add expence categories
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Utilities" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Auto" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Tax" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Grocery" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Eating out" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Cloth" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Education" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Charity" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Children" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Gifts" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Entertaiment" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Travel" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Health" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Medical" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Household" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Rent" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Loans" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Pets" });
            context.ExpenseCategories.Add( new ExpenseCategory { Type = CategoryType.Default, Name = "Other" });

            //add income categories
            context.IncomeCategories.Add(new IncomeCategory { Type = CategoryType.Default, Name = "Salary" });
            context.IncomeCategories.Add(new IncomeCategory { Type = CategoryType.Default, Name = "Bonus" });
            context.IncomeCategories.Add(new IncomeCategory { Type = CategoryType.Default, Name = "Deposit" });
            context.IncomeCategories.Add(new IncomeCategory { Type = CategoryType.Default, Name = "Other" });

            context.SaveChanges();
        }
    }
}
