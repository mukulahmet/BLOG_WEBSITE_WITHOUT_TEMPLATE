namespace WebApplication1.VMs
{
    public class UserVM
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? About { get; set; }
        public string? Email { get; set; }
        public IFormFile? ProfilePicture { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
