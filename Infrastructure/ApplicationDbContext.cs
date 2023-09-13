using Application.Common.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Reaction> Reactions => Set<Reaction>();
        public DbSet<Friendship> Friendships => Set<Friendship>();
        public DbSet<Friendrequest> FriendRequests => Set<Friendrequest>();
        public DbSet<User> Users => Set<User>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Message> Messages => Set<Message>();
        public DbSet<Chat> Chats => Set<Chat>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
