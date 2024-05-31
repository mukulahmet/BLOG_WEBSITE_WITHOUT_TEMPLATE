using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models.Entities;

namespace WebApplication1.Data
{
	public class BlogDBContext : IdentityDbContext<AppUser, AppRole, int>
	{
		public BlogDBContext(DbContextOptions options) : base(options)
		{
		}

		public BlogDBContext() { }
		
		public DbSet<Article> Articles { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ArticleCategory> ArticleCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
		{
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());



            IdentityUserRole<int> identityUserRole1 = new IdentityUserRole<int>()
			{ RoleId = 1, UserId = 1 };

			IdentityUserRole<int> identityUserRole2 = new IdentityUserRole<int>()
			{ RoleId = 2, UserId = 2 };

			builder.Entity<IdentityUserRole<int>>(x => x.HasData(identityUserRole1, identityUserRole2));
		}



	}
}
