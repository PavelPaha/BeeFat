using BeeFat.Domain.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BeeFat.Data
{
    public class ApplicationDbContext : IdentityUserContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> BeeFatUsers { get; set; }

        public DbSet<Food> Foods { get; set; }
        public DbSet<FoodGram> FoodsGram { get; set; }
        public DbSet<FoodPiece> FoodsPiece { get; set; }

        public DbSet<FoodProductGram> FoodProductsGrams { get; set; }

        public DbSet<FoodProductPiece> FoodProductsPieces { get; set; }
        
        public DbSet<FoodProduct> FoodProducts { get; set; }
        
        public DbSet<JournalFood> JournalFoods { get; set; }
        
        public DbSet<JournalFoodGram> JournalFoodsGram { get; set; }
        
        public DbSet<JournalFoodPiece> JournalFoodsPiece { get; set; }
        
        public DbSet<Track> Tracks { get; set; }
        
        public DbSet<Journal> Journals { get; set; }

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
                
                entity
                    .HasMany(f => f.FoodProducts)
                    .WithOne(fp => fp.Food)
                    .HasForeignKey(fp => fp.FoodId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<FoodProduct>()
                .HasOne(fp => fp.Food)
                .WithMany(f => f.FoodProducts)
                .HasForeignKey(fp => fp.FoodId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Track>()
                .HasMany(t => t.FoodProducts)
                .WithOne(fp => fp.Track)
                .HasForeignKey(fp => fp.TrackId)
                .IsRequired();

            modelBuilder.Entity<Journal>(entity =>
            {
                entity.HasMany(f => f.FoodProducts)
                    .WithOne(fp => fp.Journal);
            });

            modelBuilder.Entity<JournalFood>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.Name).IsRequired().HasMaxLength(255);
                entity.Property(f => f.FoodProductReference).IsRequired();

                entity.OwnsOne(f => f.Macronutrient, macronutrient =>
                {
                    macronutrient.Property(m => m.Proteins).IsRequired();
                    macronutrient.Property(m => m.Fats).IsRequired();
                    macronutrient.Property(m => m.Carbohydrates).IsRequired();
                    macronutrient.Property(m => m.Calories).IsRequired();
                });
            });

            base.OnModelCreating(modelBuilder);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

    }
}