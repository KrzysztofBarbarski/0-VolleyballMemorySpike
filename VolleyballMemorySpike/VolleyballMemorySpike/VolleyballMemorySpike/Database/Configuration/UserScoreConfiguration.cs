using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolleyballMemorySpike.Database.Entities;

namespace VolleyballMemorySpike.Database.Configuration
{
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

    public class UserScoreCombinedConfiguration : IEntityTypeConfiguration<UserScoreCombined>
    {
        public void Configure(EntityTypeBuilder<UserScoreCombined> builder)
        {
            builder.ToTable("UserScoresCombined");

            builder.HasKey(usc => usc.SessionId);

            builder.HasOne(usc => usc.User)
               .WithMany()
               .HasForeignKey(usc => usc.UserId)
               .IsRequired();

            builder.Property(usc => usc.TotalScore).IsRequired();

            builder.Property(usc => usc.UserId).IsRequired();

            builder.Property(usc => usc.CreatedDateUtc).IsRequired();

            builder.HasMany(usc => usc.UserScores)
                   .WithOne()
                   .HasForeignKey(us => us.SessionId)
                   .IsRequired();
        }
    }
}
