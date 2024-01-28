using EmailService;
using PhotovoltCalculatorAPI.Contracts;
using PhotovoltCalculatorAPI.Entities;

namespace PhotovoltCalculatorAPI.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private DataContext _dataContext;
        private IEmailSender _emailSender;
        private IUserRepository _userRepository;
        private IProjectRepository _projectRepository;
        private IProductRepository _productRepository;
        private IRefreshTokenRepository _refreshTokenRepository;
        private IProjectProductRepository _projectProductRepository;

        public UnitOfWork(DataContext dataContext, IEmailSender emailSender)
        {
            _dataContext = dataContext;
            _emailSender = emailSender;
        }
        public IUserRepository User
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_dataContext, _emailSender);
                return _userRepository;
            }
        }
        public IProjectRepository Project
        {
            get
            {
                if (_projectRepository == null)
                    _projectRepository = new ProjectRepository(_dataContext);
                return _projectRepository;
            }
        }
        public IProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_dataContext);
                return _productRepository;
            }
        }
        public IRefreshTokenRepository RefreshToken
        {
            get
            {
                if (_refreshTokenRepository == null)
                    _refreshTokenRepository = new RefreshTokenRepository(_dataContext);
                return _refreshTokenRepository;
            }
        }

        public IProjectProductRepository ProjectProduct
        {
            get
            {
                if (_projectProductRepository == null)
                    _projectProductRepository = new ProjectProductRepository(_dataContext);
                return _projectProductRepository;
            }
        }

        public async Task SaveAsync()
        {
            await _dataContext.SaveChangesAsync();
        }
    }
}
