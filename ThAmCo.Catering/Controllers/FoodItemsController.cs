using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;
using ThAmCo.Catering.DTO;

namespace ThAmCo.Catering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodItemsController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public FoodItemsController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: api/FoodItems
        /// <summary>
        /// Get list of Fooditems
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<FoodItemDTO>>> GetFoods()
        {
            if (_context.FoodItem == null)      // sees if it can communicate with FoodItem first
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var foodItems = await _context.FoodItem.ToListAsync();       // putting instances of FoodItem in a list Asynchronously
            var foodItemsDto = foodItems.Select(s => new FoodItemDTO
            {
                FoodItemId = s.FoodItemId,
                Name = s.Name,
                Price = s.Price,
            }).ToList();

            return Ok(foodItemsDto);
        }


        // GET: api/FoodItems/5
        /// <summary>
        /// Get specific Fooditem
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<FoodItemDTO>> GetFoodItem(int id)
        {
            if (_context.FoodItem == null)      
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var foodItem = await _context.FoodItem.FindAsync(id);
            var foodItemsDTO = new FoodItemDTO
            {
                FoodItemId = foodItem.FoodItemId,
                Name = foodItem.Name,
                Price = foodItem.Price,
            };

            if (foodItem == null)       // if the Food item doesnt exist return 404 error
            {
                return NotFound();
            }
            return Ok(foodItemsDTO);
        }

        // PUT: api/FoodItems/5
        /// <summary>
        /// Edit an existing Fooditem
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFoodItem(int id, FoodItemDTO foodItemDTO)
        {
            var foodItem = new FoodItem(foodItemDTO.FoodItemId, foodItemDTO.Name, foodItemDTO.Price);
            if (id != foodItem.FoodItemId)
            {
                return BadRequest();
            }

            _context.Entry(foodItem).State = EntityState.Modified;      // marks an entity as 'modified'

            try     // this exception is by EF if an error occurs whilst saving to DB
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FoodItemExists(id))        // checks to see if the item already exists
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/FoodItems
        /// <summary>
        /// Create a new Fooditem
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<FoodItemDTO>> PostFoodItem(FoodItemDTO foodItemDTO)
        {
            if (_context.FoodItem == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }

            var foodItem = new FoodItem(foodItemDTO.FoodItemId, foodItemDTO.Name, foodItemDTO.Price);
            var foodItemsDTO = new FoodItemDTO
            {
                FoodItemId = foodItem.FoodItemId,
                Name = foodItem.Name,
                Price = foodItem.Price,
            };

            try
            {
                _context.FoodItem.Add(foodItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("PostFoodItem", new { id = foodItem.FoodItemId }, foodItem);
        }


        // DELETE: api/FoodItems/5
        /// <summary>
        /// Remove specific Fooditem
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<FoodItem>> DeleteFoodItem(int id)
        {
            if (_context.FoodItem == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var foodItem = await _context.FoodItem.FindAsync(id);

            if (foodItem == null)
            {
                return NotFound();
            }

            try
            {
                _context.FoodItem.Remove(foodItem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException) { 
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

        private bool FoodItemExists(int id)
        {
            return _context.FoodItem.Any(e => e.FoodItemId == id);
        }
    }
}
