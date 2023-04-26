using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class PhoneTypeConfiguration : IEntityTypeConfiguration<PhoneType>
{
    public void Configure(EntityTypeBuilder<PhoneType> builder)
    {
        builder.HasQueryFilter(x => !x.Deleted);
            
        builder.Property(u => u.CreatedTime)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
            
        builder.Property(u => u.ModifiedTime)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v.Value, DateTimeKind.Utc));

        builder.HasOne(u => u.Creator)
            .WithMany()
            .HasForeignKey(u => u.CreatorId)
            .IsRequired(false);
        
        builder.HasOne(u => u.Modifier)
            .WithMany()
            .HasForeignKey(u => u.ModifierId)
            .IsRequired(false);
    }
}