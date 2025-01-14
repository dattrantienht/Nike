﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Nike.Application.Dto
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public ICollection<IdentityRole> Roles { get; set; }
    }
}
