using BurgerRoyale.Domain.Enumerators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurgerRoyale.Domain.DTO
{
    public class UserDTO
    {
        public string Cpf { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public UserType UserType { get; set; }

    }
}
