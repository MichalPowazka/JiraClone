using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Commands.Project.DeleteProject
{
    public class DeleteProjectCommand : IRequest<long>
    {
        public long Id;
    }
}
