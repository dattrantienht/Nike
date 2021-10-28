using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.Collections.Generic;

namespace Nike.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser 
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Gsm { get; set; }

        public ICollection<IdentityRole> Roles { get; set; }
    }
}
