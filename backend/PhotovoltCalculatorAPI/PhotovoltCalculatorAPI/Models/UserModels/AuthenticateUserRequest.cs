using System.ComponentModel.DataAnnotations;

namespace PhotovoltCalculatorAPI.Models.UserModels
{
    public class AuthenticateUserRequest
    {
        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
