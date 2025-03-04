using Core.Commands;
using Core.Models;
using Core.Repostiories.Users;
using Core.Services.NewFolder;
using MediatR;

namespace Core.Handlers
{
    public class CreateUserCommandHandler(IUserRepository userRepository, IUserService userService) : IRequestHandler<CreateUserCommand, int>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserService _userService = userService;
        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var generatedPassword = _userService.GenerateDefaultPassword();
            User user = new User()
            {
                Email = request.Email,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PaswordHash = _userService.HashPassword(generatedPassword)
            };
            var output = await _userRepository.CreateUser(user);

            //wyslanie hasla do ziomka na email
            return output;
        }
    }
}
