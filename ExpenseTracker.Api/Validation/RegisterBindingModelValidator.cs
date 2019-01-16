using ExpenseTracker.Api.Models.BindingModels;
using FluentValidation;

namespace ExpenseTracker.Api.Validation
{
    public class RegisterBindingModelValidator : AbstractValidator<RegisterBindingModel>
    {
        public RegisterBindingModelValidator()
        {
            RuleFor(m => m.Email).EmailAddress();
            RuleFor(m => m.ConfirmedPassword).NotEmpty();
            RuleFor(m => m.Password)
                .NotEmpty()
                .Matches(x => x.ConfirmedPassword);
        }
    }
}
