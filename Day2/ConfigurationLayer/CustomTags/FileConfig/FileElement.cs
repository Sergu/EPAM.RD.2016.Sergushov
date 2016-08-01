using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.CustomTags.FileConfig
{
    public class FileElement : ConfigurationElement
    {
        [ConfigurationProperty("fileName",IsRequired = true,IsKey = true)]
        public string FileName
        {
            get
            {
                return (string)base["fileName"];
            }
            set
            {
                base["fileName"] = value;
            }
        }
    }
}
