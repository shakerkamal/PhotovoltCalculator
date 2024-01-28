using EmailService;
using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;
using System.Security.Cryptography;
using System.Text;

namespace PhotovoltCalculatorAPI.Implementations
{
    public class UserRepository : RepositoryBase<SystemUser>, IUserRepository
    {
        private readonly IEmailSender _emailSender;
        public UserRepository(DataContext context, IEmailSender emailSender) : base(context)
        {
            _emailSender = emailSender;
        }

        public SystemUser GetUser(Guid userId)
        {
            return FindByCondition(u => u.Id.Equals(userId))
                .FirstOrDefault();
        }

        public SystemUser GetByUserName(string userName)
        {
            return FindByCondition(u => u.UserName.ToLower().Equals(userName.ToLower()) && u.Deleted == false)
                .FirstOrDefault();
        }

        public void CreateUser(SystemUser user, string origin)
        {
            byte[] passwordHash, salt;
            CreatePasswordHash(user.Password, out passwordHash, out salt);

            user.Password = Convert.ToBase64String(passwordHash);
            user.SecurityStamp = Convert.ToBase64String(salt);
            user.VerificationToken = GenerateVerficationToken();
            user.UserName = user.Email;
            Create(user);

            //send Verification Email
            SendVerificationEmail(user, origin);
        }

        public void UpdateUser(SystemUser user)
        {
            Update(user);
        }

        public void DeleteUser(SystemUser user)
        {
            Delete(user);
        }

        public void VerifyEmail(string token)
        {
            var user = GetUserByToken(token);
            if (user != null)
            {
                user.Verified = DateTime.UtcNow;
                user.VerificationToken = string.Empty;

                Update(user);
            }
        }

        private SystemUser GetUserByToken(string token)
        {
            return _context.SystemUsers.SingleOrDefault(u => u.VerificationToken == token);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] salt)
        {
            using (var hmac = new HMACSHA512())
            {
                salt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private string GenerateVerficationToken()
        {
            // token is a cryptographically strong random sequence of values
            var token = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

            // ensure token is unique by checking against db
            var tokenIsUnique = !_context.SystemUsers.Any(x => x.VerificationToken == token);
            if (!tokenIsUnique)
                return GenerateVerficationToken();

            return token;
        }
        private void SendVerificationEmail(SystemUser user, string origin)
        {
            var verifyUrl = $"{origin}/user/verify-email?token={user.VerificationToken}";
            string message = $@"<p>Please click the below link to verify your email address:</p>
                            <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>";
            var email = new Message()
            {
                ToAddresses = new List<string> { user.Email },
                Subject = "Please verify your email",
                Body = message
            };
            _emailSender.SendEmailAsync(email);
        }
    }
}
