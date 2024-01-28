using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;

namespace PhotovoltCalculatorAPI.Implementations
{
    public class RefreshTokenRepository : RepositoryBase<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DataContext context) : base(context)
        {
        }

        public void CreateRefreshToken(RefreshToken refreshToken)
        {
            Create(refreshToken);
        }

        public void DeleteOldRefreshTokensForUser(Guid userId, int tokenLifeTime)
        {
            var oldTokens = FindByCondition(rt => rt.SystemUserId == userId 
                                            //&& !rt.IsActive 
                                            && rt.DateCreated.AddDays(tokenLifeTime) <= DateTime.UtcNow)
                                            .ToList();
            DeleteRange(oldTokens);
        }

        public void DeleteRefreshToken(RefreshToken refreshToken)
        {
            Delete(refreshToken);
        }

        public RefreshToken GetByToken(string token)
        {
            return FindByCondition(rt => rt.Token == token)
                .FirstOrDefault();
        }

        public IEnumerable<RefreshToken> GetByUserId(Guid userId)
        {
            return FindByCondition(rt => rt.SystemUserId.Equals(userId))
                .ToList();
        }

        public void UpdateRefreshToken(RefreshToken refreshToken)
        {
            Update(refreshToken);
        }
    }
}
