using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.NewFolder
{
    public interface IUserService
    {
        public string GenerateDefaultPassword(int length = 8);
        public string HashPassword(string password);


    }
}
