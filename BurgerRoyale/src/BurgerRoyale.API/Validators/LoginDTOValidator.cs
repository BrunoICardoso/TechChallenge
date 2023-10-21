using BurgerRoyale.Domain.DTO;
using FluentValidation;

namespace BurgerRoyale.API.Validators
{
	public class UserDTOValidator : AbstractValidator<RequestUserDTO>
	{
		public UserDTOValidator()
		{
			When(w => w is not null, () =>
			{
				RuleFor(r => r.Cpf)
					.NotNull()
					.NotEmpty()
					.Must(x => Domain.Helpers.Validate.IsCpfValid(x))
					.WithMessage("Preencha um CPF válido");

				RuleFor(r => r.Email)
					.NotNull()
					.NotEmpty()
					.EmailAddress();

				RuleFor(r => r.UserType)
					.NotNull()
					.NotEmpty()
					.IsInEnum();
			});
		}
	}
}
