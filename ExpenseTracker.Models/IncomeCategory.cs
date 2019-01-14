namespace ExpenseTracker.Models
{
    public class IncomeCategory : Entity
    {
        public CategoryType Type { get; set; }

        public string Name { get; set; }
    }
}