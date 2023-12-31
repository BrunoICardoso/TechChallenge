﻿using BurgerRoyale.Domain.DTO;
using FluentValidation;

namespace BurgerRoyale.API.Validators
{
	public class LoginDTOValidator : AbstractValidator<LoginDTO>
	{
		public LoginDTOValidator()
		{
			When(w => w is not null, () =>
			{
				RuleFor(r => r.Cpf)
					.NotNull()
					.NotEmpty()
					.Must(x => Domain.Helpers.Validate.IsCpfValid(x))
					.WithMessage("Preencha um CPF válido");
			});
		}
	}
}
