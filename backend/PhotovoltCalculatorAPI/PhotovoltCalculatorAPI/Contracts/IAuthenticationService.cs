using PhotovoltCalculatorAPI.Models.UserModels;

namespace PhotovoltCalculatorAPI.Contracts
{
    public interface IAuthenticationService
    {
        Task<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest authenticateUserRequest);
        Task<AuthenticateUserResponse> RefreshToken(string token);
    }
}
