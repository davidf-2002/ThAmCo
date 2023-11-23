using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Catering.Data
{
    public class CateringDbContext : DbContext
    {
        // defining the database tables
        public DbSet<FoodBooking> FoodBookings { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<FoodItem> FoodItems { get; set; }
        public DbSet<MenuFoodItem> MenuFoodItems { get; set; }
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

            // relationship for MenuFood entity
            modelBuilder.Entity<MenuFoodItem>()
                .HasKey(mf => new { mf.MenuId, mf.FoodItemId});     // composite keys

            // relationship for Food entity
            modelBuilder.Entity<FoodItem>()
                .HasMany(f => f.MenuFoodItems)
                .WithOne(mf => mf.FoodItem)             // define Many-One MenuFood-Food
                .HasForeignKey(fi => fi.FoodItemId)     // foreign key
                .OnDelete(DeleteBehavior.Restrict);     // OnDelete prevents Food being deleted

            // relationship for Menu entity
            modelBuilder.Entity<Menu>()
                .HasMany(m => m.MenuFoodItems)
                .WithOne(mf => mf.Menu)                 // define Many-One MenuFood-Menu
                .HasForeignKey(mi => mi.MenuId)
                .OnDelete(DeleteBehavior.Restrict);



            // seed data
            modelBuilder.Entity<MenuFoodItem>().HasData(
                new MenuFoodItem { MenuId = 1, FoodItemId = 1 },
                new MenuFoodItem { MenuId = 2, FoodItemId = 2 }
            );

            modelBuilder.Entity<FoodItem>().HasData(
                new FoodItem { FoodItemId = 1, Name = "Salmon", Price = 10.00 },
                new FoodItem { FoodItemId = 2, Name = "Chicken", Price = 11.00}
            );

            modelBuilder.Entity<Menu>().HasData(
                new Menu { MenuId = 1, Name = "Menu1"},
                new Menu { MenuId = 2, Name = "Menu2"}
            );

            modelBuilder.Entity<FoodBooking>().HasData(
                new FoodBooking { BookingId = 1, ClientReferenceId = 1, NumberOfGuests = 1, MenuId = 1},
                new FoodBooking { BookingId = 2, ClientReferenceId = 2, NumberOfGuests = 2, MenuId = 2 }
            );
        }


    }
}
