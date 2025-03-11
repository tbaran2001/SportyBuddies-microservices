namespace ProfileManagement.API.Data.Configurations;

public class ProfileConfiguration : IEntityTypeConfiguration<Profile>
{
    public void Configure(EntityTypeBuilder<Profile> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id)
            .ValueGeneratedNever()
            .HasConversion(profile => profile.Value, dbId => ProfileId.Of(dbId));

        builder.HasMany(p => p.ProfileSports)
            .WithOne()
            .HasForeignKey(p => p.ProfileId)
            .OnDelete(DeleteBehavior.Cascade);

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

        builder.OwnsOne(p => p.BirthDate, birthDateBuilder =>
        {
            birthDateBuilder.Property(b => b.Value)
                .HasColumnName(nameof(Profile.BirthDate))
                .IsRequired();
        });

        builder.Property(p => p.Gender)
            .HasDefaultValue(Gender.Unknown)
            .HasConversion(
                g => g.ToString(),
                g => (Gender)Enum.Parse(typeof(Gender), g));

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