using Microsoft.EntityFrameworkCore;

namespace EfDataAccess
{
    public class PlannerContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Deed> Deeds { get; set; }

        public DbSet<Execution> Executions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=ksu-learning.database.windows.net; Database=Planner; User=planner; Password=b3thz35fd#; MultipleActiveResultSets=true");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Deed>().Navigation(x => x.Executions).AutoInclude();
            modelBuilder.Entity<Execution>().Navigation(x => x.Deed).AutoInclude();
            modelBuilder.Entity<Execution>().Navigation(x => x.User).AutoInclude();
        }
    }
}
