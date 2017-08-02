using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Models;

namespace DataInitializer
{
    /// <summary>
    /// The db initializer.
    /// </summary>
    public class DbInitializer : IDbInitializer
    {
        /// <summary>
        /// The _context.
        /// </summary>
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// The _user manager.
        /// </summary>
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// The _role manager.
        /// </summary>
        private readonly RoleManager<IdentityRole> _roleManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="DbInitializer"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="userManager">
        /// The user manager.
        /// </param>
        /// <param name="roleManager">
        /// The role manager.
        /// </param>
        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        /// <summary>
        /// The initialize.
        /// </summary>
        public async void Initialize()
        {
            _context.Database.EnsureCreated();

            if (_context.Roles.Any(r => r.Name == "Administrator"))
            {
                return;
            }

            await _roleManager.CreateAsync(new IdentityRole
            {
                Name = "Administrator"
            });

            if (_context.Users.Any())
            {
                return;
            }

            var user = new ApplicationUser
            {
                Name = "程旸",
                UserName = "szhchengyang@163.com",
                Email = "szhchengyang@163.com",
                PhoneNumber = "18251165658"
            };
            await _userManager.CreateAsync(user, "Baobao329286;");
            await _userManager.AddToRoleAsync(user, "Administrator");
        }
    }
}
