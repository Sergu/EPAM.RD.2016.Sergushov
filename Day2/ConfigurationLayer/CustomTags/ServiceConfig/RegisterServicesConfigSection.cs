using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.CustomTags.ServiceConfig
{
    public class RegisterServicesConfigSection : ConfigurationSection
    {
        public static RegisterServicesConfigSection GetConfig()
        {
            return (RegisterServicesConfigSection)ConfigurationManager.GetSection("RegisterServices") ?? new RegisterServicesConfigSection();
        }
        [ConfigurationProperty("Services")]
        [ConfigurationCollection(typeof(ServiceCollection), AddItemName = "Service")]
        public ServiceCollection Services
        {
            get
            {
                object o = this["Services"];
                return o as ServiceCollection;
            }
        }
    }
}
