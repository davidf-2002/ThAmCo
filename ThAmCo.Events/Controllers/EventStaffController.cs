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
    public class EventStaffController : Controller
    {
        private readonly EventsContext _context;

        public EventStaffController(EventsContext context)
        {
            _context = context;
        }

        // GET: EventStaff/Index/1
        public async Task<IActionResult> Index(int? id = null)
        {
            var eventsContext = _context.EventStaff.AsQueryable();      // turns DBset into searchable list type

            if (id != null)
            {
                eventsContext = eventsContext.Where(s => s.EventId == id);
            }

            eventsContext = eventsContext.Include(s => s.Staff)
                .Include(e => e.Event);

            return View(await eventsContext.ToListAsync());
        }

        // GET: EventStaff/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name");       // Creates a Select list to view events and guests
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "FirstName");
            return View();
        }

        // POST: EventStaff/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,StaffId")] EventStaff eventStaff)        // [Bind] attribute is used to include only specific properties during model binding
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventStaff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", eventStaff.EventId);
            ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "FirstName", eventStaff.StaffId);
            return View(eventStaff);
        }

        // GET: EventStaff/Delete?eventId=2&staffId=1
        public async Task<IActionResult> Delete(int? eventId, int? staffId)
        {
            if (eventId == null || staffId == null || _context.EventGuest == null)
            {
                return NotFound();
            }

            var eventStaff = await _context.EventStaff
                .Include(e => e.Staff)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.EventId == eventId && m.StaffId == staffId);
            if (eventStaff == null)
            {
                return NotFound();
            }

            return View(eventStaff);
        }

        // POST: EventStaff/Delete?eventId=2&staffId=1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? eventId, int? staffId)
        {
            if (_context.EventGuest == null)
            {
                return Problem("Entity set 'EventsContext.EventStaff'  is null.");
            }
            var eventStaff = await _context.EventStaff.FindAsync(eventId, staffId);
            if (eventStaff != null)
            {
                _context.EventStaff.Remove(eventStaff);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: EventStaff/Edit?eventId=1&staffId=1
        public async Task<IActionResult> Edit(int? eventId, int? staffId)
        {
            if (eventId == null || staffId == null || _context.EventStaff == null)
            {
                return NotFound();
            }

            var eventStaff = await _context.EventStaff
                .Include(e => e.Staff)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.EventId == eventId && m.StaffId == staffId);

            if (eventStaff == null)
            {
                return NotFound();
            }

            return View(eventStaff);
        }

        // POST: EventStaff/Edit?eventId=1&staffId=1
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int eventId, int staffId, [Bind("EventId,GuestId,HasAttended")] EventStaff eventStaff)
        {
            if (eventId != eventStaff.EventId || staffId != eventStaff.StaffId)
            {
                return NotFound();
            }

            var existingEventStaff = await _context.EventStaff      // Retrieve the existing EventGuest entity from the database
                .Include(e => e.Staff)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.EventId == eventId && m.StaffId == staffId);

            if (existingEventStaff == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingEventStaff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventStaffExists(existingEventStaff.EventId, existingEventStaff.StaffId))
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
            return View(existingEventStaff);
        }

        private bool EventStaffExists(int eventId, int staffId)
        {
            return _context.EventStaff.Any(e => e.EventId == eventId && e.StaffId == staffId);
        }
    }
}
