using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public List<MenuFoodItem> MenuFoodItems { get; set; }      // Establishing One-many relationship with MenuFoodItem
        public List<FoodBooking> Bookings { get; set; }        // Establishing One-many relationship with Bookings

    }
}
