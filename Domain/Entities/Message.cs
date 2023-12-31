﻿using Domain.Common;

namespace Domain.Entities
{
    public class Message : BaseAuditableEntity<int>, ISoftDeletable
    {
        public bool IsDeleted { get; set; }
        public int? ChatId { get; set; }
        public int? OwnerId { get; set; }
        public string? MessageBody { get; set; }
        public Chat? Chat { get; set; }
        public User? Owner { get; set; }
    }
}
