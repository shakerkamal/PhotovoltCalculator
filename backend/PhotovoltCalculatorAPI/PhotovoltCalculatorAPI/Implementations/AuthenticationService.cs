using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;
using PhotovoltCalculatorAPI.Helpers;
using PhotovoltCalculatorAPI.Models.UserModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace PhotovoltCalculatorAPI.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AppSettings _appSettings;
        public AuthenticationService(IUnitOfWork unitOfWork, IOptions<AppSettings> appSettings)
        {
            _unitOfWork = unitOfWork;
            _appSettings = appSettings.Value;
        }

        public async Task<AuthenticateUserResponse> Authenticate(AuthenticateUserRequest request)
        {
            var user = _unitOfWork.User.GetByUserName(request.UserName);//_userRepository.FindByCondition(u => u.Email == request.UserName).SingleOrDefault();

            if (user == null || !VerifyPasswordHash(request.Password, user.Password, user.SecurityStamp))
                return null;

            var jwtToken = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken(user.Id);

            _unitOfWork.RefreshToken.CreateRefreshToken(refreshToken);

            _unitOfWork.RefreshToken.DeleteOldRefreshTokensForUser(user.Id, _appSettings.TokenLifeTime);

            await _unitOfWork.SaveAsync();

            return new AuthenticateUserResponse
            {
                AccessToken = jwtToken,
                RefreshToken = refreshToken.Token
            };
        }

        public async Task<AuthenticateUserResponse> RefreshToken(string refreshToken)
        {
            var existingRefreshToken = _unitOfWork.RefreshToken.GetByToken(refreshToken);

            if (existingRefreshToken == null || !IsRefreshTokenValid(existingRefreshToken))
                return null;

            var user = _unitOfWork.User.GetUser(existingRefreshToken.SystemUserId);
            var jwtToken = GenerateJwtToken(user);
            var newRefreshToken = GenerateRefreshToken(user.Id);

            _unitOfWork.RefreshToken.DeleteOldRefreshTokensForUser(user.Id, _appSettings.TokenLifeTime);
            _unitOfWork.RefreshToken.CreateRefreshToken(newRefreshToken);
            _unitOfWork.User.UpdateUser(user);

            await _unitOfWork.SaveAsync();

            return new AuthenticateUserResponse
            {
                AccessToken = jwtToken,
                RefreshToken = newRefreshToken.Token
            };
        }

        //private methods
        #region
        private bool VerifyPasswordHash(string password, string passwordHash, string salt)
        {
            using (var hmac = new HMACSHA512(Convert.FromBase64String(salt)))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(Convert.FromBase64String(passwordHash));
            }
        }

        private string GenerateJwtToken(SystemUser user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = _appSettings.ValidIssuer,
                Audience = _appSettings.ValidAudience,
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.TokenLifeTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerateRefreshToken(Guid userId)
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Id = Guid.NewGuid(),
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddDays(7),
                    DateCreated = DateTime.UtcNow,
                    SystemUserId = userId
                };
            }
        }

        private bool IsRefreshTokenValid(RefreshToken refreshToken)
        {
            return refreshToken.Expires >= DateTime.UtcNow;
        }
        #endregion
    }
}
