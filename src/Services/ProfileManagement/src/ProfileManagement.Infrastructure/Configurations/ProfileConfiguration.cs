using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileManagement.Domain.Enums;
using ProfileManagement.Domain.Models;

namespace ProfileManagement.Infrastructure.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(p => p.Id);

        builder.HasMany(p => p.ProfileSports)
            .WithOne()
            .HasForeignKey(p => p.ProfileId);

        builder.ComplexProperty(
            p => p.Name, nameBuilder =>
            {
                nameBuilder.Property(n => n.Value)
                    .HasColumnName(nameof(Profile.Name));
            });

        builder.ComplexProperty(
            p => p.Description, descriptionBuilder =>
            {
                descriptionBuilder.Property(d => d.Value)
                    .HasColumnName(nameof(Profile.Description));
            });

        builder.ComplexProperty(p => p.Preferences, preferencesBuilder =>
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