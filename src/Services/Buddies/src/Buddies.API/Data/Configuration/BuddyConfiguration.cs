using Buddies.API.Buddies.Models;
using Buddies.API.Buddies.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buddies.API.Data.Configuration;

public class BuddyConfiguration: IEntityTypeConfiguration<Buddy>
{
    public void Configure(EntityTypeBuilder<Buddy> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedNever()
            .HasConversion(buddy => buddy.Value, dbId => BuddyId.Of(dbId));
    }
}