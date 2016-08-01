using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.CustomTags.AddressConfig
{
    public class AddressElement : ConfigurationElement
    {
        [ConfigurationProperty("address", IsKey = false, IsRequired = false,DefaultValue = "127.0.0.1")]
        public string Address
        {
            get
            {
                return (string)base["address"];
            }
            set
            {
                base["address"] = value;
            }
        }
        [ConfigurationProperty("port", IsKey = false, IsRequired = true)]
        public int Port
        {
            get
            {
                return (int)base["port"];
            }
            set
            {
                base["port"] = value;
            }
        }
    }
}
