using System.ComponentModel.DataAnnotations;

namespace BookEvent.Models
{
    public class CommentViewModel
    {
        public int EventId { get; set; }

        [Required(ErrorMessage = "Please enter a comment.")]
        public string? CommentText { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string? Name { get; set; }
    }

}
