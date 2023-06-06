
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookEvent.Models
{
    public enum Type
    {
        Public,
        Private
    }

    public class CreateEventViewModel
    {
        [Required(ErrorMessage = "Please Enter Title")]
        [DisplayName("Name")]
        public string? EventName { get; set; }

        [StringLength(50, ErrorMessage = "Event description must be at most 50 characters")]
        [DisplayName("Description")]
        public string? EventDescription { get; set; }

        [StringLength(500, ErrorMessage = "Event description must be at most 500 characters")]
        [DisplayName("Other Details")]
        public string? EventOtherDetails { get; set; }

        [Required(ErrorMessage = "Please Enter Location")]
        [DisplayName("Location")]
        public string? EventLocation { get; set; }


        [Range(0, 4, ErrorMessage = "Duration should be less than or equal to 4 hours")]
        [DisplayName("Duration")]
        public int EventDuration { get; set; }

        [DisplayName("Type")]
        public string? EventType { get; set; }

        [DisplayName("Date")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Please Enter Event Start Time")]
        [DisplayName("Start Time")]
        public string EventStartTime { get; set; }

        public string? Invitations { get; set; }
    }

}
