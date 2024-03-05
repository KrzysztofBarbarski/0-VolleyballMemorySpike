using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VolleyballMemorySpike.Database.Entities.Users;

namespace VolleyballMemorySpike.Database.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(user => user.Id);

            builder.Property(user => user.ObjectIdentifier)
                   .IsRequired();

            builder.Property(user => user.NickName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(user => user.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.HasIndex(user => user.Email)
                   .IsUnique();

            builder.Property(user => user.CreatedDateUtc)
                   .IsRequired();
        }
    }
}
