using Inventory.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory.API.Data
{
    public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<StorageLevel> StorageLevels { get; set; }
        public DbSet<Item> Items { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUserRelations(modelBuilder);
            ConfigureStorageLevelRelations(modelBuilder);
            ConfigureItemProperties(modelBuilder);
            SeedUsersTable(modelBuilder);
        }

        private static void ConfigureUserRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.StorageLevels)
                .WithOne(s => s.User)
                .HasForeignKey(s => s.UserId);
        }

        private static void ConfigureStorageLevelRelations(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StorageLevel>()
                .HasMany(s => s.Children)
                .WithOne(s => s.Parent)
                .HasForeignKey(s => s.ParentId);

            modelBuilder.Entity<StorageLevel>()
                .HasMany(s => s.Items)
                .WithOne(i => i.StorageLevel)
                .HasForeignKey(i => i.StorageLevelId);
        }

        private static void ConfigureItemProperties(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(i => i.Quantity)
                .HasDefaultValue(1);
        }

        private static void SeedUsersTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User { UserId = 1, Username = "admin", Password = "securePassword", IsAdmin = true, CreatedOn = DateTime.UtcNow });
        }
    }
}