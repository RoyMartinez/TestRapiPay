using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Context
{
    public class RapiPayContext:DbContext
    {
        private readonly IConfiguration _config;
        public RapiPayContext(IConfiguration configuration) { _config = configuration; }

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
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(RapiPayContext).Assembly);
        }
    }
}
