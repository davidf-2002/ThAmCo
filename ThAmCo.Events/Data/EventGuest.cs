namespace ThAmCo.Events.Data
{
    public class EventGuest
    {
        public EventGuest()
        {
        }

        public EventGuest(int eventId, int guestId, bool hasAttended)
        {
            EventId = eventId;
            GuestId =  guestId;
            HasAttended = hasAttended;

        }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }
        public bool HasAttended { get; set; }

    }
}
