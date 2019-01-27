namespace ExpenseTracker.Api.Models.BindingModels
{
    public class AddExpenseBindingModel
    {
        public decimal Amount { get; set; }

        public string Description { get; set; }

        public long CategoryId { get; set; }
    }
}
