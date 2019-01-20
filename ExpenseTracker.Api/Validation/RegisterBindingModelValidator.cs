using ExpenseTracker.Api.Models.BindingModels;
using FluentValidation;

namespace ExpenseTracker.Api.Validation
{
    public class RegisterBindingModelValidator : AbstractValidator<RegisterBindingModel>
    {
        public RegisterBindingModelValidator()
        {
            RuleFor(m => m.Email).NotEmpty().EmailAddress();
            RuleFor(m => m.Password).NotEmpty();
            RuleFor(m => m.ConfirmedPassword).NotEmpty();
            RuleFor(m => m.Password)
                .NotEmpty()
                .Matches(x => x.ConfirmedPassword);
        }
    }

    public class LoginBindingModelValidator : AbstractValidator<LoginBindingModel>
    {
        public LoginBindingModelValidator()
        {
            RuleFor(x => x.Email).NotNull().NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}
