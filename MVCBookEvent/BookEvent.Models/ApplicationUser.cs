using Microsoft.AspNetCore.Identity;

namespace BookEvent.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FullName { get; set; }

        // navigation property for invitations
        public ICollection<Invitation> Invitations { get; set; }

    }
}
