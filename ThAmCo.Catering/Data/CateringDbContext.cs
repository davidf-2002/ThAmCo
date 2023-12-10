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
                new MenuFoodItem ( 1, 1 ),
                new MenuFoodItem ( 1, 2 ),      // test to see if many-one relationship works
                new MenuFoodItem ( 2, 1 ),
                new MenuFoodItem ( 3, 1 ),
                new MenuFoodItem ( 4, 1 ),
                new MenuFoodItem ( 5, 2 ),
                new MenuFoodItem ( 6, 2 ),
                new MenuFoodItem ( 7, 2 ),
                new MenuFoodItem ( 8, 2 ),
                new MenuFoodItem ( 9, 3 ),
                new MenuFoodItem ( 10, 3 ),
                new MenuFoodItem ( 11, 3 ),
                new MenuFoodItem ( 12, 3 ),
                new MenuFoodItem ( 13, 4 ),
                new MenuFoodItem ( 14, 4 ),
                new MenuFoodItem ( 15, 4 ),
                new MenuFoodItem ( 16, 4 )
            );

            modelBuilder.Entity<FoodItem>().HasData(
                new FoodItem ( 1, "Spaghetti Bolognese", 8.50),
                new FoodItem ( 2, "Chicken Alfredo", 9.00),
                new FoodItem ( 3, "Prawn Linguine", 10.50),
                new FoodItem ( 4, "Pizza", 9.00),
                new FoodItem ( 5, "Beef Wellington", 12.00 ),
                new FoodItem ( 6, "Shepards Pie", 9.00 ),
                new FoodItem ( 7, "Fish and Chips", 9.50 ),
                new FoodItem ( 8, "Toad in the Hole", 9.00 ),
                new FoodItem ( 9, "Peking Roasted Duck", 12.00 ),
                new FoodItem ( 10, "Kung Pao Chicken", 9.00 ),
                new FoodItem ( 11, "Sweet and Sour Pork", 10.00 ),
                new FoodItem ( 12, "Beef Char Siu", 10.50 ),
                new FoodItem ( 13, "Chicken Tikka", 10.00 ),
                new FoodItem ( 14, "Chicken Vindaloo", 10.00 ),
                new FoodItem ( 15, "Lamb Biryani", 12.00 ),
                new FoodItem ( 16, "Rogan Josh", 10.50 )
            );

            modelBuilder.Entity<Menu>().HasData(
                new Menu ( 1, "Italian" ),
                new Menu ( 2, "British" ),
                new Menu ( 3, "Chinese" ),
                new Menu ( 4, "Indian" )
            );

            modelBuilder.Entity<FoodBooking>().HasData(
                new FoodBooking ( 1, 1, 1, 1 ),
                new FoodBooking ( 2, 2, 2, 2 ),
                new FoodBooking ( 3, 3, 3, 3 ),
                new FoodBooking ( 4, 4, 4, 4 )
                
            );
        }


    }
}
