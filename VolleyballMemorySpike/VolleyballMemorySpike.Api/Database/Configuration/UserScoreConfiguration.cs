using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolleyballMemorySpike.Api.Database.Entities;

namespace VolleyballMemorySpike.Api.Database.Configuration;

public class UserScoreConfiguration : IEntityTypeConfiguration<UserScore>
{
    public void Configure(EntityTypeBuilder<UserScore> builder)
    {
        builder.ToTable("UserScores");

        builder.HasKey(us => new
        {
            us.SessionId,
            us.UserId,
            us.CreatedDateUtc
        });

        builder.HasOne(us => us.User)
           .WithMany()
           .HasForeignKey(us => us.UserId)
           .IsRequired();

        builder.Property(us => us.Score).IsRequired();

        builder.Property(us => us.UserId).IsRequired();

        builder.Property(us => us.SessionId).IsRequired();

        builder.Property(us => us.CreatedDateUtc).IsRequired();
    }
}
