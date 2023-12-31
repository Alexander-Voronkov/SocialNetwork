﻿using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repositories
{
    public class FriendshipsRepository : IFriendshipsRepository
    {
        private readonly ApplicationDbContext _context;
        public FriendshipsRepository(ApplicationDbContext context) 
        { 
            _context = context;
        }
        public Task Add(Friendship entity)
        {
            return _context.Friendships.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<Friendship> entities)
        {
            return _context.Friendships.AddRangeAsync(entities);
        }

        public Task<IQueryable<Friendship>> FindMany(Expression<Func<Friendship, bool>> predicate)
        {
            return Task.FromResult(_context.Friendships.Where(predicate));
        }

        public Task<Friendship?> FindOne(Expression<Func<Friendship, bool>> predicate)
        {
            return _context.Friendships.FirstOrDefaultAsync(predicate);
        }

        public Task<Friendship?> Get(int id)
        {
            return _context.Friendships.FirstOrDefaultAsync(x=>x.Id == id)!;
        }

        public Task<IQueryable<Friendship>> GetAll()
        {
            return Task.FromResult(_context.Friendships.AsQueryable());
        }

        public Task Remove(Friendship entity)
        {
            _context.Friendships.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Friendship> entities)
        {
            _context.Friendships.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
