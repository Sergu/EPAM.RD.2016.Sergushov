using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Entities
{
    [Serializable]
    public enum Operation
    {
        add,
        remove
    };
    [Serializable]
    public class Message
    {
        public Operation operation { get; set; }
        public object param { get; set; }
    }
    
}
