using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Querries
{
    public class GetProjectQuerry: IRequest<Project>
    {
        public int Id { get; set; } 
    }
}
