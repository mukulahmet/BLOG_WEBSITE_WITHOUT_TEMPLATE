using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.EntityConfiguration
{
	public class Category_CFG : IEntityTypeConfiguration<Category>
	{
		public void Configure(EntityTypeBuilder<Category> builder)
		{
			builder.HasData(
				new Category { CategoryId=1,CategoryName="Bilim",CreatedDate=DateTime.Now},
				new Category { CategoryId=2,CategoryName="Gezi",CreatedDate=DateTime.Now},
				new Category { CategoryId=3,CategoryName="Psikiloji",CreatedDate=DateTime.Now},
				new Category { CategoryId=4,CategoryName="Eğitim",CreatedDate=DateTime.Now},
				new Category { CategoryId=5,CategoryName="Sinema",CreatedDate=DateTime.Now}
				);
		}
	}
}
