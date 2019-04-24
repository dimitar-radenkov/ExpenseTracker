using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Common.Models.BindingModels
{
    public class LoginBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(3, ErrorMessage = "Password lenght must be atleast 3 symbols")]
        public string Password { get; set; }
    }
}
