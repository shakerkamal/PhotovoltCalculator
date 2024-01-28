using Microsoft.EntityFrameworkCore;

namespace PhotovoltCalculatorAPI.Entities
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<SystemUser> SystemUsers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectProduct> ProjectProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<WeatherData> WeatherData { get; set; }
        public DbSet<PeakPower> PeakPowers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}
