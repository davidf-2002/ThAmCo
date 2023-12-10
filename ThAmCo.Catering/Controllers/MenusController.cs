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
    public class MenusController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public MenusController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        /// <summary>
        /// Get list of Menus
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<List<MenuDTO>>> GetMenus()
        {
            if (_context.FoodItem == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var menus = await _context.Menu.ToListAsync();
            var menusDto = menus.Select(m => new MenuDTO
            {
                MenuId = m.MenuId,
                Name = m.Name,
            }).ToList();
            return Ok(menusDto);
        }

        // GET: api/Menus/5
        /// <summary>
        /// Get specific Menu
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDTO>> GetMenu(int id)
        {
            if (_context.Menu == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var menu = await _context.Menu.FindAsync(id);
            var menuDTO = new MenuDTO
            {
                MenuId = menu.MenuId,
                Name = menu.Name,
            };
            if (menu == null)
            {
                return NotFound();
            }
            return Ok(menuDTO);
        }


        // PUT: api/Menus/5
        /// <summary>
        /// Edit an existing Menu
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(int id, MenuDTO menuDto)
        {
            var menu = new Menu(menuDto.MenuId, menuDto.Name);
            if (id != menu.MenuId)
            {
                return BadRequest();
            }

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
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

        // POST: api/Menus
        /// <summary>
        /// Create a Menu
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<MenuDTO>> PostMenu(MenuDTO menuDto)
        {
            if (_context.Menu == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest);
            }
            var menu = new Menu(menuDto.MenuId, menuDto.Name);
            var menuDTO = new MenuDTO
            {
                MenuId = menu.MenuId,
                Name = menu.Name,
            };

            try
            {
                _context.Menu.Add(menu);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("PostMenu", new { id = menu.MenuId }, menu);
        }

        // DELETE: api/Menus/5
        /// <summary>
        /// Remove specific Menu
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<ActionResult<Menu>> DeleteMenu(int id)
        {
            var menu = await _context.Menu.FindAsync(id);

            if (menu == null)
            {
                return NotFound();
            }

            try
            {
                _context.Menu.Remove(menu);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }


        private bool MenuExists(int id)
        {
            return _context.Menu.Any(e => e.MenuId == id);
        }
    }
}
