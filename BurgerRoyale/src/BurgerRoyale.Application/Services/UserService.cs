using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
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

			return new UserDTO
			{
				Cpf = Format.FormatCpf(userEntity.Cpf),
				Email = userEntity.Email,
				Name = userEntity.Name,
				UserType = userEntity.UserType
			};
		}

		public async Task<UserDTO> CreateAsync(UserDTO model)
		{
			var cpf = Format.NormalizeCpf(model.Cpf);

			User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Cpf == cpf);

			if (userEntity is not null)
			{
				throw new DomainException("CPF já cadastrado");
			}

			var user = new User
			{
				Cpf = cpf,
				Email = model.Email,
				Name = model.Name,
				UserType = model.UserType
			};

			await _userRepository.AddAsync(user);

			return new UserDTO
			{
				Cpf = Format.FormatCpf(user.Cpf),
				Email = user.Email,
				Name = user.Name,
				UserType = user.UserType
			};
		}

		public async Task<UserDTO> Update(Guid userId, UserDTO model)
		{
			var cpf = Format.NormalizeCpf(model.Cpf);

			User? userEntity = await _userRepository.FindFirstDefaultAsync(x =>
				x.Id == userId && x.Cpf == cpf
			);

			if (userEntity is null)
			{
				throw new NotFoundException("CPF não encontrado");
			}

			userEntity.Name = model.Name;
			userEntity.UserType = model.UserType;
			userEntity.Email = model.Email;

			await _userRepository.UpdateAsync(userEntity);

			return new UserDTO
			{
				Cpf = Format.FormatCpf(userEntity.Cpf),
				Email = userEntity.Email,
				Name = userEntity.Name,
				UserType = userEntity.UserType
			};
		}

		public async Task Delete(Guid userId)
		{
			User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Id == userId);

			if (userEntity is null)
			{
				throw new NotFoundException("Usuário não encontrado");
			}

			_userRepository.Remove(userEntity);
		}
	}
}
