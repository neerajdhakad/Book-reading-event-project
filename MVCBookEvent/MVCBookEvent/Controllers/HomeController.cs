using BookEvent.DataAccessLayer;
using BookEvent.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MVCBookEvent.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookEventDBContext _context;
        public HomeController(ILogger<HomeController> logger, BookEventDBContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var events = _context.Events.Where(e => e.EventType == "Public").ToList();

            // Retrieve past events and future events separately
            var pastEvents = events.Where(e => e.EventDate < DateTime.Now).ToList();
            var futureEvents = events.Where(e => e.EventDate >= DateTime.Now).ToList();

            // Pass the past and future events to the view
            ViewData["PastEvents"] = pastEvents;
            ViewData["FutureEvents"] = futureEvents;
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}