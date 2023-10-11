using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Entities
{
	public class User : Entity
	{
		public string Cpf { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public virtual UserType UserType { get; set; }

		public User(string cpf, string name, string email, UserType userType)
		{
			Cpf = cpf;
			Name = name;
			Email = email;
			UserType = userType;
		}
	}
}