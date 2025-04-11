using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Querries.User.GetUserById
{
    public class GetUserByIdQuery : IRequest<Models.User>
    {
        public string Id { get; set; }
    }
}
