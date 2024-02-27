namespace LikeComparison.EntityFrameworkTests
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;

    internal sealed class LikeSqlServerTestContext : DbContext
    {
        private readonly SqlConnection connection;

        public LikeSqlServerTestContext(string connectionString)
        {
            this.connection = new SqlConnection(connectionString);
            this.connection.Open();
        }

        public DbSet<LikeTestResult> LikeTestResults { get; set; } = null!;

        public override void Dispose()
        {
            this.connection.Dispose();
            base.Dispose();
            GC.SuppressFinalize(this);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = optionsBuilder.UseSqlServer(this.connection);
        }
    }
}