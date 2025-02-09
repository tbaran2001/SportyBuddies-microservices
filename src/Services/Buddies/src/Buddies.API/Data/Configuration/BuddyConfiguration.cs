using Buddies.API.Buddies.Models;
using Buddies.API.Buddies.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Buddies.API.Data.Configuration;

public class BuddyConfiguration : IEntityTypeConfiguration<Buddy>
{
    public void Configure(EntityTypeBuilder<Buddy> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Id)
            .ValueGeneratedNever()
            .HasConversion(buddy => buddy.Value, dbId => BuddyId.Of(dbId));

        builder.Property(b => b.OppositeBuddyId)
            .HasConversion(buddyId => buddyId.Value, dbId => BuddyId.Of(dbId));

        builder.Property(b => b.ProfileId)
            .HasConversion(id => id.Value, dbId => ProfileId.Of(dbId));

        builder.Property(b => b.MatchedProfileId)
            .HasConversion(id => id.Value, dbId => ProfileId.Of(dbId));

        builder.OwnsOne(b => b.CreatedAt, a =>
        {
            a.Property(p => p.Value)
                .HasColumnName(nameof(Buddy.CreatedAt))
                .IsRequired();
        });
    }
}