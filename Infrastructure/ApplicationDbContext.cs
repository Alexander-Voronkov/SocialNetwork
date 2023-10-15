using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            base.Database.EnsureCreated();
        }
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Reaction> Reactions => Set<Reaction>();
        public DbSet<Friendship> Friendships => Set<Friendship>();
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
