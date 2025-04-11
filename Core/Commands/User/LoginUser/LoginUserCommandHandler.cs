using Core.Repostiories.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.User.LoginUser
{
    public class LoginUserCommandHandler(IUserRepository userRepository) : IRequestHandler<LoginUserCommnad, string>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public Task<string> Handle(LoginUserCommnad request, CancellationToken cancellationToken)
        {
            return _userRepository.LoginUser(request.Username, request.Password);
        }
    }
}
