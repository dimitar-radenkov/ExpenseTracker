using System;
using Microsoft.AspNetCore.Identity;

namespace ExpenseTracker.Models
{
    public class Expense : Entity
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public long CategoryId { get; set; }
        public ExpenseCategory Category { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}
