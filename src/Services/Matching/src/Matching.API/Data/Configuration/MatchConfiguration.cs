namespace Matching.API.Data.Configuration;

public class MatchConfiguration : IEntityTypeConfiguration<Match>
{
    public void Configure(EntityTypeBuilder<Match> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id)
            .ValueGeneratedNever()
            .HasConversion(match => match.Value, dbId => MatchId.Of(dbId));

        builder.Property(m => m.OppositeMatchId)
            .HasConversion(matchId => matchId.Value, dbId => MatchId.Of(dbId));

        builder.Property(m => m.ProfileId)
            .HasConversion(profileId => profileId.Value, dbId => ProfileId.Of(dbId));

        builder.Property(m => m.MatchedProfileId)
            .HasConversion(profileId => profileId.Value, dbId => ProfileId.Of(dbId));

        builder.Property(m => m.MatchedAt)
            .HasConversion(matchedAt => matchedAt.Value, dbId => MatchedAt.Of(dbId));

        builder.Property(m => m.Swipe)
            .HasDefaultValue(Swipe.Unknown)
            .HasConversion(s => s.ToString(), dbValue => Enum.Parse<Swipe>(dbValue));

        builder.Property(m => m.SwipedAt)
            .HasConversion(swipedAt => swipedAt.Value, dbValue => SwipedAt.Of(dbValue));
    }
}