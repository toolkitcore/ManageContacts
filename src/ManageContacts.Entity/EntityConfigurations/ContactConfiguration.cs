using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class ContactConfiguration : IEntityTypeConfiguration<Contact>
{
    public void Configure(EntityTypeBuilder<Contact> builder)
    {
        builder.HasQueryFilter(x => !x.Deleted);
            
        builder.Property(u => u.CreatedTime)
            .HasConversion(v => v, v => DateTime.SpecifyKind(v, DateTimeKind.Utc));
        
        builder.HasOne(u => u.User)
            .WithMany(u => u.Contacts)
            .HasForeignKey(u => u.UserId)
            .IsRequired();
        

        builder.HasOne(c => c.Group)
            .WithMany(g => g.Contacts)
            .HasForeignKey(c => c.GroupId)
            .IsRequired();

        builder.HasOne(c => c.Company)
            .WithOne(c => c.Contact)
            .HasForeignKey<Company>(c => c.CompanyId);
        

        builder.HasMany(c => c.Addresses)
            .WithOne(ca => ca.Contact)
            .HasForeignKey(ca => ca.ContactId)
            .IsRequired();

        builder.HasMany(c => c.PhoneNumbers)
            .WithOne(cp => cp.Contact)
            .HasForeignKey(cp => cp.ContactId)
            .IsRequired();

        builder.HasMany(c => c.EmailAddresses)
            .WithOne(ce => ce.Contact)
            .HasForeignKey(ce => ce.ContactId)
            .IsRequired();

        builder.HasMany(c => c.Relatives)
            .WithOne(cr => cr.Contact)
            .HasForeignKey(cr => cr.ContactId)
            .IsRequired();
    }
}