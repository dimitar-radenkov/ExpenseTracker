using ExpenseTracker.Api.Models.BindingModels;
using FluentValidation;

namespace ExpenseTracker.Api.Validation
{
    public class LoginBindingModelValidator : AbstractValidator<LoginBindingModel>
    {
        public LoginBindingModelValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}