using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using BookEvent.Models;
using BookEvent.DataAccessLayer;

namespace MVCBookEvent.Controllers
{
    public class CommentsController : Controller
    {
        private readonly BookEventDBContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CommentsController(BookEventDBContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment([FromForm] int eventId, string text)
        {
            if (!ModelState.IsValid)
            {
                // Handle validation errors
                return BadRequest();
            }

            var user = await _userManager.GetUserAsync(HttpContext.User);

            var comment = new Comment
            {
                Text = text,
                Name = user.UserName,
                DatePosted = DateTime.UtcNow,
                EventId = eventId,
                ApplicationUserId = user.Id
            };
            _context.Comments.Add(comment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Details", "Event", new { id = eventId });
        }
    }
}
