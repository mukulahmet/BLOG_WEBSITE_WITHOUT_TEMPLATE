using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Entities;
using WebApplication1.VMs;

namespace WebApplication1.Controllers
{
    public class ArticleController : Controller
    {

        private readonly BlogDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public ArticleController(BlogDBContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            //var blogs = _context.Articles.ToList(); // Veritabanından blogları çekiyoruz
            //    var blogs = _context.Articles
            //.Include(a => a.ArticleCategories)
            //    .ThenInclude(ac => ac.Category)
            //.ToList();
            var blogs = _context.Articles
            .Include(a => a.ArticleCategories)
                .ThenInclude(ac => ac.Category)
            .Include(a => a.User) // Yazar bilgilerini dahil ediyoruz
            .ToList();
            return View(blogs);
        }

       
        public IActionResult AddArticle()
        {
            var categories = _context.Categories.ToList();
            var viewModel = new CreateArticleVm
            {
                Categories = categories
            };
            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddArticle(CreateArticleVm model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return Challenge();
                }

                var article = new Article
                {
                    Title = model.Title,
                    Content = model.Content,
                    CreatedDate = model.CreatedDate,
                    UserId = user.Id,
                };

                _context.Articles.Add(article);
                await _context.SaveChangesAsync();

                foreach (var categoryId in model.SelectedCategories)
                {
                    var articleCategory = new ArticleCategory
                    {
                        ArticleId = article.ArticleId,
                        CategoryId = categoryId
                    };
                    _context.ArticleCategories.Add(articleCategory);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            model.Categories = _context.Categories.ToList();
            return View(model);




            ////int uyeID = int.Parse(_userManager.GetUserId(User));
            //AppUser user = await _userManager.GetUserAsync(HttpContext.User);


            //model.CreatedDate = DateTime.Now;
            //model.UserId = user.Id;

            //if (ModelState.IsValid)
            //{
            //    _context.Articles.Add(model);
            //    _context.SaveChanges();
            //    return RedirectToAction("Index"); // Blog listeleme sayfasına yönlendirin
            //}

            //return View(model); // Hata durumunda formu tekrar göster
        }

       
    }
}
