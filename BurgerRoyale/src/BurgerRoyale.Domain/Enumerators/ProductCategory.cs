using System.ComponentModel;

namespace BurgerRoyale.Domain.Enumerators
{
	public enum ProductCategory
	{
		[Description("Lanche")]
		Lanche,

		[Description("Acompanhamento")]
		Acompanhamento,

		[Description("Bebida")]
		Bebida,

		[Description("Sobremesa")]
		Sobremesa
	}
}
