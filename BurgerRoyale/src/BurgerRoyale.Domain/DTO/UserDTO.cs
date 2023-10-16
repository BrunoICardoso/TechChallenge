using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.DTO
{
	public class UserDTO
	{
		public string Cpf { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public UserType UserType { get; set; }
	}
}
