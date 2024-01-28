using System.ComponentModel.DataAnnotations;

namespace PhotovoltCalculatorAPI.Models.UserModels
{
    public class VerifyEmailRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
