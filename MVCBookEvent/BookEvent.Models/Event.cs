using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookEvent.Models
{

    public class Event
    {
        [Key]
        public int EventId { get; set; }

        [Required(ErrorMessage = "Please Enter Title")]
        public string? EventName { get; set; }

        [StringLength(50, ErrorMessage = "Event description must be at most 50 characters")]
        public string? EventDescription { get; set; }

        [StringLength(500, ErrorMessage = "Event description must be at most 500 characters")]
        public string? EventOtherDetails { get; set; }

        [Required(ErrorMessage = "Please Enter Location")]
        public string? EventLocation { get; set; }

        [Range(0, 4, ErrorMessage = "Duration should be less than or equal to 4")]
        public int EventDuration { get; set; }

        public string? EventType { get; set; }

        /*[Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]*/
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Please Enter Event Start Time")]
        [DataType(DataType.Time)]
        public string? EventStartTime { get; set; }

        public string? ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

        public ICollection<Invitation> Invitations { get; set; }

        public ICollection<Comment> Comments { get; set; }

    }
}
