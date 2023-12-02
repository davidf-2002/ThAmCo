using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class Menu
    {
        public Menu(int menuId, string name)
        {
            MenuId = menuId;
            Name = name;
        }

        [Key]
        public int MenuId { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public List<MenuFoodItem> MenuFoodItem { get; set; }      // Establishing One-many relationship with MenuFoodItem
        public List<FoodBooking> FoodBooking { get; set; }        // Establishing One-many relationship with Bookings

    }
}
