using Core.Repostiories.Projects;
using MediatR;

using System.Threading.Tasks;

namespace Core.Commands.Project.DeleteProject
{
    public class DeleteProjectCommmandHandler(IProjectRepository projectRepository) : IRequestHandler<DeleteProjectCommand,long>
    {

        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<long> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var id = request.Id;

            return await _projectRepository.DeleteProject(id);
        }
    }
}
