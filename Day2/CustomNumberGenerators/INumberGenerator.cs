using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomNumberGenerators
{
    public interface INumberGenerator
    {
        IEnumerable<int> GenerateNumberEnumerable();
    }
}
