using System.ComponentModel.DataAnnotations;

namespace SocialHub.Models
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Вкажіть автора")]
        public string Author { get; set; } = "";

        [Required(ErrorMessage = "Вкажіть заголовок")]
        public string Title { get; set; } = "";

        [Required(ErrorMessage = "Напишіть текст")]
        public string Text { get; set; } = "";

        public DateTime CreatedAt { get; set; }

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}