using WebApplication1.Models.Entities;

namespace WebApplication1.VMs
{
    public class MemberHomePageVm
    {
        public ICollection<Category> FollowedCategories { get; set; }
        public ICollection<Article> ArticlesFromFollowedCategories { get; set; }
        public ICollection<Article> MostReadArticles { get; set; }
        public ICollection<Article> GeneralArticles { get; set; }
    }
}
