using System.ComponentModel.DataAnnotations;

namespace ThAmCo.Catering.DTO
{
    public class FoodItemDTO
    {
        public int FoodItemId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
