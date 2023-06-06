using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Identity;
using BookEvent.Models;
using Microsoft.EntityFrameworkCore;

using Microsoft.Extensions.Logging;
using System.Security.Claims;
using BookEvent.DataAccessLayer;

namespace MVCBookEvent.Controllers
{
    public class EventController : Controller
    {
        private readonly ILogger<EventController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly BookEventDBContext _context;

        public EventController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, ILogger<EventController> logger, BookEventDBContext context)
        {
            _userManager = userManager;
            _logger = logger;
            _context = context;
            _signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateEventViewModel model)
        {
            if (ModelState.IsValid)
            {
                var eventModel = new Event
                {
                    EventName = model.EventName,
                    EventDescription = model.EventDescription,
                    EventOtherDetails = model.EventOtherDetails,
                    EventLocation = model.EventLocation,
                    EventDuration = model.EventDuration,
                    EventType = model.EventType ?? "Public",
                    EventDate = model.EventDate,
                    EventStartTime = model.EventStartTime,
                };

                var user = await _userManager.GetUserAsync(HttpContext.User);
                eventModel.ApplicationUserId = user.Id;
                eventModel.ApplicationUser = user;

                _context.Events.Add(eventModel);
                await _context.SaveChangesAsync();

                if (!string.IsNullOrWhiteSpace(model.Invitations))
                {
                    var invitationEmails = model.Invitations.Split(',');
                    var invitations = new List<Invitation>();
                    foreach (var email in invitationEmails)
                    {
                        var userToInvite = await _userManager.FindByEmailAsync(email.Trim());
                        if (userToInvite != null)
                        {
                            var invitation = new Invitation
                            {
                                ApplicationUserId = userToInvite.Id,
                                EventId = eventModel.EventId,
                                InvitationActive = 1
                            };
                            invitations.Add(invitation);
                        }
                    }
                    if (invitations.Count > 0)
                    {
                        _context.Invitations.AddRange(invitations);
                        await _context.SaveChangesAsync();
                    }
                }
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        public IActionResult Details(int id)
        {
            var selectedEvent = _context.Events.Include(e => e.Comments).FirstOrDefault(e => e.EventId == id);
            if (selectedEvent == null)
            {
                return NotFound();
            }

            var commentViewModel = new CommentViewModel
            {
                EventId = selectedEvent.EventId
            };

            ViewBag.CommentViewModel = commentViewModel;

            return View(selectedEvent);
        }


        public IActionResult MyEvents()
        {
            // Get the current user's ID
            string currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Retrieve all events created by the current user
            List<Event> myEvents = _context.Events.Where(e => e.ApplicationUserId == currentUserId)
                                                   .OrderByDescending(e => e.EventDate)
                                                   .ToList();

            // Pass the events to the view
            return View(myEvents);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Event eventToEdit = _context.Events.Find(id);
            if (eventToEdit == null)
            {
                return NotFound();
            }
            return View(eventToEdit);
        }

        [HttpPost]
        public IActionResult Edit(Event eventModel)
        {
            Event eventToEdit = _context.Events.Find(eventModel.EventId);
            if (eventToEdit == null)
            {
                return NotFound();
            }

            eventToEdit.EventName = eventModel.EventName;
            eventToEdit.EventDescription = eventModel.EventDescription;
            eventToEdit.EventOtherDetails = eventModel.EventOtherDetails;
            eventToEdit.EventLocation = eventModel.EventLocation;
            eventToEdit.EventDuration = eventModel.EventDuration;
            eventToEdit.EventType = eventModel.EventType;
            eventToEdit.EventDate = eventModel.EventDate;
            eventToEdit.EventStartTime = eventModel.EventStartTime;

            _context.SaveChanges();

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> EventInvites()
        {
            // Get the current user
            var currentUser = await _userManager.GetUserAsync(HttpContext.User);

            // Get the events where the current user was invited
            var events = await _context.Events
                .Join(_context.Invitations, e => e.EventId, i => i.EventId, (e, i) => new { Event = e, Invitation = i })
                .Where(x => x.Invitation.ApplicationUserId == currentUser.Id && x.Invitation.InvitationActive == 1)
                .Select(x => x.Event)
                .ToListAsync();

            return View(events);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AllEvents()
        {
            // Retrieve all events
            List<Event> allEvents = _context.Events.Include(e => e.ApplicationUser)
                                                    .OrderByDescending(e => e.EventDate)
                                                    .ToList();

            // Pass the events to the view
            return View(allEvents);
        }

    }
}