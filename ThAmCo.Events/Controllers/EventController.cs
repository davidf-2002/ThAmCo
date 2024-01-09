using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThAmCo.Events.Data;
using ThAmCo.Events.Dtos;
using ThAmCo.Events.Models;
using System.Security.Cryptography.Xml;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace ThAmCo.Events.Controllers
{
    public class EventController : Controller
    {
        private readonly EventsContext _context;

        public EventController(EventsContext context)
        {
            _context = context;
        }

        //// GET: Event
        //public async Task<IActionResult> Index()
        //{
        //    var eventsWithGuests = await _context.Events.Include(e => e.Guests).ToListAsync();      // display a list of events with context on guests
        //    return View(eventsWithGuests);
        //}

        // GET: Event
        public async Task<IActionResult> Index()
        {
            var model = await _context.Events
                .Include(e => e.Guests)
                .Include(e => e.Staffs) // Include the Staffs property directly on the Event class
                .ToListAsync();

            var eventViewModels = model.Select(e => new EventVM
            {
                EventId = e.EventId,
                Name = e.Name,
                DateAndTime = e.DateAndTime,
                EventTypeId = e.EventTypeId,
                MenuId = e.MenuId,
                TotalGuestsCount = e.TotalGuestsCount,
                isFirstAider = e.IsFirstAider
            });

            return View(eventViewModels);
        }


        // GET: Event/Create
        public async Task<IActionResult> Create()
        {

            var evList = await GetEventTypes();
            ViewData["Id"] = new SelectList(evList,
                                         "Id", "Title");        // Prepare the select list

            //var availablList = await GetAvailableVenues();


            return View();
        }

        // POST: Event/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Name,DateAndTime,MenuId, EventTypeId")] EventVM eventViewModel)
        {
          
            if (ModelState.IsValid)
            {
                Event @event = new Event
                {
                    EventId = eventViewModel.EventId,
                    Name = eventViewModel.Name,
                    DateAndTime = eventViewModel.DateAndTime,
                    MenuId = eventViewModel.MenuId,
                    EventTypeId = eventViewModel.EventTypeId
                };
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var evList = await GetEventTypes();        // select list
            ViewData["Id"] = new SelectList(evList,
                                         "Id", "Title");
            return View(eventViewModel);
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
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Name,Id")] Event @event)
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

        // GET: Event/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Guests)
                .FirstOrDefaultAsync(e => e.EventId == id);

            

            if (@event == null)
            {
                return NotFound();
            }
            return View(@event);
        }

        public async Task<IActionResult> Reserve(int id)
        {
            var @event = await _context.Events
               .FirstOrDefaultAsync(e => e.EventId == id);

            var availablList = await GetAvailableVenues(@event.EventTypeId, @event.DateAndTime);
            ViewData["Id"] = new SelectList(availablList,
                                         "Name", "Name");        // Prepare the select list

            


            return View();
        }

        private async Task<List<VenueDTO>> GetAvailableVenues(string eventType, DateTime date)      // Call web service and get a list of categories
        {
            var venues = new List<VenueDTO>().AsEnumerable();

            HttpClient client = new HttpClient();       // Create and initial Http Client
            client.BaseAddress = new System.Uri("https://localhost:7088/");
            client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

            HttpResponseMessage response = await client.GetAsync($"api/Availability?eventType={eventType}&beginDate={date.Date:yyyy-MM-dd}&endDate={date.Date:yyyy-MM-dd}");

            if (response.IsSuccessStatusCode)
            {
                venues = await response.Content.ReadAsAsync<IEnumerable<VenueDTO>>();       // Decode response into a DTO
            }
            else
            {
                throw new ApplicationException("Something went wrong calling the API:" +
                             response.ReasonPhrase);
            }
            return venues.ToList();
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



        //private async Task<List<ReservationDTO>> PostReservation()
        //{
        //    var reservationList = new List<ReservationDTO>().AsEnumerable(); 

        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new System.Uri("https://localhost:7088");
        //    client.DefaultRequestHeaders.Accept.ParseAdd("application/json");

        //    var reservation = new ReservationDTO();
        //    {
        //        Reference = Reference,
        //        DateTime = 

        //    };
        //    HttpResponseMessage response = await client.PostAsJsonAsync("api/reservation", reservation);

        //    if (response.IsSuccessStatusCode)
        //    {
        //        eventtypes = await response.Content.ReadAsAsync<IEnumerable<EventTypeDTO>>();       // Decode response into a DTO
        //    }
        //    else
        //    {
        //        throw new ApplicationException("Something went wrong calling the API:" +
        //                     response.ReasonPhrase);
        //    }
        //    return eventtypes.ToList();

        //}


        private bool EventExists(int id)
        {
          return _context.Events.Any(e => e.EventId == id);
        }
    }
}
