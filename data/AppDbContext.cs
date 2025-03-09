using Microsoft.EntityFrameworkCore;
using todoListApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace todoListApi.data
{
    public class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tasks> Tasks { get; set; }

    }
}