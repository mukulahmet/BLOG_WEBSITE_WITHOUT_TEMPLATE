using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.EntityConfiguration
{
	public class User_CFG : IEntityTypeConfiguration<AppUser>
	{
		public void Configure(EntityTypeBuilder<AppUser> builder)
		{
			AppUser uye1 = new AppUser { Id = 1, UserName = "cevdet@deneme.com", NormalizedUserName = "CEVDET@DENEME.COM", Email = "cevdet@deneme.com", NormalizedEmail = "CEVDET@DENEME.COM", EmailConfirmed = false, ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString() };

			AppUser uye2 = new AppUser { Id = 2, UserName = "selami@deneme.com", NormalizedUserName = "SELAMI@DENEME.COM", Email = "selami@deneme.com", NormalizedEmail = "SELAMI@DENEME.COM", EmailConfirmed = false, ConcurrencyStamp = Guid.NewGuid().ToString(), SecurityStamp = Guid.NewGuid().ToString() };

			PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();

			uye1.PasswordHash = hasher.HashPassword(uye1, "ceVdo_123");
			uye2.PasswordHash = hasher.HashPassword(uye2, "seLo_123");

			builder.HasData(uye1, uye2);
		}
	}
}
