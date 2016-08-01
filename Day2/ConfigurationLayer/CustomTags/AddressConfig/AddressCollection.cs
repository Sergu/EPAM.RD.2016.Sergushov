using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.CustomTags.AddressConfig
{
    public class AddressCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new AddressElement();
        }

        public AddressElement this[int index]
        {
            get
            {
                return BaseGet(index) as AddressElement;
            }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((AddressElement)element);
        }
    }
}
