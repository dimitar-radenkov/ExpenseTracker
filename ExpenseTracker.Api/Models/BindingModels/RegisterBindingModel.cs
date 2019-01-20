namespace ExpenseTracker.Api.Models.BindingModels
{
    public class RegisterBindingModel
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }   
    }
}
