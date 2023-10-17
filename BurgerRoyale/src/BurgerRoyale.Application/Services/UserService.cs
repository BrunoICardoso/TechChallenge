using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Enumerators;
using BurgerRoyale.Domain.Exceptions;
using BurgerRoyale.Domain.Helpers;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.Services;

namespace BurgerRoyale.Application.Services
{
    public class UserService : IUserService
	{
		private readonly IUserRepository _userRepository;

		public UserService(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		public async Task<UserDTO> GetByCpf(string cpf)
		{
			cpf = Format.NormalizeCpf(cpf);

			User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Cpf == cpf);

			if (userEntity is null)
			{
				throw new NotFoundException("CPF não encontrado");
			}

			return new UserDTO(userEntity);
		}

		public async Task<UserDTO> CreateAsync(RequestUserDTO model)
		{
			var cpf = Format.NormalizeCpf(model.Cpf);

			bool userAlreadyExists = await _userRepository.AnyAsync(x => x.Cpf == cpf);

			if (userAlreadyExists)
			{
				throw new DomainException("CPF já cadastrado");
			}

			var user = new User(
				cpf,
				model.Email,
				model.Name,
				model.UserType
			);

			await _userRepository.AddAsync(user);

			return new UserDTO(user);
		}

		public async Task<UserDTO> Update(Guid userId, RequestUserDTO model)
		{
			var cpf = Format.NormalizeCpf(model.Cpf);

			User? user = await _userRepository.FindFirstDefaultAsync(x =>
				x.Id == userId && x.Cpf == cpf
			);

			if (user is null)
			{
				throw new NotFoundException("CPF não encontrado");
			}

			user.SetDetails(model.Name, model.Email, model.UserType);

			await _userRepository.UpdateAsync(user);

			return new UserDTO(user);
		}

		public async Task Delete(Guid userId)
		{
			User? user = await _userRepository.FindFirstDefaultAsync(x => x.Id == userId);

			if (user is null)
			{
				throw new NotFoundException("Usuário não encontrado");
			}

			_userRepository.Remove(user);
		}

		public async Task<UserDTO> GetById(Guid userId)
		{
			User? user = await _userRepository.FindFirstDefaultAsync(x => x.Id == userId);

			if (user is null)
			{
				throw new NotFoundException("Usuário não encontrado");
			}

			return new UserDTO(user);
		}

		public async Task<IEnumerable<UserDTO>> GetUsers(UserType? userType)
		{
			var users = (userType == null)
				? await _userRepository.GetAllAsync()
				: await _userRepository.FindAsync(x => x.UserType == userType);

			return users.Select(user => new UserDTO(user));
		}
	}
}
