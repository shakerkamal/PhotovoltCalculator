using System.ComponentModel.DataAnnotations;

namespace PhotovoltCalculatorAPI.Models.UserModels
{
    public class UpdateUser
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
