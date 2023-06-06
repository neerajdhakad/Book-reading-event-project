using System.ComponentModel.DataAnnotations;

namespace BookEvent.Models
{
    public class Comment
    {
        [Key]
        public int CommentId { get; set; }

        [Required(ErrorMessage = "Please enter a comment.")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Please enter a name.")]
        public string Name { get; set; }

        [Required]
        public DateTime DatePosted { get; set; }

        public int EventId { get; set; }

        public Event Event { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
    }

}
