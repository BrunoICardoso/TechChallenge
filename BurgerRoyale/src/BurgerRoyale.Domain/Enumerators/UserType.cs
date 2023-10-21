using System.ComponentModel;

namespace BurgerRoyale.Domain.Enumerators
{
	public enum UserType
	{
		[Description("Cliente")]
		Customer = 1,

		[Description("Funcionário")]
		Employee = 2
	}
}
