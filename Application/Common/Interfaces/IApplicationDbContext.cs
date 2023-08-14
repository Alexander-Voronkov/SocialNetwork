using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Reaction> Reactions { get; }
        DbSet<Friendship> Friendships { get; }
        DbSet<Post> Posts { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
