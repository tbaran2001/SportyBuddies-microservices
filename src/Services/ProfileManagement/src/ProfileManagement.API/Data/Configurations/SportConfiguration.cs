using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfileManagement.API.Profiles.Models;
using ProfileManagement.API.Sports.Models;
using ProfileManagement.API.Sports.ValueObjects;

namespace ProfileManagement.API.Data.Configurations;

public class SportConfiguration : IEntityTypeConfiguration<Sport>
{
    public void Configure(EntityTypeBuilder<Sport> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id)
            .ValueGeneratedNever()
            .HasConversion(sport => sport.Value, dbId => SportId.Of(dbId));
    }
}