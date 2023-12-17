using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Dtos;

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
        public async Task<IActionResult> Create()
        {

            var evList = await GetEventTypes();
            ViewData["Id"] = new SelectList(evList,
                                         "Id", "Title");        // Prepare the select list
            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Name,DateAndTime,MenuId,Id")] Event @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var evList = await GetEventTypes();        // select list
            ViewData["Id"] = new SelectList(evList,
                                         "Id", "Title");
            return View(@event);
        }

        // GET: Event/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Events == null)
            {
                return NotFound();
            }

            var catList = await GetEventTypes();        // prepare select list

            ViewData["Id"] = new SelectList(catList,
                                         "Id", "Title");

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
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Name,Id")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            var catList = await GetEventTypes();        // select list

            ViewData["Id"] = new SelectList(catList,
                                         "Id", "Title");

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


        private async Task<List<EventTypeDTO>> GetEventTypes()      // Call web service and get a list of categories
        {
            var eventtypes = new List<EventTypeDTO>().AsEnumerable();

            HttpClient client = new HttpClient();       // Create and initial Http Client
            client.BaseAddress = new System.Uri("https://localhost:7088/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

            HttpResponseMessage response = await client.GetAsync("api/eventtypes");     // Call web service
            if (response.IsSuccessStatusCode)
            {
                eventtypes = await response.Content.ReadAsAsync<IEnumerable<EventTypeDTO>>();       // Decode response into a DTO
            }
            else
            {
                throw new ApplicationException("Something went wrong calling the API:" +
                             response.ReasonPhrase);
            }
            return eventtypes.ToList();
        }


        private bool EventExists(int id)
        {
          return _context.Events.Any(e => e.EventId == id);
        }
    }
}
