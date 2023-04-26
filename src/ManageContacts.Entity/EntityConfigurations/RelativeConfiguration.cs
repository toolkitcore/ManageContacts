using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class RelativeConfiguration : IEntityTypeConfiguration<Relative>
{
    public void Configure(EntityTypeBuilder<Relative> builder)
    {
        builder.HasOne(pt => pt.RelativeType)
            .WithMany()
            .HasForeignKey(pt => pt.RelativeTypeId)
            .IsRequired(false);
    }
}