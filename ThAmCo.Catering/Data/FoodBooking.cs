using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodBooking
    {
        [Key] 
        public int BookingId { get; set; }
        public int ClientReferenceId { get; set; }
        public int NumberOfGuests { get; set; } 
        public int MenuId { get; set;}

        public Menu Menu { get; set; }      // Establishing Many-one relationship with Menu

    }
}
