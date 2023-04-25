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
        
        builder.HasOne(c => c.Group)
            .WithMany(g => g.Contacts)
            .HasForeignKey(c => c.GroupId)
            .IsRequired(false);
        
        builder.HasOne<Company>(c => c.Company)
            .WithOne(c => c.Contact)
            .HasForeignKey<Company>(c => c.CompanyId)
            .IsRequired(false);

        builder.HasMany(c => c.Addresses)
            .WithOne(ca => ca.Contact)
            .HasForeignKey(ca => ca.ContactId);
        
        builder.HasMany(c => c.Phones)
            .WithOne(cp => cp.Contact)
            .HasForeignKey(cp => cp.ContactId);
        
        builder.HasMany(c => c.Emails)
            .WithOne(ce => ce.Contact)
            .HasForeignKey(ce => ce.ContactId);
        
        builder.HasMany(c => c.Relatives)
            .WithOne(cr => cr.Contact)
            .HasForeignKey(cr => cr.ContactId);
        
    }
}