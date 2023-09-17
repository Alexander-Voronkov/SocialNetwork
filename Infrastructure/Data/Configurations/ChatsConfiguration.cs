using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class ChatsConfiguration : IEntityTypeConfiguration<Chat>
    {
        public void Configure(EntityTypeBuilder<Chat> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .HasMany(x => x.Messages)
                .WithOne()
                .HasForeignKey(x => x.ChatId);

            builder
                .HasOne(x => x.FirstUser)
                .WithMany()
                .HasForeignKey(x => x.FirstUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(x => x.SecondUser)
                .WithMany()
                .HasForeignKey(x => x.SecondUserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
