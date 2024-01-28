namespace PhotovoltCalculatorAPI.Entities
{
    public partial class ProjectProduct : BaseEntity
    {
        public Guid ProjectId { get; set; }
        public Guid ProductId { get; set; }
        public virtual Project Project { get; set; }
        public virtual Product Product { get; set; }
    }
}
