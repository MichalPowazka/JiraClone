using Core.Models;
using Core.Repostiories.Projects;
using Core.Querries.Project.GetPagedFilteredList;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Querries.Project.GetProject
{
    public class GetProjectQuerryHandler(IProjectRepository repository) : IRequestHandler<GetProjectQuerry, Models.Project>
    {
        private readonly IProjectRepository _repository = repository;

        public async Task<Models.Project> Handle(GetProjectQuerry request, CancellationToken cancellationToken)
        {
            return await _repository.GetProject(request.Id);
        }
    }
}
