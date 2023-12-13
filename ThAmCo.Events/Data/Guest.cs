using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Guest
    {
        public Guest()
        {
        }
        public Guest(int guestId, string lastName, string firstName)
        {
            GuestId = guestId;
            LastName = lastName;
            FirstName = firstName;
        }
        public int GuestId { get; set; } = 0;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;
        public List<EventGuest> Events { get; set; }

    }
}
