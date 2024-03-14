﻿using Microsoft.EntityFrameworkCore;
using VolleyballMemorySpike.Database.Entities;
using VolleyballMemorySpike.Database.Entities.Users;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }

    public DbSet<UserScore> UsersScores { get; set; }

    public DbSet<UserScoreCombined> UserScoresCombined { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
