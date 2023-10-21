using Bogus;
using Bogus.Extensions.Brazil;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;

namespace BurgerRoyale.UnitTests.Domain.EntitiesMocks
{
	public static class UserMock
	{
		public static User Get
		(
			string? cpf = null,
			string? name = null,
			string? email = null,
			UserType? userType = null
		)
		{
			return UserFakerInstantiator(cpf, name, email, userType)
				.Generate();
		}

		public static List<User> GetList
		(
			int? quantity = null,
			UserType? userType = null
		)
		{
			return UserFakerInstantiator(userType: userType)
				.Generate(quantity ?? 3);
		}

		private static Faker<User> UserFakerInstantiator
		(
			string? cpf = null,
			string? name = null,
			string? email = null,
			UserType? userType = null
		)
		{
			return new Faker<User>()
				.CustomInstantiator(faker => new User(
					cpf ?? faker.Person.Cpf(),
					name ?? faker.Person.FullName,
					email ?? faker.Person.Email,
					userType ?? faker.PickRandom<UserType>()
				));
		}
	}
}
