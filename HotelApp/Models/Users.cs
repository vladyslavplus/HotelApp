﻿using Microsoft.AspNetCore.Identity;

namespace HotelApp.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
