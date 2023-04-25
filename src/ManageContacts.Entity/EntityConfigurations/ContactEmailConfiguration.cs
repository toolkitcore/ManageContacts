using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class ContactEmailConfiguration : IEntityTypeConfiguration<ContactEmail>
{
    public void Configure(EntityTypeBuilder<ContactEmail> builder)
    {
        throw new NotImplementedException();
    }
}