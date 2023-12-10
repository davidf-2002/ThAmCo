using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.DTO
{
    public class BookingDTO
    {
        [Required]
        public int ClientReferenceId { get; set; }
        public int NumberOfGuests { get; set; }
        public int MenuId { get; set; }
        public int BookingId { get; set; }
    }
}
