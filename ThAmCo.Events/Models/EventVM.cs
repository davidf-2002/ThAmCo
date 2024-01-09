namespace ThAmCo.Events.Models
{
    public class EventVM
    {
        public int EventId { get; set; }
        public string Name { get; set; }
        public DateTime DateAndTime { get; set; }
        public int MenuId { get; set; }
        public int BookingId { get; set; }
        public string EventTypeId { get; set; }
        public string EventTypeName { get; set; }
        public string Reference { get; set; }
        public string Title { get; set; }
        public int TotalGuestsCount { get; set; }
        public bool isFirstAider { get; set; } 
    }
}
