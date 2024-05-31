using WebApplication1.Models.Entities;

namespace WebApplication1.VMs
{
    public class AuthorDetailsVm
    {
        public AppUser User { get; set; }
        public List<Article> Articles { get; set; }
    }
}
