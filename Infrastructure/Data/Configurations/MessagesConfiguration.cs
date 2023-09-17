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
    public class MessagesConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.MessageBody)
                .HasMaxLength(500)
                .IsRequired();

            builder
                .HasOne(x => x.Owner)
                .WithMany(x => x.Messages)
                .HasForeignKey(x => x.OwnerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.ClientSetNull);

            builder
                .HasOne(x => x.Chat)
                .WithMany(x => x.Messages)
                .IsRequired()
                .HasForeignKey(x => x.ChatId);
        }
    }
}
