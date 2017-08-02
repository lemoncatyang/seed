using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;

namespace DataInitializer
{
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// The application db context.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationDbContext"/> class.
        /// </summary>
        /// <param name="options">
        /// The options.
        /// </param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
