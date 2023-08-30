using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Users.Queries
{
    public class UserDto
    {
        public int? Id { get; set; }
        public string? Email { get; set; }
        public ICollection<Friendship>? Friendships { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Reaction>? Reactions { get; set; }

        private class Mapping : Profile
        {
            public Mapping()
            {
                CreateMap<User, UserDto>();
            }
        }
    }
}
