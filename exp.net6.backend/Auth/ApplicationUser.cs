using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace exp.net6.backend.Auth
{
    public class ApplicationUser : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
    }

    
}
