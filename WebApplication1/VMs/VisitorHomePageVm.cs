using WebApplication1.Models.Entities;

namespace WebApplication1.VMs
{
    public class VisitorHomePageVm
    {
        public ICollection<Article> MostReadArticles { get; set; }
        public ICollection<Article> GeneralArticles { get; set; }
    }
}
