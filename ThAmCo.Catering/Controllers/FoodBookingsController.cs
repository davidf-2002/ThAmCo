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

        // PUT: api/FoodBookings/5
        /// <summary>
        /// Edit an existing Food booking
        /// </summary>


        //[HttpPut("{ClientReferenceId}")]
        //public async Task<IActionResult> BookFood(int clientReferenceId, BookingDTO booking)
        //{
        //    if (clientReferenceId != booking.ClientReferenceId || !ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }

        //    var DbBookings = await _context.FoodBooking.FindAsync(clientReferenceId);

        //    if (DbBookings == null)
        //    {
        //        return NotFound();
        //    }

        //    DbBookings.BookingId = GenerateBookingRefrence(7);

        //    _context.Entry(DbBookings).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }

        //    return NoContent();
        //}




        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutFoodBooking(int id, FoodBooking foodBooking)
        //{
        //    if (id != foodBooking.BookingId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(foodBooking).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!FoodBookingExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}
















        //// POST: api/FoodBooking
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<FoodBooking>> PostFoodBooking(FoodBooking foodBooking)
        //{
        //    _context.FoodBooking.Add(foodBooking);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (FoodBookingExists(foodBooking.BookingId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetMenuFoodItem", new { id = foodBooking.BookingId }, foodBooking);
        //}

        //// POST: api/FoodBookings/AddFoodBookingDate
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost("AddFoodBooking")]
        //public async Task<ActionResult<FoodBooking>> AddFoodBooking(FoodBooking foodBooking)
        //{
        //    _context.FoodBooking.Add(foodBooking);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetFoodBooking", new { id = foodBooking.BookingId }, foodBooking);
        //}


        [HttpPost]
        public async Task<ActionResult<FoodBooking>> PostFoodBooking(FoodBooking booking)
        {
            if (booking == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }


            FoodBooking newBooking = new FoodBooking
            {
                ClientReferenceId = booking.ClientReferenceId,
                BookingId = booking.BookingId,
                MenuId = booking.MenuId,
                NumberOfGuests = booking.NumberOfGuests
            };

            try
            {
                _context.FoodBooking.Add(newBooking);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return CreatedAtAction("GetBooking", new { id = newBooking.BookingId }, newBooking);
        }







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

        static string GenerateBookingRefrence(int length)       // Generates a BookingReference of size 'length'
        {
            const string characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(length);
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(characters.Length);
                result.Append(characters[index]);
            }

            return result.ToString();
        }

    }
}
