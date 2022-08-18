using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.EntityFrameworkCore.Context
{
    public class RapidPayContext:DbContext
    {
        private readonly IConfiguration _config;
        public RapidPayContext(IConfiguration configuration) { _config = configuration; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql(_config.GetConnectionString("Connection"),
                                        b => b.MigrationsAssembly("Infrastructure"));
            }
        }
        public DbSet<Users> Users { get; set; }
        public DbSet<Cards> Cards { get; set; }
        public DbSet<Records> Records { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RapidPayContext).Assembly);
        }
    }
}
