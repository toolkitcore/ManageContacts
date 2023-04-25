using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class EmailTypeConfiguration : IEntityTypeConfiguration<EmailType>
{
    public void Configure(EntityTypeBuilder<EmailType> builder)
    {
        throw new NotImplementedException();
    }
}