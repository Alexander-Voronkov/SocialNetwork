﻿using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class ReactionsRepository : IReactionsRepository
    {
        private readonly ApplicationDbContext _context;
        public ReactionsRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public Task Add(Reaction entity)
        {
            return _context.Reactions.AddAsync(entity).AsTask();
        }

        public Task AddRange(IEnumerable<Reaction> entities)
        {
            return _context.Reactions.AddRangeAsync(entities);
        }

        public Task<IEnumerable<Reaction>> Find(Func<Reaction, bool> predicate)
        {
            return Task.FromResult(_context.Reactions.Where(predicate));
        }

        public Task<Reaction> Get(int id)
        {
            return _context.Reactions.FindAsync(id).AsTask()!;
        }

        public async Task<IEnumerable<Reaction>> GetAll()
        {
            return await _context.Reactions.ToArrayAsync();
        }

        public Task Remove(Reaction entity)
        {
            _context.Reactions.Remove(entity);
            return Task.CompletedTask;
        }

        public Task RemoveRange(IEnumerable<Reaction> entities)
        {
            _context.Reactions.RemoveRange(entities);
            return Task.CompletedTask;
        }
    }
}
