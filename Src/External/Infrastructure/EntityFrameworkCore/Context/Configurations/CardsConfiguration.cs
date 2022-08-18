using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.EntityFrameworkCore.Context.Configurations
{
    public class CardsConfiguration : IEntityTypeConfiguration<Cards>
    {

        List<Cards> SeedCards = new List<Cards>()
        {
            new Cards()
            {
                Id =1,
                Name = "Roy Martinez",
                Numbers ="400024001234567",
                CVV="001" ,
                Balance= 1000000m,
                UserCreatorId =1,
                CreationTime = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(1),
            },
            new Cards()
            {
                Id=2,
                Name = "Juan Perez",
                Numbers ="500024001234567",
                CVV="001" ,
                Balance= 1000000m,
                UserCreatorId =2,
                CreationTime = DateTime.Now,
                ExpirationDate = DateTime.Now.AddDays(1),
            }
        };

        public void Configure(EntityTypeBuilder<Cards> entity)
        {
            entity.ToTable("Cards");
            entity.HasIndex(d => d.Numbers)
                .HasName("Uq_Cards_Numbers")
                .IsUnique();
            entity.HasData(SeedCards);
        }
    }
}
