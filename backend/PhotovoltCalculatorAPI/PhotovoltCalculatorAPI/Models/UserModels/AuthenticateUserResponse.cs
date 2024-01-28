namespace PhotovoltCalculatorAPI.Models.UserModels
{
    public class AuthenticateUserResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
