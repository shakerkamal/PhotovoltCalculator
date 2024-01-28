using PhotovoltCalculatorAPI.Models.ProductModels;

namespace PhotovoltCalculatorAPI.Models.ProjectModels
{
    public class ProjectDetails
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid SystemUserId { get; set; }
        public DateTime DateCreated { get; set; }
        public List<ProductIndex> Products { get; set; }
    }
}
