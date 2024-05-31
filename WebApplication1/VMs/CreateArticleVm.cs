using WebApplication1.Models.Entities;

namespace WebApplication1.VMs
{
    public class CreateArticleVm
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public ICollection<Category>? Categories { get; set; }
        public ICollection<int> SelectedCategories { get; set; }
    }
}
