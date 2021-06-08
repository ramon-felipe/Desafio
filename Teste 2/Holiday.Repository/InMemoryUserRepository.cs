using Holiday.Domain.Models;
using Holiday.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Holiday.Repository
{
    public class InMemoryUserRepository : IUserDataBase
    {
        public List<User> Users { get; set; }
        public InMemoryUserRepository()
        {
            Users = new List<User>
            {
                { new User{ Id = 1, Name = "Ramon", Password = "12345", Role = Roles.USER} },
                { new User{ Id = 2, Name = "Felipe", Password = "54321", Role = Roles.ADMIN} }
            };
        }

        public User Get(string name, string password)
        {
            return Users.FirstOrDefault(u => u.Name.ToLower().Equals(name.ToLower()) && u.Password.Equals(password));
        }
    }
}
