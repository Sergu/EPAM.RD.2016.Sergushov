using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.CustomTags.FileConfig
{
    public class RegisterFileConfigSection : ConfigurationSection
    {
        public static RegisterFileConfigSection GetConfig()
        {
            return (RegisterFileConfigSection)ConfigurationManager.GetSection("RegisterFiles") ?? new RegisterFileConfigSection();
        }
        [ConfigurationProperty("Files")]
        [ConfigurationCollection(typeof(FileCollection), AddItemName = "File")]
        public FileCollection Files
        {
            get
            {
                object o = this["Files"];
                return o as FileCollection;
            }
        }
    }
}
