namespace WebApplication1.VMs
{
	public class LoginAppUserVM
	{
		public int Id { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }

		public bool IsEmailConfirmed { get; set; }
	}
}
