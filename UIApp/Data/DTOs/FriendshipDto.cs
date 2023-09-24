﻿namespace Data.DTOs
{
    public class FriendshipDto
    {
        public int? Id { get; set; }
        public int? FirstUserId { get; set; }
        public int? SecondUserId { get; set; }
        public UserDto? FirstUser { get; set; }
        public UserDto? SecondUser { get; set; }
    }
}
