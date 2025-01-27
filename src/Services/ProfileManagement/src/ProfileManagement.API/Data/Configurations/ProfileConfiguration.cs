using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileManagement.API.Profiles.Enums;
using ProfileManagement.API.Profiles.Models;

namespace ProfileManagement.API.Data.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.ProfileSports)
            .WithOne()
            .HasForeignKey(p => p.ProfileId);

        builder.OwnsOne(p => p.Name, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value)
                .HasColumnName(nameof(Profile.Name))
                .IsRequired();
        });

        builder.OwnsOne(p => p.Description, descriptionBuilder =>
        {
            descriptionBuilder.Property(d => d.Value)
                .HasColumnName(nameof(Profile.Description))
                .IsRequired();
        });

        builder.OwnsOne(p => p.Preferences, preferencesBuilder =>
        {
            preferencesBuilder.Property(p => p.MinAge);
            preferencesBuilder.Property(p => p.MaxAge);
            preferencesBuilder.Property(p => p.MaxDistance);
            preferencesBuilder.Property(p => p.PreferredGender)
                .HasConversion(
                    gender => gender.ToString(),
                    dbGender => (Gender)Enum.Parse(typeof(Gender), dbGender));
        });
    }
}