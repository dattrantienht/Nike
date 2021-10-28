using Microsoft.AspNetCore.Identity;

namespace Nike.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser 
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Gsm { get; set; }

        public virtual IdentityRole Role { get; set; }
    }
}
