namespace PhotovoltCalculatorAPI.Entities
{
    public partial class Project : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid SystemUserId { get; set; }

        public virtual SystemUser SystemUser { get; set; }
        public virtual ICollection<ProjectProduct> ProjectProducts { get; set; }
    }
}
