using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Context.Configurations
{
    public class CardsConfiguration : IEntityTypeConfiguration<Cards>
    {

        List<Cards> SeedCards = new List<Cards>()
        {
            new Cards()
            {
                Id =1,
                Numbers ="400024001234567",
                CVV="001" ,
                Balance= 1000000m,
                UserCreatorId =1,
                CreationTime = DateTime.Now
            },
            new Cards()
            {
                Id=2,
                Numbers ="500024001234567",
                CVV="001" ,
                Balance= 1000000m,
                UserCreatorId =2,
                CreationTime = DateTime.Now
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
