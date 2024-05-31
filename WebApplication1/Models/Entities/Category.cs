using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Entities
{
	public class Category:IBaseEntity
	{
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
		public ICollection<ArticleCategory>? ArticleCategories { get; set; }

		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public Status? Status { get; set; }
	}
}
