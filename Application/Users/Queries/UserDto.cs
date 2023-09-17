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
        public string? PhoneNumber { get; set; }
        public string? Username { get; set; }
        public bool? EmailConfirmed { get; set; }
        public bool? PhoneConfirmed { get; set; }
        public ICollection<Friendship>? Friendships { get; set; }
        public ICollection<Post>? Posts { get; set; }
        public ICollection<Reaction>? Reactions { get; set; }
    }
}
