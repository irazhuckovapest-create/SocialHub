using Microsoft.AspNetCore.Mvc;
using SocialHub.Data;
using SocialHub.Models;

namespace SocialHub.Controllers
{
    public class CommentsController : Controller
    {
        private readonly AppDbContext _context;

        public CommentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult Add(int postId, string author, string text)
        {
            if (!string.IsNullOrWhiteSpace(author) && !string.IsNullOrWhiteSpace(text))
            {
                var comment = new Comment();
                comment.PostId = postId;
                comment.Author = author;
                comment.Text = text;
                comment.CreatedAt = DateTime.Now;

                _context.Comments.Add(comment);
                _context.SaveChanges();
            }

            return RedirectToAction("Details", "Posts", new { id = postId });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var comment = _context.Comments.Find(id);

            if (comment == null)
            {
                return NotFound();
            }

            int postId = comment.PostId;

            _context.Comments.Remove(comment);
            _context.SaveChanges();

            return RedirectToAction("Details", "Posts", new { id = postId });
        }
    }
}