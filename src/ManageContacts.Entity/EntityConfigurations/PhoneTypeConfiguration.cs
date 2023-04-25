using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class PhoneTypeConfiguration : IEntityTypeConfiguration<PhoneType>
{
    public void Configure(EntityTypeBuilder<PhoneType> builder)
    {
        throw new NotImplementedException();
    }
}