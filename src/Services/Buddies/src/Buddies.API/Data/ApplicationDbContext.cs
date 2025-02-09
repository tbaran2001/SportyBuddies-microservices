﻿using Buddies.API.Buddies.Models;
using Buddies.API.Buddies.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Buddies.API.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Buddy> Buddies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}