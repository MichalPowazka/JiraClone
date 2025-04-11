using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Querries.User.GetUserByEmail
{
    public class GetUserByEmailQuery : IRequest<Models.User>
    {
        public string Email { get; set; }
    }
}
