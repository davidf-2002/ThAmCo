using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodBooking
    {
        public FoodBooking(int bookingId, int clientReferenceId, int numberOfGuests, int menuId)
        {
            BookingId = bookingId;
            ClientReferenceId = clientReferenceId;
            NumberOfGuests = numberOfGuests;
            MenuId = menuId;
        }

        [Key] 
        public int BookingId { get; set; }
        [Required]
        public int ClientReferenceId { get; set; }
        public int NumberOfGuests { get; set; } 
        public int MenuId { get; set;}

        public Menu Menu { get; set; }      // Establishing Many-one relationship with Menu

    }
}
