using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.Property(u => u.CreatedTime)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));

        builder.HasOne(u => u.User)
            .WithMany(u => u.Contacts)
            .HasForeignKey(u => u.CreatorId)
            .IsRequired()
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(c => c.Group)
            .WithMany(g => g.Contacts)
            .HasForeignKey(c => c.GroupId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasOne(c => c.Company)
            .WithOne(c => c.Contact)
            .HasForeignKey<Company>(c => c.Id)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(c => c.PhoneNumbers)
            .WithOne(cp => cp.Contact)
            .HasForeignKey(cp => cp.ContactId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}