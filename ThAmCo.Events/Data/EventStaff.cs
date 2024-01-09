namespace ThAmCo.Events.Data
{
    public class EventStaff
    {
        public EventStaff()
        {
        }

        public EventStaff(int eventId, int staffId)
        {
            EventId = eventId;
            StaffId = staffId;
        }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int StaffId { get; set; }
        public Staff Staff { get; set; }

    }
}
