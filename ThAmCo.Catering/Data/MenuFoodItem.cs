using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class MenuFoodItem
    {
        public MenuFoodItem(int foodItemId, int menuId)
        {
            FoodItemId = foodItemId;
            MenuId = menuId;
        }

        [Required]
        public int FoodItemId { get; set; }
        [Required]
        public int MenuId { get; set; }

        public FoodItem FoodItem { get; set; }      // Establishing Many-one relationship with Food

        public Menu Menu { get; set; }      // Establishing Many-one relationship with Menu
    }
}
