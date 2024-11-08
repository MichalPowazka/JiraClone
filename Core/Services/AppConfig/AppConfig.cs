using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace Core.Services.AppConfig
{
    public class AppConfig : IAppConfigService
    {
        public string ConnectionString { get; set; }



   
    }
}
