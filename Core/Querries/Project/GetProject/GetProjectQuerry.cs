using Core.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Core.Querries.Project.GetProject
{
    public class GetProjectQuerry: IRequest<Models.Project>
    {
        public int Id { get; set; } 
    }
}
