using System.Net.Mail;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using WebApplication1.Models.Entities;
using WebApplication1.Validations;
using WebApplication1.VMs;
using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace WebApplication1.Controllers
{
	public class UserController : Controller
	{
		private readonly UserManager<AppUser> userManager;
		private readonly SignInManager<AppUser> signInManager;
		private readonly BlogDBContext _context;
        private readonly IEmailSender _emailSender;

        public UserController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, BlogDBContext context, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
            _emailSender = emailSender;
        }

        public IActionResult Index()
		{
			return View();
		}

		public IActionResult Login()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginAppUserVM loginVM)
		{
			LoginVMValidator validator = new LoginVMValidator();
			var valid = validator.Validate(loginVM);
			if (valid.IsValid)
			{
				AppUser appUser = await userManager.FindByNameAsync(loginVM.Email);
				if (appUser == null)
				{
					ViewBag.Uyarı = "Bu isimde kayıtlı kullanıcı bulunmamaktadır.";
					return View();
				}
				else if (appUser.EmailConfirmed == false)
				{
					TempData["Mail"] = appUser.Email;
					return RedirectToAction("Index", "Home");
				}
				else
				{
					await signInManager.SignOutAsync();

					Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(appUser, loginVM.Password, true, false);

					if (signInResult.Succeeded)
					{
						return RedirectToAction("Index", "Home");

					}
					ViewBag.Uyarı = "Kullanıcı Adı veya Şifre Hatalı!";
					return View();
				}
			}
			foreach (var item in valid.Errors)
			{
				ModelState.AddModelError("LoginHata", item.ErrorMessage);
			}
			return View();
		}

		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Register(RegisterVM model)
		{
            //DB'de kullanıcı kontrolü
            var usernames = _context.Users.Select(x => x.Email).ToList();
            if (usernames.Contains(model.Email))
            {
                TempData["Error"] = " This email has been used by another user.";
                return RedirectToAction("Register", "User");
            }
            if (!ModelState.IsValid)
            {
                return View(model);
            }


            //User Confirmation mail
            if (ModelState.IsValid)
            {
                var user = new AppUser { UserName = model.Email, Email = model.Email, FirstName = model.FirstName, LastName = model.LastName };
                user.PasswordHash = userManager.PasswordHasher.HashPassword(user, model.Password);
                var result = await userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {

                    await userManager.AddToRoleAsync(user, "Uye");
                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Action(nameof(ConfirmEmail), "User", new { userId = user.Id, code }, protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(model.Email, "Emailinizi doğrulayın",
                        $"Lütfen emailinizi doğrulamak için <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>buraya tıklayın</a>.");

                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View();
  
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return RedirectToAction("Register", "User");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Kullanıcı ID '{userId}' bulunamadı.");
            }
            var result = await userManager.ConfirmEmailAsync(user, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }



        public async Task<IActionResult> Logout()
		{
			await signInManager.SignOutAsync();

			return RedirectToAction("Index", "Home");
		}


        public async Task<IActionResult> Details()
        {
            var user = await userManager.FindByIdAsync(GetUserID().ToString());
            if (user == null)
            {
                return NotFound();
            }

            var articles = _context.Articles
                .Where(a => a.UserId == user.Id)
                .OrderByDescending(a => a.CreatedDate)
                .ToList();

            var viewModel = new AuthorDetailsVm
            {
                User = user,
                Articles = articles
            };

            return View(viewModel);
        }

        public int GetUserID()
        {
            return int.Parse(userManager.GetUserId(User));
        }

        public IActionResult Profile()
        {

            var author = _context.Users
                .Include(u => u.Articles)
                .FirstOrDefault(u => u.Id == GetUserID());

            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        public IActionResult Edit()
        {
            var author = _context.Users
                .FirstOrDefault(u => u.Id == GetUserID());
            return View(author);
        }

        [HttpPost]  
        public IActionResult Edit(UserVM user)
        {
            //AppUser editUser = new AppUser()
            //{
            //    FirstName = user.FirstName,
            //    LastName = user.LastName,
            //    Email = user.Email,
            //    About = user.About,
            //    ProfilePicture = user.ProfilePicture
            //};
            //_context.Update(editUser);
            //_context.SaveChanges();
            //return RedirectToAction("Profile");

            var editUser = _context.Users.FirstOrDefault(u => u.Id == user.Id); // Güncellenecek kullanıcıyı al
            if (editUser != null)
            {
                // Yalnızca güncellenen alanları atayın
                editUser.FirstName = user.FirstName;
                editUser.LastName = user.LastName;
                editUser.About = user.About;
                //editUser.ProfilePicture = user.ProfilePicture;

                if (user.ProfilePicture != null && IsImage(user.ProfilePicture.ContentType))
                {
                    //string dosyaAdi = "";
                    //dosyaAdi += user.ProfilePicture.FileName;
                    //string dosyaYolu = "wwwroot/img/";
                    //editUser.ProfilePicture = "~/img/" + user.ProfilePicture.FileName;

                    //FileStream fs = new FileStream(dosyaYolu + dosyaAdi, FileMode.Create);
                    //user.ProfilePicture.CopyTo(fs);
                    //fs.Close();

                    Guid guid = Guid.NewGuid();
                    string dosyaAdi = guid.ToString();
                    dosyaAdi += user.ProfilePicture.FileName;
                    string dosyaYolu = "wwwroot/img/";
                    editUser.ProfilePicture = dosyaAdi;

                    FileStream fs = new FileStream(dosyaYolu + dosyaAdi, FileMode.Create);
                    user.ProfilePicture.CopyTo(fs);
                    fs.Close();
                }

                

                _context.Update(editUser); // Güncellenen kullanıcıyı güncelle
                _context.SaveChanges(); // Değişiklikleri kaydet
            }

            return RedirectToAction("Profile");

        }

        private bool IsImage(string contentType)
        {
            string[] allowedContentTypes = { "image/jpeg", "image/png", "image/gif" };
            return allowedContentTypes.Contains(contentType);
        }
    }
}

