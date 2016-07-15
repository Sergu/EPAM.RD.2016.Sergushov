using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConsoleApplication
{
    public class RegisterServicesConfig : ConfigurationSection
    {
        public static RegisterServicesConfig GetConfig()
        {
            return (RegisterServicesConfig)ConfigurationManager.GetSection("RegisterServices") ?? new RegisterServicesConfig();
        }
        [ConfigurationProperty("Services")]
        [ConfigurationCollection(typeof(Services),AddItemName = "Service")]
        public Services Services
        {
            get
            {
                object o = this["Services"];
                return o as Services;
            }
        }
    }
}
