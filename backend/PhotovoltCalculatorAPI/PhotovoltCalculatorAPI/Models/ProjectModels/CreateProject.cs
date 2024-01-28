using PhotovoltCalculatorAPI.Models.ProductModels;
using System.ComponentModel.DataAnnotations;

namespace PhotovoltCalculatorAPI.Models.ProjectModels
{
    public class CreateProject
    {
        [Required(ErrorMessage = "Project Name is required")]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Description { get; set; }
        public List<ProductIndex> Products { get; set; }
    }
}
