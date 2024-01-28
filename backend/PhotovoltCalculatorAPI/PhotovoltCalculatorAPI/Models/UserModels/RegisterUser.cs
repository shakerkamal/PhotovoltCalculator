using System.ComponentModel.DataAnnotations;

namespace PhotovoltCalculatorAPI.Models.UserModels
{
    public class RegisterUser
    {

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Password is required with minimum 6 characters")]
        [MinLength(6)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
