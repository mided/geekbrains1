using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EfDataAccess
{
    internal class PlannerContext : DbContext
    {
        internal DbSet<User> Users { get; set; }

        internal DbSet<Deed> Deeds { get; set; }

        public DbSet<Execution> Executions { get; set; }

        private readonly IConfiguration _configuration;

        public PlannerContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["SQLConnectionString"]);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deed>().Navigation(x => x.Executions).AutoInclude();
            modelBuilder.Entity<Execution>().Navigation(x => x.Deed).AutoInclude();
            modelBuilder.Entity<Execution>().Navigation(x => x.User).AutoInclude();
        }
    }
}
