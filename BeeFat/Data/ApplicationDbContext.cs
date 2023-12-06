using BeeFat.Domain.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApplicationUser> BeeFatUsers { get; set; }

        public DbSet<Food> Foods { get; set; }

        public DbSet<FoodProductGram> FoodProductsGrams { get; set; }

        public DbSet<FoodProductPiece> FoodProductsPieces { get; set; }

        private IConfiguration _configuration;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) :
            base(options)
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

                entity.OwnsOne(f => f.Macronutrient, macronutrient =>
                {
                    macronutrient.Property(m => m.Proteins);
                    macronutrient.Property(m => m.Fats);
                    macronutrient.Property(m => m.Carbohydrates);
                    macronutrient.Property(m => m.Calories);
                });
            });

            modelBuilder.Entity<FoodProduct>()
                .HasOne(fp => fp.Food)
                .WithOne()
                .HasForeignKey<FoodProduct>(fp => fp.FoodId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Track>()
                .HasMany<FoodProduct>(t => t.FoodProducts)
                .WithOne(fp => fp.Track)
                .HasForeignKey(fp => fp.TrackId)
                .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}