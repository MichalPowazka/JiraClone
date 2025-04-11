using Core.Repostiories.Projects;
using MediatR;


namespace Core.Commands.Project.CreateProject
{
    public class CreateProjectComandHandler(IProjectRepository projectRepository) : IRequestHandler<CreateProjectComand, int>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<int> Handle(CreateProjectComand request, CancellationToken cancellationToken)
        {
            var project = new Models.Project()
            {
                Name = request.Name,
                StartDate= request.StartDate,
                EndDate= request.EndDate
            };

           return await _projectRepository.AddProject(project);
        }
    }
}
