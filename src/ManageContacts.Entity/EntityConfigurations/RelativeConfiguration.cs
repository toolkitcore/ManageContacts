using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class RelativeConfiguration : IEntityTypeConfiguration<ContactRelative>
{
    public void Configure(EntityTypeBuilder<ContactRelative> builder)
    {
        throw new NotImplementedException();
    }
}