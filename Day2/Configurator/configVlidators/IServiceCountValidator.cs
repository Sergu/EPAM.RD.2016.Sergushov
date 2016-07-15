using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.configVlidators
{
    public interface IServiceCountValidator
    {
        bool Validate(int masterCount, int slaveCount);
    }
}
