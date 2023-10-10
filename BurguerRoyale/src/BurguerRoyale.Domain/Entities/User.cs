using BurguerRoyale.Domain.Enumerators;

namespace BurguerRoyale.Domain.Entities
{
	public class User : Entity
	{
		public string Cpf { get; private set; }
		public string Name { get; private set; }
		public decimal Email { get; private set; }
		public virtual UserType UserType { get; set; }

		public User(string cpf, string name, decimal email, UserType userType)
		{
			Cpf = cpf;
			Name = name;
			Email = email;
			UserType = userType;
		}
	}
}