using PhotovoltCalculatorAPI.Entities;

namespace PhotovoltCalculatorAPI.Models.ProductModels
{
    public class CreateProduct
    {
        public string ProductName { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Orientation Orientation { get; set; }
        public double Inclination { get; set; }
        public double Area { get; set; }
        public int Power { get; set; }
    }
}
