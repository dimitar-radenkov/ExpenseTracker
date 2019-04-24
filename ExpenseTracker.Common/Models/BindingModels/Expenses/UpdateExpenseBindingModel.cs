namespace ExpenseTracker.Common.Models.BindingModels.Expenses
{
    public class UpdateExpenseBindingModel
    {
        public long ExpenseId { get; set; }

        public decimal Amount { get; set; }

        public string Description { get; set; }

        public long CategoryId { get; set; }
    }
}