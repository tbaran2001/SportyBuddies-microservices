using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Sport.API.Data.Configuration;

public class SportConfiguration : IEntityTypeConfiguration<Sports.Models.Sport>
{
    public void Configure(EntityTypeBuilder<Sports.Models.Sport> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .ValueGeneratedNever()
            .HasConversion(sport => sport.Value, dbId => SportId.Of(dbId));

        builder.OwnsOne(p => p.Name, a =>
        {
            a.Property(p => p.Value)
                .HasColumnName(nameof(Sports.Models.Sport.Name))
                .IsRequired();
        });

        builder.OwnsOne(p => p.Description, a =>
        {
            a.Property(p => p.Value)
                .HasColumnName(nameof(Sports.Models.Sport.Description))
                .IsRequired();
        });
    }
}