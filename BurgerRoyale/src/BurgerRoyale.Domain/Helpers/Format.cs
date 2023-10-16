namespace BurgerRoyale.Domain.Helpers
{
	public static class Format
	{
		public static string FormatCpf(string cpf)
		{
			cpf = new string(cpf.Where(char.IsDigit).ToArray());
			if (string.IsNullOrEmpty(cpf) || cpf.Length != 11)
				return cpf;

			return $"{cpf.Substring(0, 3)}.{cpf.Substring(3, 3)}.{cpf.Substring(6, 3)}-{cpf.Substring(9, 2)}";
		}

		public static string NormalizeCpf(string cpf)
		{
			return new string(cpf.Where(char.IsDigit).ToArray());
		}
	}
}