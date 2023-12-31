﻿using Application.Common.Models;
using MediatR;

namespace Application.Friendships.Queries.GetUsersFriendships
{
    public class GetUsersFriendshipsQuery : IRequest<PaginatedList<FriendshipDto>>
    {
        public int? UserId { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
    }
}
