using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Exceptions
{
    public class XmlException : Exception
    {
        public XmlException() { }
        public XmlException(string message) : base(message) { }
    }
}
