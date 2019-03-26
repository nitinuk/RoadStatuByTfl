using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoadStatus.Config
{
    public interface IConfigurationManager
    {
        string GetAppSetting(string key);
    }
   
}
