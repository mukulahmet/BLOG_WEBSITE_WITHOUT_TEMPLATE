using FluentValidation;
using WebApplication1.VMs;

namespace WebApplication1.Validations
{
	public class RegisterVMValidator:AbstractValidator<RegisterVM>
	{
		public RegisterVMValidator()
		{
			RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez!").MinimumLength(8).WithMessage("Şifre en az 8 karakterden oluşmalıdır!").Matches(@"[A-Z]+").WithMessage("Şifre en az 1 büyük harf içermelidir!").Matches(@"[a-z]+").WithMessage("Şifre en az 1 küçük harf içermelidir!");

			RuleFor(x => x.FirstName).NotEmpty().WithMessage("İsim boş geçilemez!");
			RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad boş geçilemez!");
			RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş geçilemez!");
			RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir e-mail adresi giriniz");
		}
	}
}
