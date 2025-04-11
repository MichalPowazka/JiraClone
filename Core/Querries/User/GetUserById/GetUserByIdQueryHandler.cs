using Core.Repostiories.Users;
using MediatR;

namespace Core.Querries.User.GetUserById
{
    class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, Models.User>
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<Models.User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUser(request.Id);
        }
    }
}
