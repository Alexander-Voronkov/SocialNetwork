using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class FriendshipConfiguration: IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.FirstUser)
                .WithMany()
                .HasForeignKey(x=>x.FirstUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(x => x.SecondUser)
                .WithMany()
                .HasForeignKey(x=>x.SecondUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .Property(x => x.IsAccepted)
                .HasDefaultValue(false);
        }
    }
}
