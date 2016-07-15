using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationLayer.configVlidators
{
    public class ServiceCountValidator : IServiceCountValidator
    {
        public bool Validate(int masterCount, int slaveCount)
        {
            return masterCount != 1 ?  false : slaveCount >= 0 ? true : false; 
        }
    }
}
