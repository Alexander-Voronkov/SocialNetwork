using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDbContext _context;
        public UsersRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Add(User entity)
        {
            return _context.Users.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<User> entities)
        {
            return _context.Users.AddRangeAsync(entities);
        }

        public Task<IQueryable<User>> Find(Func<User, bool> predicate)
        {
            return Task.FromResult(_context.Users.Where(predicate).AsQueryable());
        }

        public Task<User> Get(int id)
        {
            return _context.Users.FindAsync(id).AsTask();
        }

        public Task<IQueryable<User>> GetAll()
        {
            return Task.FromResult(_context.Users.AsQueryable());
        }

        public Task Remove(User entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<User> entities)
        {
            _context.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
