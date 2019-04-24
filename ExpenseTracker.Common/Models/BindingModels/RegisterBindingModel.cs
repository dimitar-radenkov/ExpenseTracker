using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Common.Models.BindingModels
{
    public class RegisterBindingModel : IValidatableObject
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Password lenght must be at least 3 symbols")]
        public string Password { get; set; }

        public string ConfirmedPassword { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if(this.Password != this.ConfirmedPassword)
            {
                results.Add(new ValidationResult("'Password' and 'Confirmed Password' does not match!"));
            }

            return results;
        }
    }
}
