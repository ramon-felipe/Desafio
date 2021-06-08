using Holiday.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Holiday.Repository.Interfaces
{
    public interface IUserDataBase
    {
        User Get(string name, string password);
    }
}
