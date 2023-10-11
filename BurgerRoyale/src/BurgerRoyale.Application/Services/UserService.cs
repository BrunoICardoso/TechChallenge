
using BurgerRoyale.Domain.Entities;
using BurgerRoyale.Domain.Helpers;
using BurgerRoyale.Domain.Interface.Repositories;
using BurgerRoyale.Domain.Interface.ResponseDefault;
using BurgerRoyale.Domain.Interface.Services;
using BurgerRoyale.Domain.ResponseDefault;
using BurgerRoyale.Domain.Validation;
using BurgerRoyale.Domain.DTO;
using System.Net;

namespace BurgerRoyale.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ReturnAPI<UserDTO>> GetByCpf(string cpf)
        {
            ReturnAPI<UserDTO> ret = new ReturnAPI<UserDTO>();

            try
            {
                User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Cpf == cpf);

                if (userEntity is null)
                {
                    ret.StatusCode = HttpStatusCode.BadRequest;
                    ret.Message = "CPF não encontrado.";

                    return ret;
                }

                ret.Data = new UserDTO
                {
                    Cpf = userEntity.Cpf,
                    Email = userEntity.Email,
                    Name = userEntity.Name,
                    UserType = userEntity.UserType
                };

            }
            catch (Exception e)
            {
                ret.Exception = e;
                ret.Message = e.Message;
            }

            return ret;
        }
        public async Task<ReturnAPI<UserDTO>> GetByEmail(string email)
        {
            ReturnAPI<UserDTO> ret = new ReturnAPI<UserDTO>();

            try
            {
                User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Email == email);

                if (userEntity is null)
                {
                    ret.StatusCode = HttpStatusCode.BadRequest;
                    ret.Message = "E-mail não encontrado.";

                    return ret;
                }

                ret.Data = new UserDTO
                {
                    Cpf = userEntity.Cpf,
                    Email = userEntity.Email,
                    Name = userEntity.Name,
                    UserType = userEntity.UserType
                };

            }
            catch (Exception e)
            {
                ret.Exception = e;
                ret.Message = e.Message;
            }

            return ret;
        }
        public async Task<ReturnAPI> CreateAsync(UserDTO model)
        {
            ReturnAPI ret = new ReturnAPI();

            try
            {
                ReturnAPI existingUser = await GetByCpf(model.Cpf);

                if (existingUser.IsSuccessStatusCode)
                {
                    ret.StatusCode = HttpStatusCode.BadRequest;
                    ret.Message = "CPF já cadastrado.";

                    return ret;
                }

                await _userRepository.AddAsync(new User
                {
                    Cpf = model.Cpf,
                    Email = model.Email,
                    Name = model.Name,
                    UserType = model.UserType
                });

            }
            catch (Exception e)
            {
                ret.Exception = e;
                ret.Message = e.Message;
            }

            return ret;
        }
        public async Task<ReturnAPI> Update(UserDTO model)
        {
            ReturnAPI ret = new ReturnAPI();

            try
            {
                User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Cpf == model.Cpf);

                if (userEntity is null)
                {
                    ret.StatusCode = HttpStatusCode.BadRequest;
                    ret.Message = "Usúário não encontrado .";

                    return ret;
                }

                userEntity.Name = model.Name;
                userEntity.UserType = model.UserType;
                userEntity.Email = model.Email;

                await _userRepository.UpdateAsync(userEntity);

            }
            catch (Exception e)
            {
                ret.Exception = e;
                ret.Message = e.Message;
            }

            return ret;

        }
        public async Task<ReturnAPI> Delete(string cpf)
        {
            ReturnAPI ret = new ReturnAPI();
            try
            {
                User? userEntity = await _userRepository.FindFirstDefaultAsync(x => x.Cpf == cpf);

                if (userEntity is null)
                {
                    ret.StatusCode = HttpStatusCode.BadRequest;
                    ret.Message = "Erro ao excluir usuário.";

                    return ret;
                }

                _userRepository.Remove(userEntity);
            }
            catch (Exception e)
            {
                ret.Exception = e;
                ret.Message = e.Message;
            }
           
            return ret;
        }
    }
}
