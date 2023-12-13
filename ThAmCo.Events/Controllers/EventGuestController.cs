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

        // GET: EventGuest/1
        public async Task<IActionResult> Index(int? id = null)
        {
            var eventsContext = _context.EventGuest.AsQueryable();      // turns DBset into searchable list type

            if (id != null)
            {
                eventsContext = eventsContext.Where(ei => ei.EventId == id);
            }

            eventsContext = eventsContext
                .Include(e => e.Guest)
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


    }
}
