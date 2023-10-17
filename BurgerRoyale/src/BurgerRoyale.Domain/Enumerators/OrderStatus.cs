using System.ComponentModel;
using System.Reflection;

namespace BurgerRoyale.Domain.Enumerators
{
	public enum OrderStatus
	{
		[Description("Recebido")]
		Recebido,

		[Description("Em preparação")]
		EmPreparacao,

		[Description("Pronto")]
		Pronto,

		[Description("Finalizado")]
		Finalizado
	}
}
