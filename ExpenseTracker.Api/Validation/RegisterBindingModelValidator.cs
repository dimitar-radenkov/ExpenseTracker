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

            RuleFor(x => x).Custom((x, context) =>
            {
                if (x.Password != x.ConfirmedPassword)
                {
                    context.AddFailure(nameof(x.Password), "Passwords should match");
                }
            });
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
