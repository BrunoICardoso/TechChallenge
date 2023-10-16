using BurgerRoyale.Domain.Helpers;

namespace BurgerRoyale.UnitTests.Domain.Validation
{
	public class ValidationTests
	{
		[Theory]
		[InlineData("123.456.789-09")]
		[InlineData("529.982.247-25")]
		[InlineData("111.222.333-96")]
		[InlineData("12345678909")]
		[InlineData("52998224725")]
		public void Deve_Validar_CPFS_Validos(string cpf)
		{
			bool resultado = Validate.IsCpfValid(cpf);

			Assert.True(resultado);
		}

		[Theory]
		[InlineData("123.456.789-10")]
		[InlineData("12345678910")]
		[InlineData("111.222.333-94")]
		[InlineData("000.000.000-01")]
		[InlineData("111.222.333-93")]
		[InlineData("abc.def.ghi-jk")]
		[InlineData("")]
		[InlineData(null)]
		public void Deve_Invalidar_CPFS_Invalidos(string cpf)
		{
			bool resultado = Validate.IsCpfValid(cpf);

			Assert.False(resultado);
		}
	}
}