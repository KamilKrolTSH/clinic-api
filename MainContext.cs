using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ClinicApi.Models
{
    public class MainContext : IdentityDbContext<ApplicationUser>
    {
        public MainContext(DbContextOptions<MainContext> options)
            : base(options)
        {
        }

        public DbSet<UserData> UserDatas {get; set;}

    }
}