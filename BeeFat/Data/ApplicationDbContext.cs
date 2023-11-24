using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public DbSet<ApplicationUser> BeeFatUsers { get; set; }
        private IConfiguration _configuration;
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>()
                .OwnsOne(u => u.PersonName);
        }
    }
}
