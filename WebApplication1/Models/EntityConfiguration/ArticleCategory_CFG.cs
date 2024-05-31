using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplication1.Models.Entities;

namespace WebApplication1.Models.EntityConfiguration
{
	public class ArticleCategory_CFG : IEntityTypeConfiguration<ArticleCategory>
	{
		public void Configure(EntityTypeBuilder<ArticleCategory> builder)
		{
			builder.HasKey(x => x.ArticleCategoryId);
			builder.HasOne(x => x.Article).WithMany(x => x.ArticleCategories);
			builder.HasOne(x => x.Category).WithMany(x => x.ArticleCategories);

			builder.HasData(
				new ArticleCategory()
				{
					ArticleId = 1,
					CategoryId = 5,
					ArticleCategoryId=1
				},

                new ArticleCategory()
                {
                    ArticleId = 2,
                    CategoryId = 1,
                    ArticleCategoryId = 2
                },

                new ArticleCategory()
                {
                    ArticleId = 3,
                    CategoryId = 2,
                    ArticleCategoryId = 3
                },
                new ArticleCategory()
                {
                    ArticleId = 4,
                    CategoryId = 3,
                    ArticleCategoryId = 4
                },
                new ArticleCategory()
                {
                    ArticleId = 5,
                    CategoryId = 4,
                    ArticleCategoryId = 5
                }
                );
		}
	}
}
