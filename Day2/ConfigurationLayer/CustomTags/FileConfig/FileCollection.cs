using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.CustomTags.FileConfig
{
    [ConfigurationCollection(typeof(FileElement))]
    public class FileCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FileElement();
        }

        public FileElement this[int index]
        {
            get
            {
                return BaseGet(index) as FileElement;
            }
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FileElement)element).FileName;
        }
    }
}
