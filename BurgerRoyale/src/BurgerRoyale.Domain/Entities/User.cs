using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.Domain.Entities
{
	public class User : Entity
	{
		public string Cpf { get; private set; }
		public string Name { get; private set; }
		public string Email { get; private set; }
		public virtual UserType UserType { get; private set; }

		public User(string cpf, string name, string email, UserType userType)
		{
			Cpf = cpf;
			Name = name;
			Email = email;
			UserType = userType;
		}

		public void SetDetails(string name, string email, UserType userType)
		{
			Name = name;
			Email = email;
			UserType = userType;
		}
	}
}