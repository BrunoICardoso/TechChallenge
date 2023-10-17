using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Helpers;

namespace BurgerRoyale.Domain.DTO
{
	public class UserDTO
	{
		public Guid Id { get; set; }
		public string Cpf { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public UserType UserType { get; set; }

		public string UserTypeDescription
		{
			get => UserType.GetDescription();
		}

		public UserDTO(User user)
		{
			Id = user.Id;
			Cpf = Format.FormatCpf(user.Cpf);
			Name = user.Name;
			Email = user.Email;
			UserType = user.UserType;
		}
	}
}
