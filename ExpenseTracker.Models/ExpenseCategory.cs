namespace ExpenseTracker.Models
{
    public class ExpenseCategory : Entity
    {
        public CategoryType Type { get; set; }

        public string Name { get; set; }
    }
}
