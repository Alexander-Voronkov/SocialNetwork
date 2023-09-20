using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Body)
                .IsRequired()
                .HasMaxLength(100000);

            builder
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder
                .Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder
                .Property(x => x.Tags)
                .IsRequired()
                .HasConversion(
                    x => string.Join(',', x),
                    x => x.Split(',', StringSplitOptions.RemoveEmptyEntries));

            builder
                .HasOne(x => x.Owner)
                .WithMany(x => x.Posts)
                .HasForeignKey(x => x.OwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
