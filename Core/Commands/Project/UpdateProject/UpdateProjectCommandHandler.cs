using Core.Commands.Project.CreateProject;
using Core.Repostiories.Projects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Project.UpdateProject
{
    class UpdateProjectCommandHandler(IProjectRepository projectRepository): IRequestHandler<UpdateProjectCommand,int>
    {
        private readonly IProjectRepository _projectRepository = projectRepository;

        public async Task<int> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = new Models.Project()
            {
                Name = request.Name,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };

            return await _projectRepository.AddProject(project);
        }
    }
}
