using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models
{
    //Add aditional properties to the ApplicationUser class as needed for your application.For example, 
    //you might want to add properties for FirstName, LastName, or any other custom user information that is relevant to your application.
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsActive { get; set; }
        public string RefreshToken { get; set; }

        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
