using Domain.Entities;
using System.Collections.Generic;

namespace App.Contracts
{
    public interface IJWTGenerator
    {
        string CreateToken(User user, List<string> roles);
    }
}
