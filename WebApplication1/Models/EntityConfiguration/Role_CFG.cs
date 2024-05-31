using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.EntityConfiguration
{
	public class Role_CFG : IEntityTypeConfiguration<AppRole>
	{
		public void Configure(EntityTypeBuilder<AppRole> builder)
		{
			builder.HasData(
				new AppRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() },
				new AppRole { Id = 2, Name = "Uye", NormalizedName = "UYE", ConcurrencyStamp = Guid.NewGuid().ToString() }
				);
		}
	}
}
