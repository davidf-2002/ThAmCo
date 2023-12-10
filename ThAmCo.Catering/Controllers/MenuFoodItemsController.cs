using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;
using ThAmCo.Catering.DTO;

namespace ThAmCo.Catering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuFoodItemsController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public MenuFoodItemsController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: api/MenuFoodItems
        /// <summary>
        /// Get list of Menus along with corresponding Food items
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<MenuFoodItemDTO>>> GetMenuFoodItems()
        {
            try
            {
                var menuFoodItems = await _context.MenuFoodItem     // Include related data (Menu and FoodItem) in the query
                    .Include(mfi => mfi.Menu)
                    .Include(mfi => mfi.FoodItem)
                    .ToListAsync();

                if (menuFoodItems == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent);
                }

                var groupedMenuFoodItems = menuFoodItems.GroupBy(mfi => mfi.Menu);      // Group by menu to avoid repitition

                var menuFoodItemDtos = groupedMenuFoodItems
                    .Select(group => MenuFoodItemDTO.BuildDTO(group.Key))
                    .ToList();

                return Ok(menuFoodItemDtos);
            }

            catch (Exception ex)        // Log the exception or handle it appropriately
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving menu food items");
            }
        }

        // PUT: api/MenuFoodItems
        /// <summary>
        /// Edit Food items for specific menu
        /// </summary>
        [HttpPut("{menuId}")]
        public async Task<ActionResult> UpdateMenuFoodItems(int menuId, MenuFoodItemEditModel EditModel)
        {
            if (!ModelState.IsValid || menuId != EditModel.MenuId)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            try
            {
                var menu = await _context.Menu.FindAsync(menuId);
                if (menu == null)
                {
                    return StatusCode(StatusCodes.Status404NotFound);
                }
                var newFoodItems = EditModel.FoodItems.ToList();
                if (newFoodItems.Count == 0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest);
                }
                var currentFoodItems = _context.MenuFoodItem.Where(m => m.MenuId == menu.MenuId).ToList();      // So what are this Menus current FoodItems?

                if (currentFoodItems.Count() > 0)       // Deletes items in the input list
                {
                    var itemsToDelete = currentFoodItems.Where(item => !newFoodItems.Any(i => i == item.FoodItemId)).ToList();
                    if (itemsToDelete.Any())
                    {
                        _context.MenuFoodItem.RemoveRange(itemsToDelete);
                    }
                }

                foreach (var newItem in newFoodItems)       // Add items IN the input list
                {
                    var exists = currentFoodItems.Any(e => e.FoodItemId == newItem && e.MenuId == menu.MenuId);

                    if (!exists)
                    {
                        _context.MenuFoodItem.Add(new MenuFoodItem(newItem, menu.MenuId));
                    }
                }
                await _context.SaveChangesAsync(); // Save changes to the database
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating menu food items");
            }

            return Ok();
        }
    }
}




