using MediatR;

namespace Core.Commands.Project.CreateProject
{
    public class CreateProjectComand : IRequest<int> 
    {
        public required string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
