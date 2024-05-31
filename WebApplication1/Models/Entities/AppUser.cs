using Microsoft.AspNetCore.Identity;
using WebApplication1.Models.Enums;

namespace WebApplication1.Models.Entities
{
	public class AppUser:IdentityUser<int>,IBaseEntity
	{
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? About { get; set; }
		public string? ProfilePicture { get; set; }
		public int? ConfirmCode { get; set; }
        //public ICollection<Category>? FollowedCategories { get; set; } = new List<Category>();

        public ICollection<Article>? Articles { get; set; }

		public DateTime CreatedDate { get; set; }=DateTime.Now;
		public DateTime? UpdatedDate { get; set; }
		public DateTime? DeletedDate { get; set; }
		public Status? Status { get; set; }
	}
}
