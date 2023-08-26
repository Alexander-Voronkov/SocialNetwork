using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        public IFriendshipsRepository FriendshipsRepository { get; private set; }
        public IReactionsRepository ReactionsRepository { get; private set; }
        public IPostsRepository PostsRepository { get; private set; }
        public UnitOfWork(IApplicationDbContext context)
        {
            _context = context;
            FriendshipsRepository = new FriendshipsRepository(context);
            PostsRepository = new PostsRepository(context);
            ReactionsRepository = new ReactionsRepository(context);
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
