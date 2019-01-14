using System;

namespace ExpenseTracker.Models
{
    public class Expense : Entity
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public long CategoryId { get; set; }
        public ExpenseCategory Category { get; set; }
    }
}
