using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConsoleApplication
{
    public class Service : ConfigurationElement
    {
        [ConfigurationProperty("serviceType",IsRequired = true)]
        public Type ServiceType
        {
            get
            {
                return this["type"] as Type;
            }
        }
        [ConfigurationProperty("count",IsRequired = true)]
        public int Count
        {
            get
            {
                return (int)this["count"];
            }
        }
    }
}
