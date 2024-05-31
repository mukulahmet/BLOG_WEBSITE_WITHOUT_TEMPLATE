using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Models.Entities;
using WebApplication1.VMs;

namespace WebApplication1.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
        private readonly BlogDBContext _context;
        private readonly UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, BlogDBContext context, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                // Ziyaretçi ana sayfasý için verileri hazýrlayýn
                var viewModel = new VisitorHomePageVm
                {
                    MostReadArticles = _context.Articles
                        .OrderByDescending(a => a.ReadCount)
                        .Take(5)
                        .ToList(),
                    GeneralArticles = _context.Articles
                        .OrderByDescending(a => a.CreatedDate)
                        .Take(10)
                        .ToList()
                };
                return View("VisitorHomePage", viewModel);
            }
            else
            {
                var viewModel = new VisitorHomePageVm
                {
                    MostReadArticles = _context.Articles
                       .OrderByDescending(a => a.ReadCount)
                       .Take(5)
                       .ToList(),
                    GeneralArticles = _context.Articles
                       .OrderByDescending(a => a.CreatedDate)
                       .Take(10)
                       .ToList()
                };
                return View("MemberHomePage", viewModel);
            }
        }

        public IActionResult About()
        {
            // Hakkýmýzda sayfasý
            return View();
        }

        public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
