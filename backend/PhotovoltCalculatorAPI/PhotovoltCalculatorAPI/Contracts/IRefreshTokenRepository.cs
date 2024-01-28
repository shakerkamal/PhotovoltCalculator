using PhotovoltCalculatorAPI.Entities;

namespace PhotovoltCalculatorAPI.Contracts
{
    public interface IRefreshTokenRepository : IRepositoryBase<RefreshToken>
    {
        RefreshToken GetByToken(string token);
        IEnumerable<RefreshToken> GetByUserId(Guid userId);
        void CreateRefreshToken(RefreshToken refreshToken);
        void UpdateRefreshToken(RefreshToken refreshToken);
        void DeleteRefreshToken(RefreshToken refreshToken);
        void DeleteOldRefreshTokensForUser(Guid userId, int tokenLifeTime);
    }
}
