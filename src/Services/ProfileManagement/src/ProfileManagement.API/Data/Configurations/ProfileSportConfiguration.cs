namespace ProfileManagement.API.Data.Configurations;

public class ProfileSportConfiguration : IEntityTypeConfiguration<ProfileSport>
{
    public void Configure(EntityTypeBuilder<ProfileSport> builder)
    {
        builder.HasKey(ps => ps.Id);
        builder.Property(ps => ps.Id)
            .ValueGeneratedNever()
            .HasConversion(profileSport => profileSport.Value, dbId => ProfileSportId.Of(dbId));

        builder.HasOne<Profile>()
            .WithMany(p => p.ProfileSports)
            .HasForeignKey(ps => ps.ProfileId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Sport>()
            .WithMany()
            .HasForeignKey(ps => ps.SportId);
    }
}