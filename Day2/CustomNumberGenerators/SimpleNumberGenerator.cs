using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomNumberGenerators
{
    public class SimpleNumberGenerator : INumberGenerator
    {
        public IEnumerable<int> GenerateNumberEnumerable()
        {
            int divCount;
            List<int> list = new List<int>();
            for (int i = 2; i < int.MaxValue; i++)
            {
                divCount = 0;
                for (int j = 1; j <= i; j++)
                {
                    if (i % j == 0)
                        divCount++;
                }
                if (divCount == 2)
                {
                    yield return i;
                }
            }
        }
    }
}
