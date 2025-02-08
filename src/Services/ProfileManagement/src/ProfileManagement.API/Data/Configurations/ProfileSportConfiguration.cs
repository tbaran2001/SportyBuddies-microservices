using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileManagement.API.Profiles.Models;
using ProfileManagement.API.Profiles.ValueObjects;
using ProfileManagement.API.Sports.Models;

namespace ProfileManagement.API.Data.Configurations;

public class ProfileSportConfiguration : IEntityTypeConfiguration<ProfileSport>
{
    public void Configure(EntityTypeBuilder<ProfileSport> builder)
    {
        builder.HasKey(ps => ps.Id);
        builder.Property(ps => ps.Id)
            .ValueGeneratedNever()
            .HasConversion<Guid>(profileSport => profileSport.Value, dbId => ProfileSportId.Of(dbId));

        builder.HasOne<Sport>()
            .WithMany()
            .HasForeignKey(ps => ps.SportId);
    }
}