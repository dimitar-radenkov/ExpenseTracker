using ExpenseTracker.Api.Models.BindingModels;
using FluentValidation;

namespace ExpenseTracker.Api.Validation
{
    public class RegisterBindingModelValidator : AbstractValidator<RegisterBindingModel>
    {
        public RegisterBindingModelValidator()
        {
            RuleFor(m => m.Email).EmailAddress();
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
}
