using System;
using System.Collections.Generic;
using System.Text;

namespace Holiday.Domain.RequestModels
{
    public class UserLoginRequestModel
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
