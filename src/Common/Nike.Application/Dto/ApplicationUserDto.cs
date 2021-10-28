using Microsoft.AspNetCore.Identity;

namespace Nike.Application.Dto
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }
        public virtual IdentityRole Role { get; set; }
    }
}
