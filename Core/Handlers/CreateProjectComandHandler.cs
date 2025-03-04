using Core.Commands;
using Core.Models;
using Core.Repostiories.Projects;
using MediatR;

namespace Core.Handlers
{
    public class CreateProjectComandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProjectComand, int>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<int> Handle(CreateProjectComand request, CancellationToken cancellationToken)
        {
            var project = new Project()
            {
                Name = request.Name,
                StartDate= request.StartDate,
                EndDate= request.EndDate
            };

           return await _projectRepository.AddProject(project);
        }
    }
}
