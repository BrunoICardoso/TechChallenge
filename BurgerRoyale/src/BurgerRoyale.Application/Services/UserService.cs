using BurgerRoyale.Domain.DTO;
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Exceptions;
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
			User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Cpf == cpf);

			if (userEntity is null)
			{
				throw new NotFoundException("CPF não encontrado");
			}

			return new UserDTO
			{
				Cpf = userEntity.Cpf,
				Email = userEntity.Email,
				Name = userEntity.Name,
				UserType = userEntity.UserType
			};
		}

		public async Task<UserDTO> GetByEmail(string email)
		{
			User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Email == email);

			if (userEntity is null)
			{
				throw new NotFoundException("E-mail não encontrado");
			}

			return new UserDTO()
			{
				Cpf = userEntity.Cpf,
				Email = userEntity.Email,
				Name = userEntity.Name,
				UserType = userEntity.UserType
			};
		}

		public async Task CreateAsync(UserDTO model)
		{
			User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Cpf == model.Cpf);

			if (userEntity is not null)
			{
				throw new DomainException("CPF já cadastrado");
			}

			await _userRepository.AddAsync(new User
			{
				Cpf = model.Cpf,
				Email = model.Email,
				Name = model.Name,
				UserType = model.UserType
			});
		}

		public async Task Update(UserDTO model)
		{
			User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Cpf == model.Cpf);

			if (userEntity is null)
			{
				throw new NotFoundException("CPF não encontrado");
			}

			userEntity.Name = model.Name;
			userEntity.UserType = model.UserType;
			userEntity.Email = model.Email;

			await _userRepository.UpdateAsync(userEntity);
		}

		public async Task Delete(string cpf)
		{
			User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Cpf == cpf);

			if (userEntity is null)
			{
				throw new NotFoundException("CPF não encontrado");
			}

			_userRepository.Remove(userEntity);
		}
	}
}
