using Application.Common.Interfaces.Repositories;
using Domain.Interfaces;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IFriendshipsRepository FriendshipsRepository { get; }
        public IReactionsRepository ReactionsRepository { get; }
        public IPostsRepository PostsRepository { get; }
        public IFriendrequestsRepository FriendrequestsRepository { get; }
        public ICommentsRepository CommentsRepository { get; }
        public IUsersRepository UsersRepository { get; }
        public IChatsRepository ChatsRepository { get; }
        public IMessagesRepository MessagesRepository { get; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            FriendshipsRepository = new FriendshipsRepository(context);
            PostsRepository = new PostsRepository(context);
            ReactionsRepository = new ReactionsRepository(context);
            FriendrequestsRepository = new FriendrequestsRepository(context);
            CommentsRepository = new CommentsRepository(context);
            UsersRepository = new UsersRepository(context);
            ChatsRepository = new ChatsRepository(context);
            MessagesRepository = new MessagesRepository(context);
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
