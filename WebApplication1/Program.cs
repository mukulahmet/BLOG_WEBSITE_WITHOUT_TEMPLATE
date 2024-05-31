using Microsoft.AspNetCore.Identity;
using System.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Entities;
using WebApplication1.Servies;
using Microsoft.AspNetCore.Identity.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BlogDBContext>(
	option => option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddIdentity<AppUser, AppRole>(x => x.SignIn.RequireConfirmedAccount = false)
                .AddEntityFrameworkStores<BlogDBContext>()
                .AddRoles<AppRole>()
                .AddDefaultTokenProviders();

builder.Services.AddTransient<IEmailSender, EmailSender>();

//MVC Hizmetleri
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


//builder.Services.AddRazorPages();

//builder.Services.AddIdentity<AppUser, AppRole>(x=>x.SignIn.RequireConfirmedEmail=true).AddEntityFrameworkStores<BlogDBContext>().AddRoles<AppRole>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllerRoute(
	  name: "areas",
	  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
	);
});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
