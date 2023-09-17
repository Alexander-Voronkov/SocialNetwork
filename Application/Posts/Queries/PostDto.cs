﻿using Application.Reactions.Queries;
using AutoMapper;
using Domain.Entities;

namespace Application.Posts.Queries
{
    public class PostDto
    {
        public int? Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Body { get; set; }
        public int? OwnerId { get; set; }
        public IEnumerable<string>? Tags { get; set; }
    }
}