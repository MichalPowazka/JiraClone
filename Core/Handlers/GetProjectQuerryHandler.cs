using Core.Models;
using Core.Querries;
using Core.Repostiories.Projects;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Handlers
{
    public class GetProjectQuerryHandler(IProjectRepository repository) : IRequestHandler<GetProjectQuerry, Project>
    {
        private readonly IProjectRepository _repository = repository;

        public async Task<Project> Handle(GetProjectQuerry request, CancellationToken cancellationToken)
        {
            return await _repository.GetProject(request.Id);
        }
    }
}
