using Holiday.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Holiday.Domain.ViewModels
{
    public class UserTokenViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public string Token { get; set; }
    }
}
