using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.CustomTags.AddressConfig
{
    public class RegisterAddressConfigSection : ConfigurationSection
    {
        public static RegisterAddressConfigSection GetConfig()
        {
            return (RegisterAddressConfigSection)ConfigurationManager.GetSection("RegisterAddresses") ?? new RegisterAddressConfigSection();
        }
        [ConfigurationProperty("Addresses")]
        [ConfigurationCollection(typeof(AddressCollection), AddItemName = "Address")]
        public AddressCollection Addresses
        {
            get
            {
                object o = this["Addresses"];
                return o as AddressCollection;
            }
        }
    }
}
