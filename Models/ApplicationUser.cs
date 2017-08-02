using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Models
{
    /// <summary>
    /// The application user.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
