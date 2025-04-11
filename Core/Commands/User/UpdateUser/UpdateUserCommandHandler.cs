using Core.Repostiories.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.User.UpdateUser
{
    public class UpdateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<UpdateUserCommand, long>
    {
        private readonly IUserRepository _userRepository = userRepository;
        async Task<long> IRequestHandler<UpdateUserCommand, long>.Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUser(request.Id);
            return await _userRepository.UpdateUser(user);
        }
    }
}
