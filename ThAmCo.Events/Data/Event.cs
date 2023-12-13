using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Event
    {
        public Event()
        {
        }

        public Event(int eventId, string name, DateTime dateAndTime, int bookingId, int reference)
        {
            EventId = eventId;
            Name = name;
            DateAndTime = dateAndTime;
            BookingId = bookingId;
            Reference = reference;
        }

        public int EventId { get; set; }

        [Required]
        public string Name { get; set; }
        public DateTime DateAndTime { get; set; }

        public int MenuId { get; set; } = 0;     // Catering
        public int BookingId { get; set; } = 0;

        public string EventTypeId { get; set; } = null;          // Venues
        public int Reference { get; set; } = 0;

        public List<EventStaff> Staffs { get; set; }
        public List<EventGuest> Guests { get; set; }

        // Calculated property for total guests count
        [Display(Name = "Total Guests")]
        public int TotalGuestsCount => Guests?.Count ?? 0;

    }

}
