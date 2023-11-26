using BeeFat.Domain.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public DbSet<ApplicationUser> BeeFatUsers { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodProduct> FoodProducts { get; set; }
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

            modelBuilder.Entity<Food>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired().HasMaxLength(255);
            });

            modelBuilder.Entity<FoodProduct>()
                .HasOne(fp => fp.Food)
                .WithOne()
                .HasForeignKey<FoodProduct>(fp => fp.FoodId)
                .OnDelete(DeleteBehavior.Cascade);
        }



    }
}