using Holiday.Domain.RequestModels;
using Holiday.Domain.ViewModels;
using Holiday.Repository.Interfaces;
using Holiday.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Holiday.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<HolidaysController> _logger;
        private readonly IUserDataBase _userDataBase;
        private readonly IToken _tokenService;

        public UserController(ILogger<HolidaysController> logger, 
                              IUserDataBase userDataBase,
                              IToken tokenService)
        {
            _logger = logger;
            _userDataBase = userDataBase;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public ActionResult<UserTokenViewModel> Authenticate(UserLoginRequestModel request)
        {
            var user = _userDataBase.Get(request.Name, request.Password);

            if (user == null)
                return NotFound("User not found");

            var token = _tokenService.GenerateToken(user);

            return Ok(new UserTokenViewModel { Id = user.Id, Name = user.Name, Role = user.Role, Token = token });
        }

    }
}
