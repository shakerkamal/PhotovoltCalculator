namespace PhotovoltCalculatorAPI.Helpers
{
    public class AppSettings
    {
        public string JwtSecret { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
        public int TokenLifeTime { get; set; }
    }
}
