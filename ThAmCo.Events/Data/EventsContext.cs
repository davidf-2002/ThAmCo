using Microsoft.EntityFrameworkCore;

namespace ThAmCo.Events.Data
{
    public class EventsContext : DbContext
    {
        public string DbPath { get; set; }
        public EventsContext()
        {
            var folder = Environment.SpecialFolder.MyDocuments;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "Thamco.Events.db");
        }

        public DbSet<Event> Events { get; set; }
        public DbSet<Staff> Staff { get; set; }
        public DbSet<Guest> Guest { get; set; }
        public DbSet<EventStaff> EventStaff { get; set; }
        public DbSet<EventGuest> EventGuest { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // EventStaff
            modelBuilder.Entity<EventStaff>()
                .HasKey(es => new { es.EventId, es.StaffId });

            modelBuilder.Entity<EventStaff>()
                .HasOne(es => es.Staff)
                .WithMany(es => es.Events)
                .HasForeignKey(es => es.StaffId);

            modelBuilder.Entity<EventStaff>()
                .HasOne(sp => sp.Event)
                .WithMany(sp => sp.Staffs)
                .HasForeignKey(sp => sp.EventId);


            // EventGuest
            modelBuilder.Entity<EventGuest>()
                .HasKey(eg => new { eg.EventId, eg.GuestId });

            modelBuilder.Entity<EventGuest>()
                .HasOne(eg => eg.Guest)
                .WithMany(eg => eg.Events)
                .HasForeignKey(eg => eg.GuestId);

            modelBuilder.Entity<EventGuest>()
                .HasOne(ep => ep.Event)
                .WithMany(ep => ep.Guests)
                .HasForeignKey(ep => ep.EventId);


            modelBuilder.Entity<Event>().HasData(
                new Event(1, "Event 1", new DateTime(2023, 01, 10, 10, 0, 0), 47531, 59310),
                new Event(2, "Event 2", new DateTime(2023, 01, 11, 10, 0, 0), 35105, 93821),
                new Event(3, "Event 3", new DateTime(2023, 09, 01, 12, 0, 0), 62318, 21523)
                );

            modelBuilder.Entity<Staff>().HasData(
                new Staff(1, "Parkinson", "Josh"),
                new Staff(2, "Swales", "James"),
                new Staff(3, "Harrison", "Lee"),
                new Staff(4, "Hadfield", "Thomas"),
                new Staff(5, "Tomlinson", "Greg"),
                new Staff(6, "Blackburn", "Billy")
                );

            modelBuilder.Entity<Guest>().HasData(
                new Guest(1, "Emmerson", "Samuel"),
                new Guest(2, "Flanigan", "Tony"),
                new Guest(3, "Bartwick", "Jonathon"),
                new Guest(4, "Greenaway", "Benjamin"),
                new Guest(5, "Bainbridge", "Stephen"),
                new Guest(6, "Bunny", "Matthew")
                );

            modelBuilder.Entity<EventStaff>().HasData(
                new EventStaff(1, 1),
                new EventStaff(1, 2),
                new EventStaff(2, 3),
                new EventStaff(2, 4),
                new EventStaff(3, 5),
                new EventStaff(3, 6)
                );

            modelBuilder.Entity<EventGuest>().HasData(
                new EventGuest(1, 1),
                new EventGuest(1, 2),
                new EventGuest(2, 3),
                new EventGuest(2, 4),
                new EventGuest(3, 5),
                new EventGuest(3, 6)
                );

        }
    }
}
