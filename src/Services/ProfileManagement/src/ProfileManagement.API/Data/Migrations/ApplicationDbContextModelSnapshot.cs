﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProfileManagement.API.Data;

#nullable disable

namespace ProfileManagement.API.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProfileManagement.API.Profiles.Models.Profile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValue("Unknown");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("ProfileManagement.API.Profiles.Models.ProfileSport", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("ProfileId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("SportId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("SportId");

                    b.ToTable("ProfileSports");
                });

            modelBuilder.Entity("ProfileManagement.API.Sports.Models.Sport", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("Sports");
                });

            modelBuilder.Entity("ProfileManagement.API.Profiles.Models.Profile", b =>
                {
                    b.OwnsOne("ProfileManagement.API.Profiles.ValueObjects.BirthDate", "BirthDate", b1 =>
                        {
                            b1.Property<Guid>("ProfileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("datetime2")
                                .HasColumnName("BirthDate");

                            b1.HasKey("ProfileId");

                            b1.ToTable("Profiles");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.OwnsOne("ProfileManagement.API.Profiles.ValueObjects.Description", "Description", b1 =>
                        {
                            b1.Property<Guid>("ProfileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Description");

                            b1.HasKey("ProfileId");

                            b1.ToTable("Profiles");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.OwnsOne("ProfileManagement.API.Profiles.ValueObjects.Name", "Name", b1 =>
                        {
                            b1.Property<Guid>("ProfileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("Name");

                            b1.HasKey("ProfileId");

                            b1.ToTable("Profiles");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.OwnsOne("ProfileManagement.API.Profiles.ValueObjects.Preferences", "Preferences", b1 =>
                        {
                            b1.Property<Guid>("ProfileId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<int>("MaxAge")
                                .HasColumnType("int");

                            b1.Property<int>("MaxDistance")
                                .HasColumnType("int");

                            b1.Property<int>("MinAge")
                                .HasColumnType("int");

                            b1.Property<string>("PreferredGender")
                                .IsRequired()
                                .HasColumnType("nvarchar(max)");

                            b1.HasKey("ProfileId");

                            b1.ToTable("Profiles");

                            b1.WithOwner()
                                .HasForeignKey("ProfileId");
                        });

                    b.Navigation("BirthDate");

                    b.Navigation("Description");

                    b.Navigation("Name");

                    b.Navigation("Preferences");
                });

            modelBuilder.Entity("ProfileManagement.API.Profiles.Models.ProfileSport", b =>
                {
                    b.HasOne("ProfileManagement.API.Profiles.Models.Profile", null)
                        .WithMany("ProfileSports")
                        .HasForeignKey("ProfileId");

                    b.HasOne("ProfileManagement.API.Sports.Models.Sport", null)
                        .WithMany()
                        .HasForeignKey("SportId");
                });

            modelBuilder.Entity("ProfileManagement.API.Profiles.Models.Profile", b =>
                {
                    b.Navigation("ProfileSports");
                });
#pragma warning restore 612, 618
        }
    }
}
