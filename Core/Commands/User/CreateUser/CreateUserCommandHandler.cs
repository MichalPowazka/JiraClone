
using Core.Commands.User.CreateUser;
using Core.Models;
using Core.Repostiories.Users;
using Core.Services.NewFolder;
using MediatR;
using System.Net.Mail;


namespace Core.Handlers
{
    public class CreateUserCommandHandler(IUserRepository userRepository, IUserService userService) : IRequestHandler<CreateUserCommand, long>
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IUserService _userService = userService;
        public async Task<long> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var generatedPassword = _userService.GenerateDefaultPassword();
            User user = new User()
            {
                Email = request.Email,
                LastName = request.LastName,
                FirstName = request.FirstName,
                PaswordHash = _userService.HashPassword(generatedPassword)
            };

            MailMessage passwordMessage = new MailMessage(new MailAddress("twojastara@wp.pl", "Ziomek Nowy"), new MailAddress("twojastara@wp.pl", "Ziomek Nowy"));
            passwordMessage.Subject = "SIEMA TWOJE HASLO WARIACIE";
            passwordMessage.IsBodyHtml = true;
            passwordMessage.Body = generatedPassword;

            var client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            client.PickupDirectoryLocation = "C://JiraClone/";

            client.Send(passwordMessage);

            return await _userRepository.CreateUser(user);
        }
    }
}
