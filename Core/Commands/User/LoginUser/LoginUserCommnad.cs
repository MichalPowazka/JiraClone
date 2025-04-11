using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.User.LoginUser;

public class LoginUserCommnad : IRequest<string>
{
public required string Username { get; set; }

public required string Password { get; set; }

}


