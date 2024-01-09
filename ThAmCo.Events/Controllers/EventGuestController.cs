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
    public class EventGuestController : Controller
    {
        private readonly EventsContext _context;

        public EventGuestController(EventsContext context)
        {
            _context = context;
        }

        // GET: EventGuest/Index/1
        public async Task<IActionResult> Index(int? id = null)
        {
            var eventsContext = _context.EventGuest.AsQueryable();      // turns DBset into searchable list type

            if (id != null)
            {
                eventsContext = eventsContext.Where( g => g.EventId == id);
            }

            eventsContext = eventsContext.Include(g => g.Guest)
                .Include(e => e.Event);

            return View(await eventsContext.ToListAsync());
        }

        // GET: EventGuest/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name");       // Creates a Select list to view events and guests
            ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "FirstName");
            return View();
        }

        // POST: EventGuest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,GuestId")] EventGuest eventGuest)        // [Bind] attribute is used to include only specific properties during model binding
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventGuest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", eventGuest.EventId);
            ViewData["GuestId"] = new SelectList(_context.Guest, "GuestId", "FirstName", eventGuest.GuestId);
            return View(eventGuest);
        }

        // GET: EventGuest/Delete?eventId=2&guestId=1
        public async Task<IActionResult> Delete(int? eventId, int? guestId)
        {
            if (eventId == null || guestId == null || _context.EventGuest == null)
            {
                return NotFound();
            }

            var eventGuest = await _context.EventGuest
                .Include(e => e.Guest)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.EventId == eventId && m.GuestId == guestId);
            if (eventGuest == null)
            {
                return NotFound();
            }

            return View(eventGuest);
        }

        // POST: EventGuest/Delete?eventId=2&guestId=1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? eventId, int? guestId)
        {
            if (_context.EventGuest == null)
            {
                return Problem("Entity set 'EventsContext.EventGuest'  is null.");
            }
            var eventGuest = await _context.EventGuest.FindAsync(eventId, guestId);
            if (eventGuest != null)
            {
                _context.EventGuest.Remove(eventGuest);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: EventGuest/Edit?eventId=1&guestId=1
        public async Task<IActionResult> Edit(int? eventId, int? guestId)
        {
            if (eventId == null || guestId == null || _context.EventGuest == null)
            {
                return NotFound();
            }

            var eventGuest = await _context.EventGuest
                .Include(e => e.Guest)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.EventId == eventId && m.GuestId == guestId);

            if (eventGuest == null)
            {
                return NotFound();
            }

            return View(eventGuest);
        }

        // POST: EventGuest/Edit?eventId=1&guestId=1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int eventId, int guestId, [Bind("EventId,GuestId,HasAttended")] EventGuest eventGuest)
        {
            if (eventId != eventGuest.EventId || guestId != eventGuest.GuestId)
            {
                return NotFound();
            }

            var existingEventGuest = await _context.EventGuest      // Retrieve the existing EventGuest entity from the database
                .Include(e => e.Guest)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.EventId == eventId && m.GuestId == guestId);

            if (existingEventGuest == null)
            {
                return NotFound();
            }

            existingEventGuest.HasAttended = eventGuest.HasAttended;        // Update only the HasAttended property

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingEventGuest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventGuestExists(existingEventGuest.EventId, existingEventGuest.GuestId))
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
            return View(existingEventGuest);
        }

        private bool EventGuestExists(int eventId, int guestId)
        {
            return _context.EventGuest.Any(e => e.EventId == eventId && e.GuestId == guestId);
        }

    }
}
