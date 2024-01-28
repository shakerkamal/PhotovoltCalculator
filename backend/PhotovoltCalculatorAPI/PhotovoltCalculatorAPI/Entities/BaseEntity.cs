using PhotovoltCalculatorAPI.Contracts;

namespace PhotovoltCalculatorAPI.Entities
{
    public abstract class BaseEntity : ISoftDelete
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime DateModified { get; set; }
        public bool Deleted { get; set; }
    }
}
