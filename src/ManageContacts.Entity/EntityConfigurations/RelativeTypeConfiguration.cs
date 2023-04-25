using ManageContacts.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ManageContacts.Entity.EntityConfigurations;

public class RelativeTypeConfiguration : IEntityTypeConfiguration<RelativeType>
{
    public void Configure(EntityTypeBuilder<RelativeType> builder)
    {
        throw new NotImplementedException();
    }
}