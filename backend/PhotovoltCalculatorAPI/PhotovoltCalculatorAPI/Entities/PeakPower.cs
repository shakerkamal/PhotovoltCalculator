using PhotovoltCalculatorAPI.Contracts;

namespace PhotovoltCalculatorAPI.Entities
{
    public partial class PeakPower : BaseEntity, ISoftDelete
    {
        public double Power { get; set; }
        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
