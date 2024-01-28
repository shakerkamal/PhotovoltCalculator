using PhotovoltCalculatorAPI.Entities;

namespace PhotovoltCalculatorAPI.Contracts
{
    public interface IUserRepository : IRepositoryBase<SystemUser>
    {
        SystemUser GetByUserName(string userName);
        SystemUser GetUser(Guid userId);
        void CreateUser(SystemUser user, string origin);
        void UpdateUser(SystemUser user);
        void DeleteUser(SystemUser user);
        void VerifyEmail(string token);
    }
}
