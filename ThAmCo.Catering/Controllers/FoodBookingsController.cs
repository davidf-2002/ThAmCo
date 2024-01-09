using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Catering.Data;
using ThAmCo.Catering.DTO;
using static System.Collections.Specialized.BitVector32;

namespace ThAmCo.Catering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodBookingsController : ControllerBase
    {
        private readonly CateringDbContext _context;

        public FoodBookingsController(CateringDbContext context)
        {
            _context = context;
        }

        // GET: api/FoodBookings
        /// <summary>
        /// Get all Food bookings as a list of BookingDTO
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookingDTO>>> GetFoodBooking()
        {
            //return await _context.FoodBooking.ToListAsync();
            if (_context.FoodBooking == null)
            {
                return NotFound();
            }
            var foodBookings = await _context.FoodBooking.ToListAsync();

            var dto = foodBookings.Select(b => new BookingDTO
            {
                BookingId = b.BookingId,
                ClientReferenceId = b.ClientReferenceId,
                NumberOfGuests = b.NumberOfGuests,
                MenuId = b.MenuId,
            }).ToList();

            return Ok(dto);
        }

        /// <summary>
        /// Add Food Booking
        /// </summary>
        /// <param name="foodBooking"></param>
        /// <returns></returns>
        // POST: api/FoodBooking

        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<FoodBooking>> PostFoodBooking(BookingDTO foodBooking)
        {
            var foodBookings = new FoodBooking
                (foodBooking.BookingId, foodBooking.ClientReferenceId, foodBooking.NumberOfGuests, foodBooking.MenuId);

            _context.FoodBooking.Add(foodBookings);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FoodBookingExists(foodBookings.BookingId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }


        //PUT: api/FoodBookings/5
        /// <summary>
        /// Edit an existing Food booking
        /// </summary>
        [HttpPut("{foodBooking}")]
        public async Task<IActionResult> BookFood(int foodBooking, BookingDTO booking)
        {
            if (foodBooking != booking.BookingId || !ModelState.IsValid)
            {
                return BadRequest();
            }

            var DbBookings = await _context.FoodBooking.FindAsync(foodBooking);

            if (DbBookings == null)
            {
                return NotFound();
            }

            DbBookings.ClientReferenceId = booking.ClientReferenceId;
            DbBookings.NumberOfGuests = booking.NumberOfGuests;

            _context.Entry(DbBookings).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return NoContent();
        }


        /// <summary>
        /// Delete Food Booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/FoodBookings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFoodBooking(int id)
        {
            var foodBooking = await _context.FoodBooking.FindAsync(id);
            if (foodBooking == null)
            {
                return NotFound();
            }

            _context.FoodBooking.Remove(foodBooking);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FoodBookingExists(int id)
        {
            return _context.FoodBooking.Any(e => e.ClientReferenceId == id);
        }

        static int GenerateBookingRefrence(int length)       // Generates a BookingReference of size 'length'
        {
            Random rnd = new Random(); 
            int number = rnd.Next(1, 13);

            return number;
        }

    }
}
