namespace PhotovoltCalculatorAPI.Contracts
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IProjectRepository Project { get; }
        IProductRepository Product { get; }
        IRefreshTokenRepository RefreshToken { get; }
        IProjectProductRepository ProjectProduct { get; }

        Task SaveAsync();
    }
}
