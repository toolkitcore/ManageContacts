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
    }
}