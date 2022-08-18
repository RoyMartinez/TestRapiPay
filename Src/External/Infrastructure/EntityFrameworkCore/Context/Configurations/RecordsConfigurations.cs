using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.EntityFrameworkCore.Context.Configurations
{
    public class RecordsConfigurations : IEntityTypeConfiguration<Records>
    {
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
        }
    }
}
