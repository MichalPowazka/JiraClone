using Core.Repostiories.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Querries.User.GetUserByEmail
{
    public class GetUserByEmailQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByEmailQuery, Models.User>
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<Models.User> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUser(request.Email);
        }
    }
}
