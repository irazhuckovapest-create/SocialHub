using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialHub.Data;

namespace SocialHub.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;

        public HomeController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var posts = _db.Posts
                .Include(p => p.Comments)
                .OrderByDescending(p => p.CreatedAt)
                .Take(5)
                .ToList();

            return View(posts);
        }
    }
}