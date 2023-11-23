using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.Data
{
    public class FoodItem
    {
        [Key]
        public int FoodItemId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set;}
        public double Price { get; set;}

        public List<MenuFoodItem> MenuFoodItems { get; set; }      // Establishing One-many relationship with MenuFood class
    }
}
