using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodItem
    {
        public FoodItem(int foodItemId, string name, double price)
        {
            FoodItemId = foodItemId;
            Name = name;
            Price = price;
        }

        [Key]
        public int FoodItemId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set;}
        public double Price { get; set;}

        public List<MenuFoodItem> MenuFoodItem { get; set; }      // Establishing One-many relationship with MenuFood class
    }
}
