using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ConsoleApplication
{
    [ConfigurationCollection(typeof(Service))]
    public class Services : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new Service();
        }

        public Service this[int index]
        {
            get
            {
                return BaseGet(index) as Service;
            }
        }
        public new Service this[string responseString]
        {
            get { return (Service)BaseGet(responseString); }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Service)element).ServiceType;
        }
    }
}
