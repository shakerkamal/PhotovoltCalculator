using PhotovoltCalculatorAPI.Models.ProductModels;
using System.ComponentModel.DataAnnotations;

namespace PhotovoltCalculatorAPI.Models.ProjectModels
{
    public class UpdateProject
    {
        [Required]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Project Name is required")]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public List<ProductIndex> Products { get; set; }
    }
}
