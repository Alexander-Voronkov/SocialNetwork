﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) 
        {
            builder
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .Property(x => x.Username)
                .IsRequired();

            builder
                .Property(x => x.EmailConfirmed)
                .IsRequired();

            builder
               .Property(x => x.PhoneConfirmed)
               .IsRequired();

            builder
                .Property(x => x.Email)
                .IsRequired();

            builder
                .Property(x => x.PhoneNumber)
                .IsRequired();

            builder
                .HasMany(x => x.Friendships);

            builder
                .HasMany(x => x.Friendrequests);

            builder
                .HasMany(x => x.Chats);

            builder
                .HasMany(x => x.Posts)
                .WithOne()
                .HasForeignKey(x => x.OwnerId);

            builder
                .HasMany(x => x.Comments)
                .WithOne()
                .HasForeignKey(x => x.OwnerId);
        }
    }
}
