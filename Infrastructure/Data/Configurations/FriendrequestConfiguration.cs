using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations
{
    public class FriendrequestConfiguration : IEntityTypeConfiguration<Friendrequest>
    {
        public void Configure(EntityTypeBuilder<Friendrequest> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasOne(x => x.From)
                .WithMany()
                .HasForeignKey(x => x.FromUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(x => x.To)
                .WithMany()
                .HasForeignKey(x => x.ToUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
