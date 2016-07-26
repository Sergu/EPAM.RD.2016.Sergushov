using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    [Serializable]
    public class EndPointAddress
    {
        public string address { get; set; }
        public int port { get; set; }
    }
}
