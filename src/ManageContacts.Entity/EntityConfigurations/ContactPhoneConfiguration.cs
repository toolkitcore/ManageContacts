using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class ContactPhoneConfiguration : IEntityTypeConfiguration<ContactPhone>
{
    public void Configure(EntityTypeBuilder<ContactPhone> builder)
    {
        builder.HasOne(pt => pt.PhoneType)
            .WithMany()
            .HasForeignKey(pt => pt.PhoneTypeId)
            .IsRequired();
    }
}