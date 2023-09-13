using Domain.Entities;
using MediatR;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Friendships.Queries.GetAllFriendships
{
    public class GetAllFriendshipsQuery : IRequest<IEnumerable<FriendshipDto>>
    {
    }
}
