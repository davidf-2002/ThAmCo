namespace ThAmCo.Events.Data
{
    public class EventGuest
    {
        public EventGuest()
        {
        }

        public EventGuest(int eventId, int guestId)
        {
            EventId = eventId;
            GuestId =  guestId;
        }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int GuestId { get; set; }
        public Guest Guest { get; set; }

    }
}
