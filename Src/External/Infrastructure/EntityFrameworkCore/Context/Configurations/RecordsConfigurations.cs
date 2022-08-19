using Domain.Enums;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace Infrastructure.EntityFrameworkCore.Context.Configurations
{
    public class RecordsConfigurations : IEntityTypeConfiguration<Records>
    {
        List<Records> SeedRecord = new List<Records>() 
        {
            new Records()
            {
                Id =1,
                CreationTime = DateTime.Now,
                UserCreatorId =1,
                CardId = 1,
                RecordType = (int)RecordTypeEnum.Recharge,
                PaymentReference="ReferenceDataSeed1",
                Amount = 1000m,
                Fee = 0,
                Total = 1000m,
                PercentageFee = 0,
                CardOldBalance =0m,
                CardNewBalance =1000m,
            },
            new Records()
            {
                Id =2,
                CreationTime = DateTime.Now,
                UserCreatorId =2,
                CardId = 2,
                RecordType = (int)RecordTypeEnum.Recharge,
                PaymentReference="ReferenceDataSeed2",
                Amount = 1000m,
                Fee = 0,
                Total = 1000m,
                PercentageFee = 0,
                CardOldBalance =0m,
                CardNewBalance =1000m,
            }
        };
        public void Configure(EntityTypeBuilder<Records> entity)
        {
            entity.ToTable("Records");
            entity.HasIndex(d => d.PaymentReference)
                .HasName("Uq_Records_Reference")
                .IsUnique();
            entity.HasOne(d => d.Card)
               .WithMany(d => d.Records)
               .HasForeignKey(d => d.CardId)
               .HasConstraintName("fk_Records_Cards");
            entity.HasData(SeedRecord);
        }
    }
}
