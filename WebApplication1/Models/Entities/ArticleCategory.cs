using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models.Entities
{
	public class ArticleCategory
	{
        public int ArticleCategoryId { get; set; }
        public int ArticleId { get; set; }
		public int CategoryId { get; set; }

		
		[ForeignKey("ArticleId")]
		public Article? Article { get; set; }

		[ForeignKey("CategoryId")]
		public Category? Category { get; set; }
	}
}
