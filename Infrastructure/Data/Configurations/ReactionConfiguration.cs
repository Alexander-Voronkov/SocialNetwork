using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class ReactionConfiguration : IEntityTypeConfiguration<Reaction>
    {
        public void Configure(EntityTypeBuilder<Reaction> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.Owner)
                .WithMany(x => x.Reactions)
                .HasForeignKey(x=>x.OwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(x => x.Post)
                .WithMany(x => x.Reactions)
                .HasForeignKey(x => x.PostId)
                .IsRequired();

            builder
                .HasQueryFilter(x => !x.IsDeleted);
        }
    }
}
