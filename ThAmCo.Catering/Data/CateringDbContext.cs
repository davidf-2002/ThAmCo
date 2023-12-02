using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Catering.Data
{
    public class CateringDbContext : DbContext
    {
        // defining database properties
        public DbSet<FoodBooking> FoodBooking { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<FoodItem> FoodItem { get; set; }
        public DbSet<MenuFoodItem> MenuFoodItem { get; set; }
        private string DbPath { get; set; } = string.Empty;


        // constructor to set up DbPath
        public CateringDbContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "thamco.catering.db");
        }


        // OnConfiguring to specify that the SQLite database engine will be used
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        // OnModelCreating to create entity relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuFoodItem>().HasKey(mf => new { mf.MenuId, mf.FoodItemId });     // composite keys

            // relationship for Menu entity
            modelBuilder.Entity<Menu>()
                .HasMany(m => m.MenuFoodItem)
                .WithOne(ms => ms.Menu)                 // define Many-One MenuFoodItem-Menu
                .HasForeignKey(ms => ms.MenuId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<FoodItem>()
            .HasMany(m => m.MenuFoodItem)
            .WithOne(ms => ms.FoodItem)                 // define Many-One MenuFoodItem-FoodItem
            .HasForeignKey(ms => ms.FoodItemId)
            .OnDelete(DeleteBehavior.Restrict);

            //seed data
            modelBuilder.Entity<MenuFoodItem>().HasData(
                new MenuFoodItem (1, 1 ),
                new MenuFoodItem (2, 2)
            );

            modelBuilder.Entity<FoodItem>().HasData(
                new FoodItem ( 1, "Salmon", 10.00 ),
                new FoodItem ( 2, "Chicken", 11.00)
            );

            modelBuilder.Entity<Menu>().HasData(
                new Menu ( 1, "Menu1"),
                new Menu ( 2, "Menu2")
            );

            modelBuilder.Entity<FoodBooking>().HasData(
                new FoodBooking ( 1, 1, 1, 1 ),
                new FoodBooking ( 2, 2, 2, 2 )
            );
        }


    }
}
