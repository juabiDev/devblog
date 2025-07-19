using ServicesContracts.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesContracts.ServicesContracts
{
    public interface IJwtService
    {
        AuthenticationResponse CreateJwtToken(LoginRequest user);
        AuthenticationResponse CreateJwtToken(UserDTO user);
    }
}
