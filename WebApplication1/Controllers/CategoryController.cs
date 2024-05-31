using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;

namespace WebApplication1.Controllers
{
    public class CategoryController : Controller
    {
        private readonly BlogDBContext _context;

        public CategoryController(BlogDBContext blogDBContext)
        {
            _context = blogDBContext;
        }

        public IActionResult Index()
        {
            // Tüm kategorileri ve ilgili makaleleri çekiyoruz
            var categories = _context.Categories
                .Include(c => c.ArticleCategories)
                    .ThenInclude(ac => ac.Article)
                        .ThenInclude(a => a.User)
                        .Where(topic => topic.ArticleCategories.Any(ta => ta.Article != null))
                .ToList();

            return View(categories);
        }





    }
}
