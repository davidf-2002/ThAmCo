using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;

namespace ThAmCo.Events.Controllers
{
    public class EventController : Controller
    {
        private readonly EventsContext _context;

        public EventController(EventsContext context)
        {
            _context = context;
        }

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var eventsWithGuests = await _context.Events.Include(e => e.Guests).ToListAsync();      // display a list of events with context on guests
            return View(eventsWithGuests);
        }

        // GET: Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Name,DateAndTime,BookingId,Reference")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }




        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        // POST: Event/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Name,DateAndTime,BookingId,Reference")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }



























        //// GET: Event/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null || _context.Events == null)
        //    {
        //        return NotFound();
        //    }

        //    var @event = await _context.Events
        //        .FirstOrDefaultAsync(m => m.EventId == id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@event);
        //}

        //// GET: Event/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Events == null)
        //    {
        //        return NotFound();
        //    }

        //    var @event = await _context.Events
        //        .FirstOrDefaultAsync(m => m.EventId == id);
        //    if (@event == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(@event);
        //}

        //// POST: Event/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Events == null)
        //    {
        //        return Problem("Entity set 'EventsContext.Events'  is null.");
        //    }
        //    var @event = await _context.Events.FindAsync(id);
        //    if (@event != null)
        //    {
        //        _context.Events.Remove(@event);
        //    }
            
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool EventExists(int id)
        {
          return _context.Events.Any(e => e.EventId == id);
        }
    }
}
