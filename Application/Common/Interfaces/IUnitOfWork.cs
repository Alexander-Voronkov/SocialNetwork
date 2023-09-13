using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.Interfaces.Repositories;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFriendshipsRepository FriendshipsRepository { get; }
        IReactionsRepository ReactionsRepository { get; }
        IPostsRepository PostsRepository { get; }
        IUsersRepository UsersRepository { get; }
        ICommentsRepository CommentsRepository { get; }
        IFriendrequestsRepository FriendrequestsRepository { get; }
        IChatsRepository ChatsRepository { get; }
        IMessagesRepository MessagesRepository { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
