using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.CommentBody)
                .IsRequired()
                .HasMaxLength(500);

            builder
                .HasOne(x => x.Owner)
                .WithMany()
                .HasForeignKey(x => x.OwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(x => x.Post)
                .WithMany()
                .HasForeignKey(x => x.PostId)
                .IsRequired();

            builder
                .HasOne(x => x.ReferringComment)
                .WithMany()
                .HasForeignKey(x => x.ReferringCommentId)
                .IsRequired(false);

            builder
                .HasMany(x => x.DependentComments)
                .WithOne()
                .HasForeignKey(x => x.ReferringCommentId)
                .IsRequired(false);

            builder
                .HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
