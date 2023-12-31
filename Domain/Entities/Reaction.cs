﻿using Domain.Common;
using Domain.Enums;

namespace Domain.Entities
{
    public class Reaction : BaseAuditableEntity<int>, ISoftDeletable
    {
        public bool IsDeleted { get; set; }
        public ReactionType? Type { get; set; }
        public int? OwnerId { get; set; }
        public int? PostId { get; set; }
        public Post? Post { get; set; }
        public User? Owner { get; set;}
    }
}
