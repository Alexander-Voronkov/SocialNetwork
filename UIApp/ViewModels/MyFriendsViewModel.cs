﻿using Data.DTOs;
using UIApp.Models;

namespace UIApp.ViewModels
{
    public class MyFriendsViewModel
    {
        public PaginatedList<UserDto>? Friends { get; set; }
    }
}
