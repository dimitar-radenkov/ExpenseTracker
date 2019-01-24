using ExpenseTracker.Api.Models.BindingModels;
using FluentValidation;

namespace ExpenseTracker.Api.Validation
{
    public class LoginBindingModelValidator : AbstractValidator<LoginBindingModel>
    {
        public LoginBindingModelValidator()
        {
            this.RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            this.RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}