namespace PhotovoltCalculatorAPI.Entities
{
    public partial class WeatherData
    {
        public Guid Id { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string WeatherCondition { get; set; }  //main from the api response
        public double Temperature { get; set; }
        public string TemperatureUnit { get; set; } //in farenheit from openweather
        public int Humidity { get; set; }
        public int Clouds { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime Sunrise { get; set; }
        public DateTime Sunset { get; set; }
    }
}
