using FluentValidation;
using WebApplication1.VMs;


namespace WebApplication1.Validations
{
	public class LoginVMValidator: AbstractValidator<LoginAppUserVM>
	{
		public LoginVMValidator()
		{
			RuleFor(x => x.Email).NotEmpty().WithMessage("Kullanıcı adı girmelisiniz!");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez!");
		}
	}
}
