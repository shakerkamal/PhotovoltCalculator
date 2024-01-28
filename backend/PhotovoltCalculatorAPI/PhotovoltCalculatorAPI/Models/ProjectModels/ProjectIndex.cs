using PhotovoltCalculatorAPI.Models.ProductModels;

namespace PhotovoltCalculatorAPI.Models.ProjectModels
{
    public class ProjectIndex
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime DateCreated { get; set; }
        //public List<ProductIndex> Products { get; set; }
    }
}
