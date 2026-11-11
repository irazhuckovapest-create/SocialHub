using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialHub.Data;
using SocialHub.Models;

namespace SocialHub.Controllers
{
    public class PostsController : Controller
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string? author)
        {
            var posts = _context.Posts.AsQueryable();

            if (!string.IsNullOrEmpty(author))
            {
                posts = posts.Where(p => p.Author.Contains(author));
            }

            ViewBag.Author = author;

            return View(posts.OrderByDescending(p => p.CreatedAt).ToList());
        }

        public IActionResult Details(int id)
        {
            var post = _context.Posts
                .Include(p => p.Comments)
                .FirstOrDefault(p => p.Id == id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            post.CreatedAt = DateTime.Now;
            _context.Posts.Add(post);
            _context.SaveChanges();

            return RedirectToAction("Details", new { id = post.Id });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var post = _context.Posts.Find(id);

            if (post == null)
            {
                return NotFound();
            }

            return View(post);
        }

        [HttpPost]
        public IActionResult Edit(Post post)
        {
            if (!ModelState.IsValid)
            {
                return View(post);
            }

            var postFromDb = _context.Posts.Find(post.Id);

            if (postFromDb == null)
            {
                return NotFound();
            }

            postFromDb.Author = post.Author;
            postFromDb.Title = post.Title;
            postFromDb.Text = post.Text;

            _context.SaveChanges();

            return RedirectToAction("Details", new { id = postFromDb.Id });
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var post = _context.Posts.Find(id);

            if (post != null)
            {
                _context.Posts.Remove(post);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}