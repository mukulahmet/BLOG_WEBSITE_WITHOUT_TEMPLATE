using System.ComponentModel.DataAnnotations.Schema;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Entities
{
	public class Article:IBaseEntity
	{
		public int ArticleId { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }

        public int? ReadCount { get; set; }

        public int UserId { get; set; }
		public AppUser? User { get; set; }

		public ICollection<ArticleCategory>? ArticleCategories { get; set; }

		public DateTime CreatedDate { get; set; }=DateTime.Now;
		public DateTime? UpdatedDate { get; set; }			
		public DateTime? DeletedDate { get; set; }
		public Status? Status { get; set; }
	}
}
