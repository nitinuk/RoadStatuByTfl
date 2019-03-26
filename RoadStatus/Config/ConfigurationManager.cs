using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace RoadStatus.Config
{
    public class ConfigurationManager : IConfigurationManager
    {
        //public ConfigurationManager()
        //{

        //}

        public string GetAppSetting(string key)
        {
            return  System.Configuration.ConfigurationManager.AppSettings[key];
        }

    }
}
