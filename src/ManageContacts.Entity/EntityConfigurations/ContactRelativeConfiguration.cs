using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class ContactRelativeConfiguration : IEntityTypeConfiguration<ContactRelative>
{
    public void Configure(EntityTypeBuilder<ContactRelative> builder)
    {
        builder.HasOne(pt => pt.RelativeType)
            .WithMany()
            .HasForeignKey(pt => pt.RelativeTypeId)
            .IsRequired();
    }
}