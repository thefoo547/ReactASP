using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Contracts
{
    public interface IJWTGenerator
    {
        string CreateToken(User user, List<string> roles);
    }
}
