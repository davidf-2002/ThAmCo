using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ThAmCo.Catering.Data;

namespace ThAmCo.Catering.DTO
{
    public class MenuFoodItemDTO       // DTO to show foods along with the Menu names
    {
        public int MenuId { get; set; }
        public string Name { get; set; }
        public List<FoodItemDTO> FoodItems { get; set; }

        public static MenuFoodItemDTO BuildDTO(Menu menu)
        {
            List<DTO.FoodItemDTO> foodItems = new List<DTO.FoodItemDTO>();
            MenuFoodItemDTO dto = new MenuFoodItemDTO();

            foodItems = menu.MenuFoodItem.Select(m => new FoodItemDTO 
            { 
                FoodItemId = m.FoodItemId,
                Name = m.FoodItem.Name,
                Price = m.FoodItem.Price,
            }).ToList();

            if (foodItems.Count > 0)
            {
                dto.MenuId = menu.MenuId;
                dto.Name = menu.Name;
                dto.FoodItems = foodItems;
                return dto;
            }

            return null;
        }
    }

    public class MenuFoodItemEditModel
    {
        public int MenuId { get; set; }
        public List<int> FoodItems { get; set; }
    }

}