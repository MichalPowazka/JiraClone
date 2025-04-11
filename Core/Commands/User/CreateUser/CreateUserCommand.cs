using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.User.CreateUser
{
    public class CreateUserCommand : IRequest<long>
    {
        public required string FirstName {get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
    }
}
