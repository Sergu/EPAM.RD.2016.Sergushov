using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.CustomTags.ServiceConfig
{
    public class ServiceElement : ConfigurationElement
    {
        [ConfigurationProperty("serviceType", IsKey = true, IsRequired = true)]
        public string ServiceType
        {
            get
            {
                return (string)base["serviceType"];
            }
            set
            {
                base["serviceType"] = value;
            }
        }
        [ConfigurationProperty("count", IsKey = false, IsRequired = true)]
        public int Count
        {
            get
            {
                return (int)base["count"];
            }
            set
            {
                base["count"] = value;
            }
        }
    }
}
