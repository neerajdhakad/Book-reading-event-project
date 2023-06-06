using System.ComponentModel.DataAnnotations;

namespace BookEvent.Models
{
    public class Invitation
    {
        [Key]
        public int InvitationId { get; set; }
        [Required]
        public int InvitationActive { get; set; }

        // foreign key to ApplicationUser
        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        // foreign key to Event
        public int EventId { get; set; }
        public Event Event { get; set; }
    }
}
