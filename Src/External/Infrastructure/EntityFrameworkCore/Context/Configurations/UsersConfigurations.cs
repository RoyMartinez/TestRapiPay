using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityFrameworkCore.Context.Configurations
{
    public class UsersConfigurations : IEntityTypeConfiguration<Users>
    {
        List<Users> SeedUsers = new List<Users>()
        {
            new Users() { Id=1,UserName ="RoyMartinez", Password="123" },
            new Users() { Id=2,UserName ="JuanPerez", Password="123" }
        };

        public void Configure(EntityTypeBuilder<Users> entity)
        {
            entity.ToTable("Users");
            entity.HasIndex(d => d.UserName)
                .HasName("Uq_User_UserName")
                .IsUnique();
            entity.HasData(SeedUsers);
        }
    }
}
