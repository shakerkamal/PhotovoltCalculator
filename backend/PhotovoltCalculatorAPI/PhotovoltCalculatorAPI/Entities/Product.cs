namespace PhotovoltCalculatorAPI.Entities
{
    public enum Orientation
    {
        North,
        South,
        East,
        West
    }
    public partial class Product : BaseEntity
    {
        public string ProductName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Orientation Orientation { get; set; }
        public double Inclination { get; set; }
        public double Area { get; set; }
        public int Power { get; set; }
        public virtual ICollection<PeakPower> PeakPowers { get; set; }
    }
}
