using BurgerRoyale.Domain.DTO;
using FluentValidation;

namespace BurgerRoyale.API.Validators
{
	public class OrderDTOValidator : AbstractValidator<CreateOrderDTO>
	{
		public OrderDTOValidator()
		{
			When(w => w is not null, () =>
			{
				RuleFor(r => r.OrderProducts).NotNull().NotEmpty().WithMessage("Nenhum produto adicionado.");
				RuleForEach(r => r.OrderProducts).ChildRules(x =>
				{
					x.RuleFor(r => r.Quantity).GreaterThan(0).WithMessage("A quantidade deve ser maior que zero.");
					x.RuleFor(r => r.ProductId).NotNull().NotEmpty();
				});

			});
		}
	}
}