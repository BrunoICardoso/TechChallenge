using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Helpers;
using BurgerRoyale.Domain.Repositories;
using BurgerRoyale.Domain.Services;
using BurgerRoyale.Domain.Validation;

namespace BurgerRoyale.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<User> GetByCpf(string cpf)
        {
            return await _userRepository.GetByCpf(Format.FormatCpf(cpf));
        }
        public async Task<User> GetByEmail(string email)
        {
            return await _userRepository.GetByEmail(email);
        }
        public async Task<User> CreateAsync(User user)
        {
            user.Cpf = Format.FormatCpf(user.Cpf);
            await _userRepository.AddAsync(user);
            return user;
        }
        public async Task<bool> Update(User user)
        {
            var existingUser = await GetByCpf(user.Cpf);
            if (existingUser != null && Validate.IsEmailValid(user.Email))
            {
                existingUser.Name = user.Name;
                existingUser.UserType = user.UserType;
                existingUser.Email = user.Email;

                await _userRepository.UpdateAsync(existingUser);
                return true;
            }
            return false;
        }
        public async Task<bool> Delete(string cpf)
        {
            var user = await GetByCpf(cpf);
            if (user != null)
            {
                _userRepository.Remove(user);
                return true;
            }
            return false;
        }
    }
}
