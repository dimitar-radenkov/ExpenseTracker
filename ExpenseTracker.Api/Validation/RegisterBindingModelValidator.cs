using ExpenseTracker.Api.Models.BindingModels;
using FluentValidation;

namespace ExpenseTracker.Api.Validation
{
    public class RegisterBindingModelValidator : AbstractValidator<RegisterBindingModel>
    {
        public RegisterBindingModelValidator()
        {
            this.RuleFor(m => m.Email).NotEmpty().EmailAddress();
            this.RuleFor(m => m.Password).NotEmpty();
            this.RuleFor(m => m.ConfirmedPassword).NotEmpty();
            this.RuleFor(m => m.Password)
                .NotEmpty()
                .Matches(x => x.ConfirmedPassword);
        }
    }
}
