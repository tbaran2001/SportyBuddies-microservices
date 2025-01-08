using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileManagement.API.Profiles.Models;

namespace ProfileManagement.API.Data.Configurations;

public class ProfileSportConfiguration : IEntityTypeConfiguration<ProfileSport>
{
    public void Configure(EntityTypeBuilder<ProfileSport> builder)
    {
        builder.HasKey(ps => ps.Id);

        builder.HasOne<Sport>()
            .WithMany()
            .HasForeignKey(ps => ps.SportId);
    }
}