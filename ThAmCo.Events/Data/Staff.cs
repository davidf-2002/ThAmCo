using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Events.Data
{
    public class Staff
    {
        public Staff()
        {
        }
        public Staff(int staffId, string lastName, string firstName)
        {
            StaffId = staffId;
            LastName = lastName;
            FirstName = firstName;
        }
        public int StaffId { get; set; } = 0;

        [Required]
        public string LastName { get; set; } = null!;

        [Required]
        public string FirstName { get; set; } = null!;

        public List<EventStaff> Events { get; set; }
    }
}
