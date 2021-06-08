using Holiday.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Holiday.Services.Interfaces
{
    public interface IToken
    {
        string GenerateToken(User user);
    }
}
