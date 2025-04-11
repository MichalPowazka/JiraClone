using Core.Commands.User.CreateUser;
using Core.Commands.User.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JiraClone.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController(IMediator mediatr) : ControllerBase
    {
        private readonly IMediator _mediatr = mediatr;


        [HttpPost]
        public async Task<long> CreateUser(CreateUserCommand request)
        {
            return await _mediatr.Send(request);
        }

        [HttpPost("login")]
        public async Task<string> Login(LoginUserCommnad request)
        {
            return await _mediatr.Send(request);
        }


        //tworzenie uzytownika
        //logowanie 
        //zmiana hasla
    }
}
