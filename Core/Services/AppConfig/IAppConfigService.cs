using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.AppConfig
{

    public interface IAppConfigService
    {
        public string ConnectionString { get; set; }    
     }
}
