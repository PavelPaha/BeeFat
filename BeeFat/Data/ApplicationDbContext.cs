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
        
        public DbSet<FoodProduct> FoodProducts { get; set; }
        
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
                    .HasMany(f => f.FoodProducts) // Устанавливаем отношение один ко многим
                    .WithOne(fp => fp.Food) // Обратная навигационное свойство в сущности FoodProduct
                    .HasForeignKey(fp => fp.FoodId) // Внешний ключ
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade); // Удаляем FoodProduct при удалении Food
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

            base.OnModelCreating(modelBuilder);
        }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

    }
}